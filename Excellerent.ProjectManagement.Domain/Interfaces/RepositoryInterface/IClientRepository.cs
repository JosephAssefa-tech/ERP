using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface
{
    public interface IClientRepository : IAsyncRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientByClientName(string clientName);
    }
}
