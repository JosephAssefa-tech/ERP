using System;

#nullable disable

namespace Excellerent.ReportManagement.Core.Entities.Models
{
    public class ProjectResourceTimesheet
    {
        public Guid? ClientGuid { get; set; }
        public string ClientName { get; set; }
        public Guid? ProjectGuid { get; set; }
        public string ProjectName { get; set; }
        public Guid? SupervisorGuid { get; set; }
        public string SupervisorFullName { get; set; }
        public string SupervisorEmailAddress { get; set; }
        public Guid? EmployeeGuid { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeEmailAddress { get; set; }
        public Guid? TimesheetGuid { get; set; }
        public Guid? TimesheetApprovalGuid { get; set; }
        public int? TimesheetStatus { get; set; }
        public int? TimesheetApprovalStatus { get; set; }
        public DateTime? TimesheetFromDate { get; set; }
        public DateTime? TimesheetToDate { get; set; }
        public int? TimesheetTotalHours { get; set; }
    }
}
