using System;

namespace Excellerent.Timesheet.Domain.Dtos
{
    public class TimeEntryDto
    {
        public Guid Guid { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public int Index { get; set; }
        public int Hour { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TimeSheetId { get; set; }
    }
}
