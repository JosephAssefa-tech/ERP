using System;

namespace Excellerent.ResourceManagement.Domain.DTOs
{
    public class EmployeeBillabilityDto
    {
        public Guid guid { get; set; }
        public string employeeName { get; set; }
        public string photo { get; set; } 
        public string jobTitle { get; set; }
        public string reportingManager { get; set; }
        public string EmpType { get; set; }


    }
}
