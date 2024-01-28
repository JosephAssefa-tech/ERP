using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
namespace Excellerent.ProjectManagement.Domain.Entities
{
    public class ClientEntity : BaseEntity<Client>
    {

        public string ClientName { get; set; }
        public string ClientStatus { get; set; }
        public string ManagerAssigned { get; set; }
        public string Description { get; set; }
        public ClientEntity()
        {

        }
        public ClientEntity(Client client) : base(client)
        {
            ClientName = client.ClientName;
            ClientStatus = client.ClientStatus;
            ClientName = client.ClientName;
            ClientStatus = client.ClientStatus;
        }

        public override Client MapToModel()
        {
            Client client = new Client();
            client.ClientName = ClientName;
            client.ClientStatus = ClientStatus;
            client.ClientName = ClientName;
            client.ClientStatus = ClientStatus;
            return client;

        }

        public override Client MapToModel(Client s)
        {
            Client client = s;
            client.ClientName = ClientName;
            client.ClientStatus = ClientStatus;
            client.ClientName = ClientName;
            client.ClientStatus = ClientStatus;
            return client;
        }
    }
}
