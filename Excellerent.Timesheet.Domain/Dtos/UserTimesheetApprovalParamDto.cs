using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Dtos
{
    public class UserTimesheetApprovalParamDto
    {
        public Guid EmployeeGuId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public string? SortField { get; set; }
        public SortOrder Sort { get; set; } 
        public  Guid[] ProjectFilters { get; set; }
        public  Guid[] ClientFilters { get; set; }
        public ApprovalStatus[] StatusFilter { get; set; }
        public DateTime? DateWeek { get; set; }
}
 
}
