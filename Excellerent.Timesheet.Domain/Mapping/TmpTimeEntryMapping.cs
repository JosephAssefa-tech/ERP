using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Dtos;

namespace Excellerent.Timesheet.Domain.Mapping
{
    public static class TmpTimeEntryMapping
    {
        public static TmpTimeEntry MapToModel(this TmpTimeEntryDto timeEntryDto) 
        {
            TmpTimeEntry timeEntry = new TmpTimeEntry();

            timeEntry.Guid = timeEntryDto.Guid;
            timeEntry.Note = timeEntryDto.Note;
            timeEntry.Date = timeEntryDto.Date;
            timeEntry.Index = timeEntryDto.Index;
            timeEntry.Hour = timeEntryDto.Hour;
            timeEntry.ProjectId = timeEntryDto.ProjectId;
            timeEntry.TimesheetGuid = timeEntryDto.TimeSheetId;

            return timeEntry;
        }

        public static TmpTimeEntryDto MapToDto(this TmpTimeEntry timeEntry)
        {
            TmpTimeEntryDto timeEntryDto = new TmpTimeEntryDto();

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
