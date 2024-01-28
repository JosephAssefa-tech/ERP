using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.SharedModules.DTO;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Helpers;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Presentation.Utilities;
using Excellerent.UserManagement.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TimeSheetController : AuthorizedController
    {
        private readonly ITimeSheetService _timeSheetService;
        private readonly ITimeEntryService _timeEntryService;
        private readonly ITimesheetApprovalService _timesheetApprovalService;
        private readonly static string _feature = "Timesheet";

        public TimeSheetController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, ITimeSheetService timeSheetService, ITimeEntryService timeEntryService, ITimesheetApprovalService timesheetAprovalService) : base(htttpContextAccessor, configuration, _businessLog, _feature)
        {
            _timeSheetService = timeSheetService;
            _timeEntryService = timeEntryService;
            _timesheetApprovalService = timesheetAprovalService;
        }

        #region Timesheet

        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Timesheet" })]
        [HttpGet("Timesheets/{id}")]
        public Task<ResponseDTO> GetTimesheet(Guid id)
        {
            return _timeSheetService.GetTimeSheet(id);
        }


        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Timesheet" })]
        [HttpGet("Timesheets")]
        public Task<ResponseDTO> GetTimesheet(Guid employeeId, DateTime? date)
        {
            return _timeSheetService.GetTimeSheet(employeeId, date);
        }

        [Authorize]
        [HttpGet("IsEmployeeWithTimeesheetExists")]
        public async Task<bool> IsEmployeeWithTimeesheetExists(Guid employeeGuid)
        {
           return await _timeSheetService.IsEmployeeWithTimeesheetExists(employeeGuid);
        }

        #endregion

        #region Time Entry

        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Timesheet" })]
        [HttpGet("TimeEntries/{id}")]
        public async Task<ResponseDTO> GetTimeEntry(Guid id, bool requestedForApproval = false)
        {
            if (requestedForApproval)
            {
                var response = await _timeSheetService.GetTimeSheet(id, requestedForApproval);

                if (response.ResponseStatus != ResponseStatus.Success) 
                {
                    return response;
                }

                response.Data = (response.Data as TimeSheet).TimeEntry.Where(te => te.Guid == id).Select(te => te.MapToDto()).FirstOrDefault();

                return response;
            }
            else
            {
                return await _timeEntryService.GetTimeEntry(id);
            }
        }

        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Timesheet" })]
        [HttpGet("TimeEntries")]
        public async Task<ResponseDTO> GetTimeEntries(Guid timeSheetId, DateTime? date, Guid? projectId, bool requestedForApproval = false)
        {
            if (requestedForApproval)
            {
                var response = await _timeSheetService.GetTimeSheet(timeSheetId, requestedForApproval);

                if (response.ResponseStatus != ResponseStatus.Success)
                {
                    return response;
                }

                var timeEntry = (response.Data as TimeSheet).TimeEntry
                    .Where(te => (date == null || te.Date == date) && (projectId == null || te.ProjectId == projectId))
                    .Select(te => te.MapToDto());

                response.Message = "List of Time Entry by timesheet, date, and project Id";
                response.Data = timeEntry;

                return response;
            }
            else
            {
                return await _timeEntryService.GetTimeEntries(timeSheetId, date, projectId);
            }
        }

        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Submit_Timesheet" })]
        [HttpPost("TimeEntries")]
        public Task<ResponseDTO> AddTimeEntry([FromQuery] Guid employeeId, [FromBody] TmpTimeEntryDto timeEntryDto)
        {
            return _timeEntryService.AddTimeEntry(employeeId, timeEntryDto);
        }

        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Edit_TimeEntry" })]
        [HttpPut("TimeEntries")]
        public Task<ResponseDTO> UpdateTimeEntry(TmpTimeEntryDto timeEntryDto)
        {
            return _timeEntryService.UpdateTimeEntry(timeEntryDto);
        }
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Submit_Timesheet" })]
        [HttpPost("TimeEntriesForRange")]
        public Task<ResponseDTO> AddTimeEntry([FromQuery] Guid employeeId, [FromBody] TmpTimeEntryDto[] entries)
        {
            return _timeEntryService.AddTImeEntryForRangeOfDays(employeeId, entries);
        }
        [HttpDelete("DeleteTimeEntry")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_TimeEntry" })]
        public async Task<ResponseDTO> DeleteTimeEntry(Guid timeEntryId)
        {
            try
            {
                return await _timeEntryService.RemoveTimeEntryById(timeEntryId);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        #endregion

        #region TimesheetApproval

        [HttpGet("TimesheetAproval")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Timesheet_Submissions" })]
        public async Task<ResponseDTO> GetApprovalStatus(Guid timesheetGuid)
        {
            try
            {
                var timesheetApprovalEntities = await _timesheetApprovalService.GetTimesheetApprovalStatus(timesheetGuid);

                if (timesheetApprovalEntities == null || timesheetApprovalEntities.Count() == 0)
                {
                    return new ResponseDTO(ResponseStatus.Success, "No Timesheet Approval status for this Timesheet.", null);
                }
                else
                {
                    return new ResponseDTO(ResponseStatus.Success, "List of Timesheet Approval Status for this Timesheet", timesheetApprovalEntities.Select(tsa => tsa.MapToDto()));
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        [HttpPost("TimesheetAproval")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Submit_Timesheet" })]
        public async Task<ResponseDTO> RequestForApproval(Guid timesheetGuid)
        {
            var result = await _timeEntryService.RequestForApproval(timesheetGuid);

            if (result.ResponseStatus == ResponseStatus.Success)
            {
                await _timeSheetService.SyncRequestedForApprovalTimeEntries(timesheetGuid);
            }

            return result;
        }

        [HttpGet("GetApprovalProjectDetails")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetApprovalProject()
        {
            return await _timesheetApprovalService.GetAprovalProject();
        }
        [HttpGet("GetApprovalClientDetails")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetApprovalClient()
        {
            return await _timesheetApprovalService.GetAprovalClient();
        }

        [HttpPost("TimesheetApprovalBulkApprove")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Approve_Timesheet" })]
        public async Task<ResponseDTO> TimesheetApprovalBulkApprove(List<Guid> guids)
        {
            var result = await _timesheetApprovalService.TimesheetApprovalBulkApprove(guids);

            if (result.ResponseStatus == ResponseStatus.Success) 
            {
                foreach (Guid timesheetId in (result.Data as List<Guid>)) 
                {
                    await _timeSheetService.SyncApprovedTimeEntries(timesheetId);
                }
            }

            return result;
        }

        [HttpPut("TimesheetProjectStatus")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Re-submit_Timesheet, Approve_Timesheet, Return_for_Review" })]
        public async Task<ResponseDTO> UpdateProjectApproval(TimesheetApprovalEntity entity)
        {
            var result = await _timesheetApprovalService.UpdateProjectApprovalStatus(entity);

            if (result.ResponseStatus == ResponseStatus.Success && entity.Status == ApprovalStatus.Approved)
            {
                await _timeSheetService.SyncApprovedTimeEntries(entity.TimesheetId);
            }

            return result;
        }

        //all approved timesheet
        [HttpGet("TimesheetsApprovalPaginated")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Timesheet_Submissions" })]
        public async Task<PredicatedResponseDTO> AllApprovalTimesheet(Guid? id, [FromQuery] PaginationParams paginationParams)
        {
            var predicate = TimesheetApprovalPredicate.ApprovalPredicate(id, paginationParams);
            return await _timesheetApprovalService.AllTimesheetAproval(predicate, paginationParams);
        }

        [HttpGet("UserTimesheetApprovalsHistory")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Timesheet_Submissions" })]
        public async Task<ResponseDTO> GetUserTimesheetApprovalsHistory([FromQuery] UserTimesheetApprovalParamDto paginationParams)

        {
            return await _timesheetApprovalService.GetUserTimesheetApprovalHistory(paginationParams, paginationParams.EmployeeGuId);

        }
        #endregion
    }
}
