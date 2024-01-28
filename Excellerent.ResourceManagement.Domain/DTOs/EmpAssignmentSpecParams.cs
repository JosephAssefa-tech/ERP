using System;
using System.Collections.Generic;

namespace Excellerent.ResourceManagement.Domain.DTOs
{
    public enum EmpStatusTypes
    {
        Bench,
        Billable ,
        NonBillable ,
        Internal 
    }

    public class EmpAssignmentSpecParams
    {
        public Guid? id { get; set; }
        public List<string> Role { get; set; }
        public List<string> ReportingManager { get; set; }
        public string EmpType { get; set; }
        public string? searchKey { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;


    }
}
