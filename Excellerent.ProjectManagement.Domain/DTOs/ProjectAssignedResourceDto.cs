
using Excellerent.ProjectManagement.Domain.Models;
using System;


namespace Excellerent.ProjectManagement.Domain.DTOs
{
    public class ProjectAssignedResourceDto
    {
        public Guid Guid { get; set; }
        public Guid ProjectGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public Excellerent.ResourceManagement.Domain.DTOs.EmployeeDTO Empolyee { get; set; }

        public DateTime AssignDate { get; set; }
        public DateTime  CreatedDate{get;set;}
        public Boolean? Billable { get; set; }

        public ProjectAssignedResourceDto(AssignResourcEntity assignResourceEntity)
        {
            this.Guid = assignResourceEntity.Guid;
            this.ProjectGuid = assignResourceEntity.ProjectGuid;
            this.EmployeeGuid = assignResourceEntity.EmployeeGuid;
            this.Empolyee = new Excellerent.ResourceManagement.Domain.DTOs.EmployeeDTO(assignResourceEntity.Empolyee) ;
            this.AssignDate = assignResourceEntity.AssignDate;
            this.CreatedDate = assignResourceEntity.CreatedDate;
            this.Billable = assignResourceEntity.Billable;

        }
    }
}
