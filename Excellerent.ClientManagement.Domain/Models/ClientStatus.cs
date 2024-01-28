using Excellerent.SharedModules.Seed;

namespace Excellerent.ClientManagement.Domain.Models
{
    public class ClientStatus : BaseAuditModel
    {
        public string StatusName { get; set; }
    }
}