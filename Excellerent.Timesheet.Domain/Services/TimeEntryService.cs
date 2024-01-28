using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Services
{
    public class TimeEntryService : CRUD<TmpTimeEntryEntity, TmpTimeEntry>, ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly ITimeSheetService _timesheetService;
        private readonly ITimesheetApprovalService _timesheetAprovalService;
        private readonly ITimeEntryValidationService _timeEntryValidationService;
        private readonly ProjectManagement.Domain.Interfaces.ServiceInterface.IProjectService _projectService;

        public TimeEntryService(ITimeEntryRepository repository,
            ITimeSheetService timeSheetService,
            ITimesheetApprovalService timesheetAprovalService,
            ITimeSheetConfigService timeSheetConfigService,
            IEmployeeService employeeService,
            ProjectManagement.Domain.Interfaces.ServiceInterface.IProjectService projectService) : base(repository)
        {
            _timeEntryRepository = repository;
            _timesheetService = timeSheetService;
            _timesheetAprovalService = timesheetAprovalService;
            _timeEntryValidationService = new TimeEntryValidationService(this, timeSheetService, timesheetAprovalService, timeSheetConfigService, employeeService );
            _projectService = projectService;
        }

        public async Task<ResponseDTO> RequestForApproval(Guid timesheetId)
        {
            List<TimesheetApprovalEntity> timesheetApprovalEntities = (await _timesheetAprovalService.GetTimesheetApprovalForUpdateOrDelete(timesheetId)).ToList();
            List<Guid> approvedProjectIds = new List<Guid>();
            List<TmpTimeEntryDto> timeEntries = (await GetTimeEntries(timesheetId, null, null)).Data as List<TmpTimeEntryDto>;

            if (timeEntries == null || timeEntries.Count() == 0)
            {
                return null;
            }

            if (timesheetApprovalEntities.Count() > 0) 
            {
                approvedProjectIds = timesheetApprovalEntities.Where(tsa => tsa.Status == ApprovalStatus.Approved).Select(tsa => tsa.ProjectId).ToList();

                timesheetApprovalEntities = timesheetApprovalEntities.Where(tsa => tsa.Status != ApprovalStatus.Approved).Select(tsa => 
                {
                    tsa.TimeSheet = null;
                    tsa.Project = null;
                    return tsa;
                }).ToList();

                foreach (TimesheetApprovalEntity timesheetApproval in timesheetApprovalEntities) 
                {
                    await _timesheetAprovalService.Delete(timesheetApproval);
                }
            }

            timesheetApprovalEntities.Clear();

            timeEntries = timeEntries.Where(te => !approvedProjectIds.Contains(te.ProjectId)).ToList();

            if (timeEntries.Count() == 0)
            {
                return null;
            }

            foreach (var timeEntry in timeEntries)
            {
                var timesheetApprovalEntity = timesheetApprovalEntities.Where(tse => tse.TimesheetId == timeEntry.TimeSheetId && tse.ProjectId == timeEntry.ProjectId).FirstOrDefault();

                if (timesheetApprovalEntity != null) { continue; }

                timesheetApprovalEntity = new TimesheetApprovalEntity
                {
                    Guid = Guid.NewGuid(),
                    TimesheetId = timeEntry.TimeSheetId,
                    ProjectId = timeEntry.ProjectId,
                    Status = ApprovalStatus.Requested
                };

                await _timesheetAprovalService.Add(timesheetApprovalEntity);

                timesheetApprovalEntities.Add(timesheetApprovalEntity);
            }

            return new ResponseDTO(ResponseStatus.Success, "", timesheetApprovalEntities.Select(tsa => tsa.MapToDto()).ToList());
        }

        public async Task<ResponseDTO> GetTimeEntry(Guid timeEntryId)
        {
            try
            {
                TmpTimeEntry timeEntry = await _timeEntryRepository.GetTimeEntry((
                        te => te.Guid == timeEntryId
                    ));

                return new ResponseDTO(ResponseStatus.Success, "Time Entry by Id", timeEntry?.MapToDto());
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> GetTimeEntryForUpdateOrDelete(Guid timeEntryId)
        {
            try
            {
                TmpTimeEntry timeEntry = await _timeEntryRepository.GetTimeEntryForUpdateOrDelete((te => 
                        te.Guid == timeEntryId
                    ));

                return new ResponseDTO(ResponseStatus.Success, "Time Entry by Id for Update or Delete", timeEntry?.MapToDto());
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> GetTimeEntries(Guid timeSheetId, DateTime? date, Guid? projectId)
        {
            try
            {
                var timeEntries = await _timeEntryRepository.GetTimeEntries((te => 
                        te.TimesheetGuid == timeSheetId &&
                        (date == null || te.Date == date) &&
                        (projectId == null || te.ProjectId == projectId)
                    ));

                return new ResponseDTO(ResponseStatus.Success, "List of Time Entry by timesheet, date, and project Id", timeEntries.Select(x => x.MapToDto()).ToList());
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> AddTimeEntry(Guid employeeId, TmpTimeEntryDto timeEntryDto)
        {
            try
            {
                await _timeEntryValidationService.ValidateTimeEntry(employeeId, timeEntryDto);

                ResponseDTO timesheetResponse = await _timesheetService.GetTimeSheet(employeeId, timeEntryDto.Date);

                if (timesheetResponse.ResponseStatus == ResponseStatus.Error)
                {
                    return timesheetResponse;
                }
                else if (timesheetResponse.Data != null)
                {
                    timeEntryDto.TimeSheetId = (timesheetResponse.Data as TimeSheetDto).Guid;
                    timeEntryDto.Guid = Guid.NewGuid();
                    timeEntryDto.Date = (DateTime)timeEntryDto.Date.Date;

                    var timeEntry = await _timeEntryRepository.AddTimeEntry(timeEntryDto.MapToModel());

                    return new ResponseDTO(ResponseStatus.Success, "Time Entry Added Successfully", timeEntryDto);
                }
                else
                {
                    timeEntryDto.Guid = Guid.NewGuid();
                    timeEntryDto.Date = (DateTime)timeEntryDto.Date.Date;
                    return await _timesheetService.AddTimeSheet(employeeId, timeEntryDto);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> UpdateTimeEntry(TmpTimeEntryDto timeEntryDto)
        {
            try
            {
                await _timeEntryValidationService.ValidateTimeEntry(timeEntryDto);

                ResponseDTO timeEntryResponse = await GetTimeEntryForUpdateOrDelete(timeEntryDto.Guid);

                if (timeEntryResponse.ResponseStatus == ResponseStatus.Error)
                {
                    return timeEntryResponse;
                }
                else if (timeEntryResponse.Data != null)
                {
                    await _timeEntryRepository.UpdateTimeEntry(timeEntryDto.MapToModel());

                    return new ResponseDTO(ResponseStatus.Success, "TimeEntry updated successfully.", null);
                }
                else
                {
                    return new ResponseDTO(ResponseStatus.Info, "Can not update nonexistent TimeEntry", null);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> AddTImeEntryForRangeOfDays(Guid employeeId, TmpTimeEntryDto[] entries)
        {
            try
            {
                string message;
                ResponseStatus status;
                List<ResponseDTO> responses = new List<ResponseDTO>();
                for (int i = 0; i < entries.Length; i++)
                {
                    var prev = await _timeEntryRepository.GetTimeEntryForUpdateOrDelete(x => x.Guid == entries[i].MapToModel().Guid);
                    var result = prev != null ? await this.UpdateTimeEntry(entries[i]) : await this.AddTimeEntry(employeeId, entries[i]);
                    responses.Add(result);
                }
                if ((responses.Find(x => x.ResponseStatus == ResponseStatus.Error || x.ResponseStatus == ResponseStatus.Warning) != null))
                {
                    message = "Some entries were not added successfully.";
                    status = ResponseStatus.Warning;
                }
                else
                {
                    message = "Time Entries added successfully";
                    status = ResponseStatus.Success;
                }
                return new ResponseDTO(status, message, responses);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> RemoveTimeEntryById(Guid timeEntryId)
        {
            try
            {
                var entryToDelete = await _timeEntryRepository.GetByGuidAsync(timeEntryId);
                if (entryToDelete != null)
                {
                    await _timeEntryValidationService.ValidateDeleteTimeEntry(entryToDelete.MapToDto());
                    await _timeEntryRepository.DeleteAsync(entryToDelete);
                    return new ResponseDTO(ResponseStatus.Success, "Time Entry deleted successfully", entryToDelete);
                }
                else
                {
                    return new ResponseDTO(ResponseStatus.Error, "TimeEntry doesnot exist", null);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> RemoveTimeEntry(TmpTimeEntryDto timeEntryDto)
        {
            try
            {
                await _timeEntryValidationService.ValidateDeleteTimeEntry(timeEntryDto);
                await _timeEntryRepository.DeleteAsync(timeEntryDto.MapToModel());
                return new ResponseDTO(ResponseStatus.Success, "Time Entry deleted successfully", timeEntryDto);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }       
    }

}

