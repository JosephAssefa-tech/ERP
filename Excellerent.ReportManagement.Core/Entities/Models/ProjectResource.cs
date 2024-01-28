using System;

#nullable disable

namespace Excellerent.ReportManagement.Core.Entities.Models
{
    public class ProjectResource
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
    }
}
