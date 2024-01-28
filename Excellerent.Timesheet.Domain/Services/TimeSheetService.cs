using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Dtos.Report;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Services
{
    public class TimeSheetService : CRUD<TimeSheetEntity, TimeSheet>, ITimeSheetService
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly ITimesheetApprovalRepository _timesheetApprovalRepository;
        private readonly IAssignResourceRepository _assignResourceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientDetailsService _clientDetailsService;
        private readonly IProjectService _projectService;

        public TimeSheetService(
            ITimeSheetRepository timeSheetRepository,
            ITimesheetApprovalRepository timesheetApprovalRepository,
            IAssignResourceRepository assignResourceRepository,
            IEmployeeRepository employeeRepository,
            IClientDetailsService clientDetailsService,
            IProjectService projectService) : base(timeSheetRepository)
        {
            _timeSheetRepository = timeSheetRepository;
            _timesheetApprovalRepository = timesheetApprovalRepository;
            _assignResourceRepository = assignResourceRepository;
            _employeeRepository = employeeRepository;
            _clientDetailsService = clientDetailsService;
            _projectService = projectService;
        }

        public async Task<ResponseDTO> GetTimeSheet(Guid id, bool requestedForApproval = false)
        {
            try
            {
                var timeSheet = new TimeSheet();
                var response = new ResponseDTO();
                if (requestedForApproval)
                {
                    timeSheet = await _timeSheetRepository.GetTimeSheet(id);
                    response.ResponseStatus = ResponseStatus.Success;
                    response.Message = "Time Sheet by Id";
                    response.Data = timeSheet;
                }
                else
                {
                    timeSheet = await _timeSheetRepository.GetTimeSheet((ts => ts.Guid == id));
                    response.ResponseStatus = ResponseStatus.Success;
                    response.Message = "Time Sheet by Id";
                    response.Data = timeSheet?.MapToDto();
                }

                return response;
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }
        public async Task<ResponseDTO> GetTimeSheet(Guid employeeId, DateTime? dateTime)
        {
            DateTime localDateTime = dateTime ?? DateTime.Now;
            localDateTime = (DateTime)localDateTime.Date;

            DateTime fromDate = DateTimeUtility.GetWeeksFirstDate(localDateTime);
            DateTime toDate = DateTimeUtility.GetWeeksLastDate(localDateTime);

            return await GetTimeSheet(employeeId, fromDate, toDate);
        }
        public async Task<ResponseDTO> GetTimeSheet(Guid employeeId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.GetTimeSheet((
                        ts => ts.EmployeeId == employeeId && ts.FromDate == fromDate && ts.ToDate == toDate
                    ));

                return new ResponseDTO(ResponseStatus.Success, "Time Sheet by employee Id, from Date, and to Date", timeSheet?.MapToDto());
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> GetTimeSheetsForReport(Guid? clientId, List<Guid> projectIds, DateTime fromDate, DateTime toDate)
        {
            List<Guid> clientIds = (await _clientDetailsService.GetClientByName("Leave")).Select(c => c.Guid).ToList();
            List<Guid> leaveProjectIds = new List<Guid>();
            if (clientIds.Count > 0)
            {
                leaveProjectIds = (await _projectService.GetClientProjects(clientIds[0])).Select(p => p.Guid).ToList();
            }
            if (clientId != null)
            {
                if (projectIds.Count == 0)
                {
                    projectIds = (await _projectService.GetClientProjects((Guid)clientId)).Select(p => p.Guid).ToList();
                }
                clientIds.Add((Guid)clientId);
            }
            List<TimeSheet> timesheets = (await _timeSheetRepository.GetTimeSheetsForReport(clientIds, projectIds, leaveProjectIds, fromDate, toDate)).ToList();
            List<Guid> salesPersonIds = new List<Guid>();


            projectIds.Clear();

            foreach (TimeSheet timesheet in timesheets)
            {
                var tmpProjectIds = timesheet.TimeEntry.Select(te => te.ProjectId);
                var tmpSalesPersonIds = timesheet.TimeEntry.Select(te => te.Project.Client.SalesPersonGuid);

                projectIds.AddRange(tmpProjectIds);
                salesPersonIds.AddRange(tmpSalesPersonIds);
            }

            var assinedResources = (await _assignResourceRepository.FindAsync(asr => projectIds.Contains(asr.ProjectGuid))).ToList();

            var salesPersons = (await _employeeRepository.FindAsync(emp => salesPersonIds.Contains(emp.Guid))).ToList();

            var timesheetReportDtos = timesheets.MapToReportDto(fromDate, toDate, assinedResources, salesPersons);

            return new ResponseDTO(ResponseStatus.Success, "Timesheet List for Report", timesheetReportDtos);
        }

        public async Task<ResponseDTO> AddTimeSheet(Guid employeeId, TmpTimeEntryDto timeEntryDto)
        {
            TimeSheetDto timeSheetDto = new TimeSheetDto();
            DateTime localDateTime = (DateTime)timeEntryDto.Date.Date;
            DateTime fromDate = DateTimeUtility.GetWeeksFirstDate(localDateTime);
            DateTime toDate = DateTimeUtility.GetWeeksLastDate(localDateTime);

            timeSheetDto.Guid = Guid.NewGuid();
            timeSheetDto.FromDate = fromDate;
            timeSheetDto.ToDate = toDate;
            timeSheetDto.TotalHours = 0;
            timeSheetDto.Status = 0;
            timeSheetDto.EmployeeId = employeeId;

            return await AddTimeSheet(timeSheetDto, timeEntryDto);
        }
        public async Task<ResponseDTO> AddTimeSheet(TimeSheetDto timeSheetDto, TmpTimeEntryDto timeEntryDto)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.AddTimeSheet(timeSheetDto.MapToModel(timeEntryDto));

                return new ResponseDTO(ResponseStatus.Success, "Timesheet Added Successfully", timeSheet?.MapToDto());
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<TimeSheet> GetTimeSheetById(Guid id)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.GetTimeSheet((
                        ts => ts.Guid == id
                    ));

                return timeSheet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task SyncRequestedForApprovalTimeEntries(Guid timesheetId)
        {
            var timesheet = await _timeSheetRepository.GetTimeSheet(timesheetId);
            var timesheetApproval = await _timesheetApprovalRepository.FindAsync(tsa => tsa.TimesheetId == timesheetId);
            var requestedForApprovalProjects = timesheetApproval.Where(tsa => tsa.Status != ApprovalStatus.Approved).Select(tsa => tsa.ProjectId);
            var approvedProjects = timesheetApproval.Where(tsa => tsa.Status == ApprovalStatus.Approved).Select(tsa => tsa.ProjectId);
            var timeEntryIdsToRemove = new List<Guid>();

            if (requestedForApprovalProjects.Count() == 0)
            {
                return;
            }

            var tmpTimeEntries = timesheet.TmpTimeEntry.Where(te =>
            {
                return requestedForApprovalProjects.Contains(te.ProjectId);
            }).ToList();

            var timeEntries = timesheet.TimeEntry.Select(te => te).ToList();

            foreach (TimeEntry timeEntry in timesheet.TimeEntry)
            {
                var tmpTimeEntry = tmpTimeEntries.Where(te => te.ProjectId == timeEntry.ProjectId && te.TimesheetGuid == timeEntry.TimesheetGuid && te.Date == timeEntry.Date).FirstOrDefault();

                if (approvedProjects.Contains(timeEntry.ProjectId))
                {
                    continue;
                }
                else if (tmpTimeEntry == null)
                {
                    timeEntries.Remove(timeEntries.Where(te => te.Guid == timeEntry.Guid).FirstOrDefault());
                }
                else
                {
                    timeEntry.Hour = tmpTimeEntry.Hour;
                    timeEntry.Note = tmpTimeEntry.Note;
                }
            }

            tmpTimeEntries = timesheet.TmpTimeEntry.Where(tmpte =>
            {
                return timesheet.TimeEntry.Where(te => te.TimesheetGuid == tmpte.TimesheetGuid && te.ProjectId == tmpte.ProjectId && te.Date == tmpte.Date).ToList().Count() == 0;
            }).ToList();

            foreach (TmpTimeEntry timeEntry in tmpTimeEntries)
            {
                timeEntries.Add(new TimeEntry()
                {
                    Guid = Guid.NewGuid(),
                    Note = timeEntry.Note,
                    Date = timeEntry.Date,
                    Index = timeEntry.Index,
                    Hour = timeEntry.Hour,
                    ProjectId = timeEntry.ProjectId,
                    TimesheetGuid = timeEntry.TimesheetGuid
                });
            }

            await _timeSheetRepository.Update(timesheet.Guid, timeEntries);
        }

        public async Task SyncApprovedTimeEntries(Guid timesheetId)
        {
            var timesheet = await _timeSheetRepository.GetTimeSheet(timesheetId);
            var timesheetApproval = await _timesheetApprovalRepository.FindAsync(tsa => tsa.TimesheetId == timesheetId);
            var requestedForApprovalProjects = timesheetApproval.Where(tsa => tsa.Status != ApprovalStatus.Approved).Select(tsa => tsa.ProjectId);
            var approvedProjects = timesheetApproval.Where(tsa => tsa.Status == ApprovalStatus.Approved).Select(tsa => tsa.ProjectId);

            if (approvedProjects.Count() == 0)
            {
                return;
            }

            var timeEntries = timesheet.TimeEntry.Where(te =>
            {
                return approvedProjects.Contains(te.ProjectId);
            });

            var tmpTimeEntries = timesheet.TmpTimeEntry.Select(te => te).ToList();

            foreach (TmpTimeEntry tmpTimeEntry in timesheet.TmpTimeEntry)
            {
                var timeEntry = timeEntries.Where(te => te.ProjectId == tmpTimeEntry.ProjectId && te.TimesheetGuid == tmpTimeEntry.TimesheetGuid && te.Date == tmpTimeEntry.Date).FirstOrDefault();

                if (requestedForApprovalProjects.Contains(tmpTimeEntry.ProjectId))
                {
                    continue;
                }
                else if (timeEntry == null)
                {
                    tmpTimeEntries.Remove(tmpTimeEntries.Where(te => te.Guid == tmpTimeEntry.Guid).FirstOrDefault());
                }
                else
                {
                    tmpTimeEntry.Hour = timeEntry.Hour;
                    tmpTimeEntry.Note = timeEntry.Note;
                }
            }

            timeEntries = timesheet.TimeEntry.Where(te =>
            {
                return timesheet.TmpTimeEntry.Where(tmpte => tmpte.TimesheetGuid == te.TimesheetGuid && tmpte.ProjectId == te.ProjectId && tmpte.Date == te.Date).ToList().Count() == 0;
            }).ToList();

            foreach (TimeEntry timeEntry in timeEntries)
            {
                tmpTimeEntries.Add(new TmpTimeEntry()
                {
                    Guid = Guid.NewGuid(),
                    Note = timeEntry.Note,
                    Date = timeEntry.Date,
                    Index = timeEntry.Index,
                    Hour = timeEntry.Hour,
                    ProjectId = timeEntry.ProjectId,
                    TimesheetGuid = timeEntry.TimesheetGuid
                });
            }

            await _timeSheetRepository.Update(timesheet.Guid, tmpTimeEntries);
        }

        public async Task<bool> Update(TimeSheet t)
        {
            return await _timeSheetRepository.Update(t);
        }
        public async Task<bool> IsEmployeeWithTimeesheetExists(Guid employeeGuid)
        {
            return await _timeSheetRepository.IsEmployeeWithTimeesheetExists(employeeGuid);
        }

        #region Report
        public async Task<ResponseDTO> GetTimeSheetAgregateReport(Guid? clientId, List<Guid> projectGuids, DateTime? dateFrom, DateTime? dateTo)
        {
            var result_billables = await _timeSheetRepository.GetTimeSheetEntryHoursForReport(clientId, projectGuids, dateFrom, dateTo, true);
            //non billables
            var result_non_billables = await _timeSheetRepository.GetTimeSheetEntryHoursForReport(clientId, projectGuids, dateFrom, dateTo, false);


            var billableHours = (from p in result_billables.AsEnumerable()
                                 group p by (p.ProjectId, p.Project.ClientGuid, p.TimeSheet.Employee.Guid) into r
                                 select new TimeSheetAgregateReportModel
                                 {
                                     EmployeeGuid = r.Select(re => re.TimeSheet.Employee.Guid).FirstOrDefault(),
                                     ProjectId = r.Select(re => re.ProjectId).FirstOrDefault(),
                                     ProjectName = r.Select(re => re.Project.ProjectName).FirstOrDefault(),
                                     ClientGuid = r.Select(re => re.Project.ClientGuid).FirstOrDefault(),
                                     ClientName = r.Select(re => re.Project.Client.ClientName).FirstOrDefault(),
                                     ClientManagerName = r.Select(re => re.Project.Client.ClientContacts.Select(x => x.ContactPersonName).FirstOrDefault())?.FirstOrDefault(),
                                     // FirstName = r.Select(re => re.TimeSheet.Employee.FirstName).FirstOrDefault(),
                                     // LastName = r.Select(re => re.TimeSheet.Employee.GrandFatherName).FirstOrDefault(),
                                     FullName = r.Select(re => re.TimeSheet.Employee.FullName).FirstOrDefault(), // (r.Select(re => re.TimeSheet.Employee.FirstName).FirstOrDefault() ?? "") + " " + r.Select(re => re.TimeSheet.Employee.GrandFatherName).FirstOrDefault() ?? "",
                                     EmployeeRoleName = r.Select(re => re.TimeSheet.Employee.EmployeeOrganization.Role.Name).FirstOrDefault(),
                                     BillableHours = r.Sum(re => re.Hour),
                                     NonBillableHours = 0,

                                 }).ToList();


            var nonBillableHours = (from p in result_non_billables.AsEnumerable()
                                    group p by (p.ProjectId, p.Project.ClientGuid, p.TimeSheet.Employee.Guid) into r
                                    select new TimeSheetAgregateReportModel
                                    {
                                        EmployeeGuid = r.Select(re => re.TimeSheet.Employee.Guid).FirstOrDefault(),
                                        ProjectId = r.Select(re => re.ProjectId).FirstOrDefault(),
                                        ProjectName = r.Select(re => re.Project.ProjectName).FirstOrDefault(),
                                        ClientGuid = r.Select(re => re.Project.ClientGuid).FirstOrDefault(),
                                        ClientName = r.Select(re => re.Project.Client.ClientName).FirstOrDefault(),
                                        ClientManagerName = r.Select(re => re.Project.Client.ClientContacts.Select(x => x.ContactPersonName).FirstOrDefault())?.FirstOrDefault(),

                                        // FirstName = r.Select(re => re.TimeSheet.Employee.FirstName).FirstOrDefault(),
                                        // LastName = r.Select(re => re.TimeSheet.Employee.GrandFatherName).FirstOrDefault(),
                                        FullName = r.Select(re => re.TimeSheet.Employee.FullName).FirstOrDefault(), // (r.Select(re => re.TimeSheet.Employee.FirstName).FirstOrDefault() ?? "") + " " + (r.Select(re => re.TimeSheet.Employee.GrandFatherName).FirstOrDefault() ?? ""),
                                        EmployeeRoleName = r.Select(re => re.TimeSheet.Employee.EmployeeOrganization.Role.Name).FirstOrDefault(),
                                        NonBillableHours = r.Sum(re => re.Hour),
                                        BillableHours = 0,

                                    }).ToList();
            billableHours.AddRange(nonBillableHours);

            var combinedGroups = (from p in billableHours.AsEnumerable()
                                  group p by (p.ProjectId, p.ClientGuid, p.EmployeeGuid) into b
                                  select new TimeSheetAgregateReportModel
                                  {
                                      EmployeeGuid = b.Select(b => b.EmployeeGuid).FirstOrDefault(),
                                      ProjectId = b.Select(b => b.ProjectId).FirstOrDefault(),
                                      ProjectName = b.Select(b => b.ProjectName).FirstOrDefault(),
                                      ClientGuid = b.Select(b => b.ClientGuid).FirstOrDefault(),
                                      ClientName = b.Select(b => b.ClientName).FirstOrDefault(),
                                      ClientManagerName = b.Select(b => b.ClientManagerName).FirstOrDefault(),
                                      // FirstName = b.Select(b => b.FirstName).FirstOrDefault(),
                                      // LastName = b.Select(b => b.LastName).FirstOrDefault(),
                                      FullName = b.Select(b => b.FullName).FirstOrDefault(),
                                      EmployeeRoleName = b.Select(b => b.EmployeeRoleName).FirstOrDefault(),
                                      BillableHours = b.Sum(re => re.BillableHours),
                                      NonBillableHours = b.Sum(re => re.NonBillableHours),
                                  }).ToList();


            return new ResponseDTO(ResponseStatus.Success, "Timesheet List for Report", combinedGroups);
        }

        #endregion
    }
}
