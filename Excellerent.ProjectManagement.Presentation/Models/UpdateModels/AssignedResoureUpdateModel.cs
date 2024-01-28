using Excellerent.ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Presentation.Models.UpdateModels
{
        public class AssignedResoureUpdateModel
        {
             public Guid  Guid { get; set; }
            public Guid EmployeeGuid { get; set; }
            public Guid ProjectGuid { get; set; }
            public DateTime AssignDate { get; set; }
            public Boolean? Billable { get; set; }
    }
        public static class MappAssinResource
        {
            public static AssignResourceEntity MappToEntity(this AssignedResoureUpdateModel model)
            {
                AssignResourceEntity assignResourceEntity = new AssignResourceEntity();
                 assignResourceEntity.Guid = model.Guid;
                assignResourceEntity.ProjectGuid = model.ProjectGuid;
                assignResourceEntity.EmployeeGuid = model.EmployeeGuid;
                assignResourceEntity.AssignDate = model.AssignDate;
               assignResourceEntity.Billable = model.Billable;
                return assignResourceEntity;
            }
        }
    
}
