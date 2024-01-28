using System;

namespace Excellerent.Timesheet.Domain.Dtos.Report
{
    public  class TimeSheetAgregateReportModel
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Guid ClientGuid { get; set; }
        public string ClientName { get; set; }
        public string ClientManagerName { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmployeeRoleName { get; set; }
        public int BillableHours { get; set; }
        public int NonBillableHours { get; set; }
    }
}
