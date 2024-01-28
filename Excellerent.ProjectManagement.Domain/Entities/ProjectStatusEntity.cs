using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;


namespace Excellerent.ProjectManagement.Domain.Entities
{
    public class ProjectStatusEntity : BaseEntity<ProjectStatus>
    {

        public string StatusName { get; set; }
        public bool AllowResource { get; set; }
        public ProjectStatusEntity()
        {

        }

        public ProjectStatusEntity(ProjectStatus projectStatus) : base(projectStatus)
        {
            StatusName = projectStatus.StatusName;
            AllowResource = projectStatus.AllowResource;
        }

        public override ProjectStatus MapToModel()
        {
            ProjectStatus status = new ProjectStatus();
            status.StatusName = StatusName;
            status.AllowResource =AllowResource;
            return status;

        }

        public override ProjectStatus MapToModel(ProjectStatus s)
        {
            ProjectStatus status = s;
            status.StatusName = StatusName;
            status.AllowResource = AllowResource;
            return status;
        }
    }
}
