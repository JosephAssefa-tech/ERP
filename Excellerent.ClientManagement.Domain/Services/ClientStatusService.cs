using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Domain.Services
{
    public class ClientStatusService : CRUD<ClientStatusEntity, ClientStatus>, IClientStatusService
    {
        private readonly IClientStatusRepository _clientStatusRepository;

        public ClientStatusService(IClientStatusRepository clientStatusRepository) : base(clientStatusRepository)
        {
            _clientStatusRepository = clientStatusRepository;
        }

        public async Task<ClientStatus> GetClientStatusById(Guid id)
        {
            var status = await _clientStatusRepository.GetClientStatusById(id);

            return status;
        }
    }
}