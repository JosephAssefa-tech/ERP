using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Infrastructure.Repositories
{
    public class ClientStatusRepository : AsyncRepository<ClientStatus>, IClientStatusRepository
    {
        private readonly EPPContext _context;

        public ClientStatusRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClientStatus> GetClientStatusById(Guid statusId)
        {
            var status = await _context.ClientStatus.FindAsync(statusId);
            return status;
        }
    }
}