using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Dtos
{
    public class TimesheetApprovalDto
    {
        public Guid TimesheetApprovalGuid { get; set; }
        public Guid TimesheetId { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public int TotalHours { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}
