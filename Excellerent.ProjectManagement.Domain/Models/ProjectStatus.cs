using Excellerent.SharedModules.Seed;


namespace Excellerent.ProjectManagement.Domain.Models
{

    public class ProjectStatus : BaseAuditModel
    {
        public string StatusName { get; set; }
        public bool AllowResource { get; set; }

    }





}
