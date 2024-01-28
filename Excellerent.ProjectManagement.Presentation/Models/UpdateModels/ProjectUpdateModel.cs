using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Presentation.Models.UpdateModels
{
    public class ProjectUpdateModel
    {
        
        public Guid Guid { get; set; }

        public string ProjectName { get; set; }
        public string Description { get; set; }
        public ProjectType ProjectType { get; set; }
        public Guid ProjectStatusGuid { get; set; }
        public Guid ClientGuid { get; set; }
        public Guid SupervisorGuid { get; set; }
        public DateTime StartDate { get; set; }
        public string EndDate { get; set; }

    }

    public static class ProjectDetailsEntityMapper
    {
        public static ProjectEntity MappToEntity(this ProjectUpdateModel model)
        {
            var projecrEntity = new ProjectEntity();


            ProjectEntity projectEntity = new ProjectEntity();
            projectEntity.Guid = model.Guid;
            projectEntity.ProjectName = model.ProjectName;
            projectEntity.ProjectType = model.ProjectType;
            if (string.IsNullOrEmpty(model.ProjectStatusGuid.ToString()))
            {
                projectEntity.ProjectStatusGuid = Guid.Empty;
            }
            if (!string.IsNullOrEmpty(model.ProjectStatusGuid.ToString()))
            {
                projectEntity.ProjectStatusGuid = model.ProjectStatusGuid;
            }
            projectEntity.SupervisorGuid = model.SupervisorGuid;
            projectEntity.ClientGuid = model.ClientGuid;
            projectEntity.StartDate = model.StartDate;
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                projectEntity.EndDate = Convert.ToDateTime(model.EndDate);
            }
            projectEntity.Description = model.Description;
            return projectEntity;
        }

    }
}