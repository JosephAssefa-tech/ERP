using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Services
{
    public class TimeEntryValidationService : ITimeEntryValidationService
    {
        private readonly ITimeSheetService _timeSheetService;
        private readonly ITimeEntryService _timeEntryService;
        private readonly ITimesheetApprovalService _timesheetApprovalService;
        private readonly ITimeSheetConfigService _timeSheetConfigService;
        private readonly IEmployeeService _employeeService;

        public TimeEntryValidationService(
            ITimeEntryService timeEntryService,
            ITimeSheetService timeSheetService,
            ITimesheetApprovalService timesheetAprovalService,
            ITimeSheetConfigService timeSheetConfigService,
            IEmployeeService employeeService)
        {
            _timeSheetService = timeSheetService;
            _timeEntryService = timeEntryService;
            _timesheetApprovalService = timesheetAprovalService;
            _timeSheetConfigService = timeSheetConfigService;
            _employeeService = employeeService;
        }

        //Validate time entry for new time entry
        public async Task ValidateTimeEntry(Guid employeeId, TmpTimeEntryDto timeEntry)
        {
            var timesheetConfiguration = await GetTimesheetConfiguration();

            if (timeEntry.Date > DateTime.Now)
            {
                throw new Exception("Can not add time entry for future date.");
            }
            else if (timeEntry.Hour <= 0)
            {
                throw new Exception("Can not add time entry with 0 or less than 0 hour.");
            }
            else if (timeEntry.Hour > timesheetConfiguration.WorkingHours.Max)
            {
                throw new Exception($"Can not add time entry with more than {timesheetConfiguration.WorkingHours.Max} hour.");
            }

            ResponseDTO responseDto = await _timeSheetService.GetTimeSheet(employeeId, timeEntry.Date);

            if (responseDto.Data == null)
            {
                return;
            }

            Guid timesheetId = (responseDto.Data as TimeSheetDto).Guid;

            responseDto = await _timeEntryService.GetTimeEntries(timesheetId, timeEntry.Date, null);

            IEnumerable<TmpTimeEntryDto> timeEntryDtos = (responseDto.Data as IEnumerable<TmpTimeEntryDto>);

            if (timeEntryDtos == null || timeEntryDtos.Count() == 0)
            {
                return;
            }

            float totalHour = 0;

            foreach (TmpTimeEntryDto timeEntryDto in timeEntryDtos)
            {
                totalHour += timeEntryDto.Hour;
            }
            totalHour += timeEntry.Hour;

            if (totalHour > timesheetConfiguration.WorkingHours.Max)
            {
                throw new Exception($"Can not add time entry with more than {timesheetConfiguration.WorkingHours.Max} hour.");
            }

            IEnumerable<TimesheetApprovalEntity> timesheetApprovals = await _timesheetApprovalService.GetTimesheetApprovalStatus(timesheetId);

            if (timesheetApprovals == null || timesheetApprovals.Count() == 0)
            {
                return;
            }

            if (timesheetApprovals.Where(tsa => tsa.Status != ApprovalStatus.Approved).Count() == 0) 
            {
                throw new Exception("Can not add time entry for approved timesheet");
            }

            TimesheetApprovalEntity timesheetApproval = timesheetApprovals.Where(tsa => tsa.ProjectId == timeEntry.ProjectId).FirstOrDefault();

            if (timesheetApproval != null && timesheetApproval.Status == ApprovalStatus.Approved)
            {
                throw new Exception("Can not add time entry for approved timesheet.");
            }
        }

        // validate time entry for existing time entry
        public async Task ValidateTimeEntry(TmpTimeEntryDto timeEntry)
        {
            var timesheetConfiguration = await GetTimesheetConfiguration();

            if (timeEntry.Date > DateTime.Today)
            {
                throw new Exception("Can not add time entry for future date.");
            }
            else if (timeEntry.Hour <= 0)
            {
                throw new Exception("Can not add time entry with 0 or less than 0 hour.");
            }
            else if (timeEntry.Hour > timesheetConfiguration.WorkingHours.Max)
            {
                throw new Exception($"Can not add time entry with more than {timesheetConfiguration.WorkingHours.Max} hour.");
            }

            Guid timesheetId = timeEntry.TimeSheetId;

            ResponseDTO responseDto = await _timeEntryService.GetTimeEntryForUpdateOrDelete(timeEntry.Guid);

            TmpTimeEntryDto[] timeEntryDtos = (responseDto.Data as TmpTimeEntryDto[]);

            if (timeEntryDtos == null || timeEntryDtos.Length == 0)
            {
                return;
            }

            float totalHour = 0;

            foreach (TmpTimeEntryDto timeEntryDto in timeEntryDtos)
            {
                totalHour += timeEntryDto.Hour;
            }
            totalHour += timeEntry.Hour;

            if (totalHour > timesheetConfiguration.WorkingHours.Max)
            {
                throw new Exception($"Can not add time entry with more than {timesheetConfiguration.WorkingHours.Max} hour.");
            }

            IEnumerable<TimesheetApprovalEntity> timesheetApprovals = await _timesheetApprovalService.GetTimesheetApprovalStatus(timesheetId);

            if (timesheetApprovals == null || timesheetApprovals.Count() == 0)
            {
                return;
            }

            if (timesheetApprovals.Where(tsa => tsa.Status != ApprovalStatus.Approved).Count() == 0) 
            {
                throw new Exception("Can not add time entry for approved timesheet");
            }

            TimesheetApprovalEntity timesheetApproval = timesheetApprovals.Where(tsa => tsa.ProjectId == timeEntry.ProjectId).FirstOrDefault();

            if (timesheetApproval != null && timesheetApproval.Status == ApprovalStatus.Approved) 
            {
                throw new Exception("Can not add time entry for approved timesheet");
            }
        }
        public async Task ValidateDeleteTimeEntry(TmpTimeEntryDto timeEntryDto)
        {
            if ((await _timesheetApprovalService.GetTimesheetApprovalStatus(timeEntryDto.TimeSheetId)).ToList().Find(x => x.ProjectId == timeEntryDto.ProjectId && x.Status == ApprovalStatus.Approved) != null)
            {
                throw new Exception("Cannot delete time entry that is approved");
            }
        }

        public async Task ValidateMinimumWorkingDayAndWorkingHourForApproval(Guid timesheetId)
        {

            var timesheetConfiguration = await GetTimesheetConfiguration();
            var timeEntries = (await _timeEntryService.GetTimeEntries(timesheetId, null, null))?.Data as List<TmpTimeEntryDto>;

            try
            {
                var empId = _timeSheetService.GetTimeSheet(timesheetId)?.Result?.Data?.EmployeeId;
                if (_employeeService.GetEmployeesById(empId) != null && _employeeService.GetEmployeesById(empId).EmployeeOrganization.EmploymentType == "contract")
                {
                    return;
                }
            }
            catch (Exception ex) {}

            try
            {
                var timesheetConfig = new ConfigurationDto();

                if (timeEntries == null || timeEntries.Count == 0)
                {
                    throw new Exception("No Time Entry to request for approval. Please add Time Entry First.");
                }
                
                timesheetConfiguration.WorkingDays = timesheetConfiguration.WorkingDays.Select(wd => wd.ToUpper()).ToList();

                List<string> weekDays = timeEntries.Select(te => te.Date.DayOfWeek.ToString().ToUpper()).ToList();

                foreach (string workingDay in timesheetConfiguration.WorkingDays)
                {
                    if (!weekDays.Contains(workingDay))
                    {
                        throw new Exception($"Pelase add time entry for {workingDay} before requesting for approval.");
                    }
                }

                if (timesheetConfiguration.WorkingHours.Min <= 0) // Why check???
                {
                    return;
                }

                List<DateTime> timeEntryDates = timeEntries.Select(te => te.Date).ToList();

                foreach (var date in timeEntryDates)
                {
                    if (!timesheetConfiguration.WorkingDays.Contains(date.DayOfWeek.ToString().ToUpper()))
                    {
                        continue;
                    }

                    var totalHour = timeEntries.FindAll(te => te.Date == date).Select(te => te.Hour).Sum();

                    if (totalHour < timesheetConfiguration.WorkingHours.Min)
                    {
                        throw new Exception("The minimum hours per day should be filled");
                    }
                    if (totalHour > timesheetConfig.WorkingHours.Max)
                    {
                        throw new Exception("Time entry for a day should not be more than the maximum hour for a day");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private async Task<ConfigurationDto> GetTimesheetConfiguration()
        {
            ConfigurationDto timesheetConfiguration = (await _timeSheetConfigService.GetTimeSheetConfiguration())?.Data as ConfigurationDto;
            ConfigurationDto tmpConfigurationDto = new ConfigurationDto();

            if (timesheetConfiguration == null)
            {
                return tmpConfigurationDto;
            }

            if(timesheetConfiguration.StartOfWeeks == null || timesheetConfiguration.StartOfWeeks.Count() == 0) 
            {
                timesheetConfiguration.StartOfWeeks = tmpConfigurationDto.StartOfWeeks;
            }

            if (timesheetConfiguration.WorkingDays == null)
            {
                timesheetConfiguration.WorkingDays = tmpConfigurationDto.WorkingDays;
            }

            if (timesheetConfiguration.WorkingHours == null)
            {
                timesheetConfiguration.WorkingHours = tmpConfigurationDto.WorkingHours;
            }

            return timesheetConfiguration;
        }
    }
}
    

