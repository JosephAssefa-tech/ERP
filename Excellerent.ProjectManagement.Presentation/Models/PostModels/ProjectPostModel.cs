using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace Excellerent.ProjectManagement.Presentation.Models.PostModels
{
    public class ProjectPostModel
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public ProjectType ProjectType { get; set; }
        public Guid ProjectStatusGuid { get; set; }
        public Guid ClientGuid { get; set; }
        public Guid SupervisorGuid { get; set; }
        public DateTime StartDate { get; set; }
        public string EndDate { get; set; }
        public List<AssignResourcePostModel>? AssignResource { get; set; }

    }

    public static class MappProjectEntity
    {
        public static ProjectEntity MappToEntity(this ProjectPostModel model)
        {
            ProjectEntity projectEntity = new ProjectEntity();

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
            projectEntity.SupervisorGuid= model.SupervisorGuid;
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
