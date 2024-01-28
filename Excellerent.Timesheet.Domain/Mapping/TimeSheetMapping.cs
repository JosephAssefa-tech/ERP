using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Dtos.Report;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellerent.Timesheet.Domain.Mapping
{
    public static class TimeSheetMapping
    {
        public static TimeSheet MapToModel(this TimeSheetDto timeSheetDto)
        {
            TimeSheet timeSheet = new TimeSheet();

            timeSheet.Guid = timeSheetDto.Guid;
            timeSheet.FromDate = timeSheetDto.FromDate;
            timeSheet.ToDate = timeSheetDto.ToDate;
            timeSheet.TotalHours = timeSheetDto.TotalHours;
            timeSheet.Status = timeSheetDto.Status;
            timeSheet.EmployeeId = timeSheetDto.EmployeeId;

            return timeSheet;
        }

        public static TimeSheet MapToModel(this TimeSheetDto timeSheetDto, TmpTimeEntryDto timeEntryDto)
        {
            TimeSheet timeSheet = new TimeSheet();

            timeSheet.Guid = timeSheetDto.Guid;
            timeSheet.FromDate = timeSheetDto.FromDate;
            timeSheet.ToDate = timeSheetDto.ToDate;
            timeSheet.TotalHours = timeSheetDto.TotalHours;
            timeSheet.Status = timeSheetDto.Status;
            timeSheet.EmployeeId = timeSheetDto.EmployeeId;
            timeSheet.TmpTimeEntry.Add(timeEntryDto.MapToModel());

            return timeSheet;
        }

        public static TimeSheetDto MapToDto(this TimeSheet timeSheet)
        {
            TimeSheetDto timeSheetDto = new TimeSheetDto();

            timeSheetDto.Guid = timeSheet.Guid;
            timeSheetDto.FromDate = timeSheet.FromDate;
            timeSheetDto.ToDate = timeSheet.ToDate;
            timeSheetDto.TotalHours = timeSheet.TotalHours;
            timeSheetDto.Status = timeSheet.Status;
            timeSheetDto.EmployeeId = timeSheet.EmployeeId;

            return timeSheetDto;
        }

        public static List<TimesheetReportDto> MapToReportDto(this List<TimeSheet> timeSheets, DateTime fromDate, DateTime toDate, List<AssignResourcEntity> assignResources, List<ResourceManagement.Domain.Models.Employee> salesPersons)
        {
            List<TimesheetReportDto> timesheetReportDtos = new List<TimesheetReportDto>();

            foreach (TimeSheet timeSheet in timeSheets)
            {
                List<TimeEntry> timeEntries = timeSheet.TimeEntry.Where(te => te.Project.Client.ClientName.ToUpper() != "Leave".ToUpper()).ToList();
                List<TimeEntry> leaveTimeEntries = timeSheet.TimeEntry.Where(te => te.Project.Client.ClientName == "Leave").ToList();
                List<Project> projects = timeEntries.Select(te => te.Project).ToList();
                List<AssignResourcEntity> empAssinedResources = assignResources.Where(ar => ar.EmployeeGuid == timeSheet.EmployeeId).ToList();

                //Prepare temporary Timesheet report Dto
                List<TimesheetReportDto> tmpTimesheetReportDtos = timeEntries.Select(te =>
                {
                    var timesheetReportDto = new TimesheetReportDto();
                    var tmpLeaveTimeEntries = leaveTimeEntries.Where(lte => lte.Date.Date == te.Date).ToList();

                    timesheetReportDto.Client = te.Project.Client.ClientName;
                    timesheetReportDto.ClientManager = salesPersons.Where(sp => sp.Guid == te.Project.Client.SalesPersonGuid).Select(sp => sp.FullName).FirstOrDefault();
                    timesheetReportDto.Month = string.Format("{0}, {1}", DateTimeUtility.Month[te.Date.Month - 1], te.Date.Year);
                    timesheetReportDto.ReportDate = DateTime.Now.Date;
                    timesheetReportDto.ProjectName = te.Project.ProjectName;
                    timesheetReportDto.Name = timeSheet.Employee.FullName;
                    timesheetReportDto.Role = timeSheet.Employee.EmployeeOrganization.Role.Name;
                    timesheetReportDto.Billable = assignResources.Where(ar => ar.EmployeeGuid == timeSheet.EmployeeId && ar.ProjectGuid == te.ProjectId).Select(ar => ar.Billable).FirstOrDefault() ?? false;
                    timesheetReportDto.Date = te.Date;
                    if (tmpLeaveTimeEntries.Count > 0)
                    {
                        switch (tmpLeaveTimeEntries[0].Project.ProjectName)
                        {
                            case "Casual Leave":
                                timesheetReportDto.Hours = "L";
                                break;
                            case "Sick Leave":
                                timesheetReportDto.Hours = "S";
                                break;
                            case "Vacation":
                                timesheetReportDto.Hours = "V";
                                break;
                            case "Medical/Maternity":
                                timesheetReportDto.Hours = "M";
                                break;
                            case "Holiday":
                                timesheetReportDto.Hours = "H";
                                break;
                            default:
                                timesheetReportDto.Hours = "L";
                                break;
                        }
                        foreach (var leaveTimeEntry in tmpLeaveTimeEntries)
                        {
                            leaveTimeEntries.Remove(leaveTimeEntry);
                        }
                    }
                    else
                    {
                        timesheetReportDto.Hours = te.Hour.ToString();
                    }

                    return timesheetReportDto;
                }).ToList();

                //Prepare unique leave Time Entries for a day
                leaveTimeEntries = leaveTimeEntries.Where(lte =>
                {
                    var tmpLeaveTimeEntries = leaveTimeEntries.Where(tlte => tlte.Date.Date == lte.Date).ToList();

                    if (tmpLeaveTimeEntries.IndexOf(lte) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }).ToList();


                //Add leave time entries to temporary timesheet report dto
                foreach (var lte in leaveTimeEntries)
                {
                    foreach (var Project in projects)
                    {
                        var timesheetReportDto = new TimesheetReportDto();

                        timesheetReportDto.Client = Project.Client.ClientName;
                        timesheetReportDto.ClientManager = salesPersons.Where(sp => sp.Guid == Project.Client.SalesPersonGuid).Select(sp => sp.FullName).FirstOrDefault();
                        timesheetReportDto.Month = string.Format("{0}, {1}", DateTimeUtility.Month[lte.Date.Month - 1], lte.Date.Year);
                        timesheetReportDto.ReportDate = DateTime.Now.Date;
                        timesheetReportDto.ProjectName = Project.ProjectName;
                        timesheetReportDto.Name = timeSheet.Employee.FullName;
                        timesheetReportDto.Role = timeSheet.Employee.EmployeeOrganization.Role.Name;
                        timesheetReportDto.Billable = assignResources.Where(ar => ar.EmployeeGuid == timeSheet.EmployeeId && ar.ProjectGuid == Project.Guid).Select(ar => ar.Billable).FirstOrDefault() ?? false;
                        timesheetReportDto.Date = lte.Date;
                        switch (Project.ProjectName)
                        {
                            case "Casual Leave":
                                timesheetReportDto.Hours = "L";
                                break;
                            case "Sick Leave":
                                timesheetReportDto.Hours = "S";
                                break;
                            case "Vacation":
                                timesheetReportDto.Hours = "V";
                                break;
                            case "Medical/Maternity":
                                timesheetReportDto.Hours = "M";
                                break;
                            default:
                                timesheetReportDto.Hours = "L";
                                break;
                        }

                        tmpTimesheetReportDtos.Add(timesheetReportDto);
                    }
                }
                //*/


                //Add unasigned time entry
                var tmpAssignedResources = empAssinedResources.Where(ar => ar.AssignDate.Date > timeSheet.FromDate.Date && ar.EmployeeGuid == timeSheet.EmployeeId).ToList();
                foreach (var assignedResource in tmpAssignedResources)
                {
                    DateTime startDate = timeSheet.FromDate;
                    DateTime endDate = timeSheet.ToDate;
                    var tmpProjects = projects.Where(p => p.Guid == assignedResource.ProjectGuid).ToList();

                    if (tmpProjects.Count == 0) { continue; }

                    var project = tmpProjects[0];

                    if (fromDate > endDate)
                    {
                        continue;
                    }

                    while (startDate.Date < assignedResource.AssignDate.Date && startDate.Date <= toDate.Date)
                    {
                        if (timeSheet.TimeEntry.Where(te => te.Date.Date == startDate.Date && te.ProjectId == project.Guid).Count() > 0)
                        {
                            startDate = startDate.AddDays(1);
                            continue;
                        }

                        var timesheetReportDto = new TimesheetReportDto();

                        timesheetReportDto.Client = project.Client.ClientName;
                        timesheetReportDto.ClientManager = salesPersons.Where(sp => sp.Guid == project.Client.SalesPersonGuid).Select(sp => sp.FullName).FirstOrDefault();
                        timesheetReportDto.Month = string.Format("{0}, {1}", DateTimeUtility.Month[startDate.Date.Month - 1], startDate.Date.Year);
                        timesheetReportDto.ReportDate = DateTime.Now.Date;
                        timesheetReportDto.ProjectName = project.ProjectName;
                        timesheetReportDto.Name = timeSheet.Employee.FullName;
                        timesheetReportDto.Role = timeSheet.Employee.EmployeeOrganization.Role.Name;
                        timesheetReportDto.Billable = assignResources.Where(ar => ar.EmployeeGuid == timeSheet.EmployeeId && ar.ProjectGuid == project.Guid).Select(ar => ar.Billable).FirstOrDefault() ?? false;
                        timesheetReportDto.Date = startDate.Date;
                        timesheetReportDto.Hours = "U";

                        tmpTimesheetReportDtos.Add(timesheetReportDto);

                        startDate = startDate.AddDays(1);
                    }
                }
                //*/

                //Add temporary timesheet report dto to timesheet report dto
                timesheetReportDtos.AddRange(tmpTimesheetReportDtos);
            }

            return timesheetReportDtos;
        }
    }
}
