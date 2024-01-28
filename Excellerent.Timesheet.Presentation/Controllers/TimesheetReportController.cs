using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.SharedModules.DTO;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.UserManagement.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/TimeSheet/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeSheetReportController : AuthorizedController
    {
        private readonly ITimeSheetService _timeSheetService;
        private readonly ITimesheetApprovalService _timeSheetApprovalService;
        private readonly static string _feature = "TimeSheetReport";
        public TimeSheetReportController(ITimeSheetService timeSheetService, ITimesheetApprovalService timesheetApprovalService, IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog)
            : base(htttpContextAccessor, configuration, _businessLog, _feature)
        {
            _timeSheetService = timeSheetService;
            _timeSheetApprovalService = timesheetApprovalService;
        }

        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Monthly_Timesheet_Report" })]
        [HttpGet("{fromDate}, {toDate}")]
        public async Task<ResponseDTO> GetTimeSheet(Guid? clientId, string projectIds, DateTime fromDate, DateTime toDate)
        {
            string[] Ids = string.IsNullOrEmpty(projectIds) ? new string[] { } : projectIds.Split(',');


            List<Guid> projectGuids = new List<Guid>();
            for (int i = 0; i < Ids.Length; i++)
            {
                projectGuids.Add(Guid.Parse(Ids[i]));
            }
            return await _timeSheetService.GetTimeSheetAgregateReport(clientId, projectGuids, fromDate, toDate);
        }

        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Monthly_Timesheet_Report" })]
        [HttpGet("TimsheetForExport")]
        public async Task<ResponseDTO> GetTimesheetForExport(DateTime fromDate, DateTime toDate, Guid? clientId, string projectIds)
        {
            string[] Ids = string.IsNullOrEmpty(projectIds) ? new string[] { } : projectIds.Split(",");
            List<Guid> projectGuids = new List<Guid>();
            for (int i = 0; i < Ids.Length; i++)
            {
                projectGuids.Add(Guid.Parse(Ids[i].Trim()));
            }

            var response = await _timeSheetService.GetTimeSheetsForReport(clientId, projectGuids, fromDate, toDate);

            return response;
        }
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Monthly_Timesheet_Report" })]
        [HttpGet("ProjectOnBillableHours")]


        public async Task<ResponseDTO> ProjectOnBillabelity([FromQuery] DashboardProjectParams dashboardProjectParams)
        {
            return await this._timeSheetApprovalService.GetDashboardTimesheetApprovedProject(dashboardProjectParams);

        }


    }
}
