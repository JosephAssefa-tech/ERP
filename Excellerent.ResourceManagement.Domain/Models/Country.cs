using Excellerent.SharedModules.Seed;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class Country : BaseAuditModel
    {
        public string Name { get; set; }

        public string Nationality { get; set; }
    }
}
