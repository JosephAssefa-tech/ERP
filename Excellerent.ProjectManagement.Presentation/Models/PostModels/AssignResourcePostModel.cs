using Excellerent.ProjectManagement.Domain.Entities;
using System;

namespace Excellerent.ProjectManagement.Presentation.Models.PostModels
{
    public class AssignResourcePostModel
    {
        public Guid EmployeeGuid { get; set; }
        public Guid ProjectGuid{ get; set; }
        public DateTime AssignDate { get; set; }
        public Boolean? Billable { get; set; }
    }
    public static class MappAssinResource
    {
        public static AssignResourceEntity MappToEntity(this AssignResourcePostModel model)
        {
            AssignResourceEntity assignResourceEntity = new AssignResourceEntity();
            assignResourceEntity.ProjectGuid = model.ProjectGuid;
            assignResourceEntity.EmployeeGuid = model.EmployeeGuid;
            assignResourceEntity.AssignDate = model.AssignDate;
            assignResourceEntity.Billable = model.Billable;
            return assignResourceEntity;
        }
    }
}
