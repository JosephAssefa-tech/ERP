using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Dtos;

namespace Excellerent.Timesheet.Domain.Mapping
{
    public static class TimeEntryMapping
    {
        public static TimeEntry MapToModel(this TimeEntryDto timeEntryDto) 
        {
            TimeEntry timeEntry = new TimeEntry();

            timeEntry.Guid = timeEntryDto.Guid;
            timeEntry.Note = timeEntryDto.Note;
            timeEntry.Date = timeEntryDto.Date;
            timeEntry.Index = timeEntryDto.Index;
            timeEntry.Hour = timeEntryDto.Hour;
            timeEntry.ProjectId = timeEntryDto.ProjectId;
            timeEntry.TimesheetGuid = timeEntryDto.TimeSheetId;

            return timeEntry;
        }

        public static TimeEntryDto MapToDto(this TimeEntry timeEntry)
        {
            TimeEntryDto timeEntryDto = new TimeEntryDto();

            timeEntryDto.Guid = timeEntry.Guid;
            timeEntryDto.Note = timeEntry.Note;
            timeEntryDto.Date = timeEntry.Date;
            timeEntryDto.Index = timeEntry.Index;
            timeEntryDto.Hour = timeEntry.Hour;
            timeEntryDto.ProjectId = timeEntry.ProjectId;
            timeEntryDto.TimeSheetId = timeEntry.TimesheetGuid;

            return timeEntryDto;
        }
    }
}
