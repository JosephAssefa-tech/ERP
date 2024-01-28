using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ClientManagement.Domain.Entities
{
    public class ClientStatusEntity : BaseEntity<ClientStatus>
    {
        public string StatusName { get; set; }

        public ClientStatusEntity()
        {
        }

        public ClientStatusEntity(ClientStatus clientStatus) : base(clientStatus)
        {
            StatusName = clientStatus.StatusName;
        }

        public override ClientStatus MapToModel()
        {
            ClientStatus status = new ClientStatus();
            status.StatusName = StatusName;
            return status;
        }

        public override ClientStatus MapToModel(ClientStatus cs)
        {
            ClientStatus status = cs;
            status.StatusName = StatusName;
            return status;
        }
    }
}