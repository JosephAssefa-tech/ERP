using Excellerent.SharedModules.Seed;

namespace Excellerent.ProjectManagement.Domain.Models
{
    public class Client : BaseAuditModel
    {

        public string ClientName { get; set; }
        public string ClientStatus { get; set; }
        public string ManagerAssigned { get; set; }
        public string Description { get; set; }
    }
}
