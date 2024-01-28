using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface
{
    public interface IClientStatusRepository : IAsyncRepository<ClientStatus>
    {
        Task<ClientStatus> GetClientStatusById(Guid statusId);
    }
}