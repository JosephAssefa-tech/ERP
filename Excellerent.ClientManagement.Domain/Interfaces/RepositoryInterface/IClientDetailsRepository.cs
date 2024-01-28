using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface
{
    public interface IClientDetailsRepository : IAsyncRepository<ClientDetails>
    {
        Task<IEnumerable<ClientDetails>> GetClientByName(string clientName);

        Task<ClientDetails> GetClientById(Guid id);
        Task<ResponseDTO> GetProjectByClientId(Guid id); 
        Task<IEnumerable<ClientDetails>> GetClientFullData();

        Task<IEnumerable<ClientDetails>> GetPaginatedClient(Expression<Func<ClientDetails, Boolean>> predicate, int pageIndex, int pageSize);
        ClientDetails UpdateClient(ClientDetails client);
    }
}