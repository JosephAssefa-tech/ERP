using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Services
{
    public class ClientService : CRUD<ClientEntity, Client>, IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository) : base(clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<ClientEntity>> GetClientByClientName(string clientName)
        {
            var data = _clientRepository.GetClientByClientName(clientName);
            return (await data).Select(x => new ClientEntity(x));

        }
        public async Task<Client> GetClientByGuid(Guid guid)
        {
            return await _clientRepository.FindOneAsync(x => x.Guid == guid);
        }
    }
}
