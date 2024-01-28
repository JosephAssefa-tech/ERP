using System;

namespace Excellerent.Timesheet.Domain.Dtos
{
    public class TimeSheetDto
    {
        public Guid Guid { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? TotalHours { get; set; }
        public int Status { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
