using Excellerent.SharedModules.Seed;

namespace Excellerent.ApplicantTracking.Domain.Models
{
    public class JobRequirment: BaseAuditModel
    {
        public string JobDescriptionName { get; set; }
        public string JobBrief { get; set; }
        public string JobResponsiblity { get; set; }
    }
}
