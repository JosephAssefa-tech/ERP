

﻿using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.DTOs;
﻿using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Linq;

namespace Excellerent.ProjectManagement.Domain.Entities
{
    public class ProjectEntity : BaseEntity<Project>
    {
    public Guid ProjectGuid { get; set; }
    public string ProjectName { get; set; }
    public ProjectType ProjectType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public EmployeeDTO Supervisor { get; set; }

    public Guid ClientGuid { get; set; }
    public ClientDetails Client { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    public Guid SupervisorGuid { get; set; }
    public Guid ProjectStatusGuid { get; set; }
    public string? Description { get; set; }
     public DateTime? AssignDate { get; set; }
        public ProjectEntity()
        {
            this.IsActive = true;
  

        }



        public ProjectEntity(Project project) : base(project)
        {
            ProjectGuid = project.Guid;
            ProjectName = project.ProjectName;
            ProjectType = project.ProjectType;
            SupervisorGuid = project.SupervisorGuid;
            ClientGuid = project.ClientGuid;
            Client = project.Client;
            ProjectStatusGuid = project.ProjectStatusGuid;
            ProjectStatus = project.ProjectStatus;
            StartDate = project.StartDate;
            EndDate = project.EndDate;
            IsActive = project.IsActive;
            IsDeleted = project.IsDeleted;
            CreatedDate = project.CreatedDate;
            CreatedbyUserGuid = project.CreatedbyUserGuid;
            Description = project.Description;

        }

        public override Project MapToModel()
        {
            Project project = new Project();
            project.Guid = Guid;
            project.ProjectName = ProjectName;
            project.ProjectType = ProjectType;
            project.ProjectStatusGuid = ProjectStatusGuid;
            project.ProjectStatus = ProjectStatus;
            project.SupervisorGuid = SupervisorGuid;
            project.ClientGuid = ClientGuid;
            project.StartDate = StartDate;
            project.EndDate = EndDate;
            project.Description = Description;
  

            return project;
        }

        public override Project MapToModel(Project t)
        {
            Project project = t;
            project.Description = Description;
            project.ProjectName = ProjectName;
            project.ProjectType = ProjectType;
            project.ProjectStatusGuid = ProjectStatusGuid;
            project.ProjectStatus = ProjectStatus;
            project.SupervisorGuid = SupervisorGuid;
            project.ClientGuid = ClientGuid;
            project.Client = Client;
            project.StartDate = StartDate;
            project.EndDate = EndDate;
            project.IsActive = IsActive;
            project.IsDeleted = IsDeleted;
            project.CreatedDate = CreatedDate;
            project.CreatedbyUserGuid = CreatedbyUserGuid;
            return t;
        }
    }
}
