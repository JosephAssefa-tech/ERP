using Excellerent.SharedModules.Seed;

namespace Excellerent.Usermanagement.Domain.Models
{
    public class GroupSet : BaseAuditModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
