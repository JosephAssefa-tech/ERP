using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;

namespace Excellerent.ClientManagement.Infrastructure.Repositories
{
    public class ClientContactRepository : AsyncRepository<ClientContact>, IClientContactRepository
    {
        private readonly EPPContext _context;

        public ClientContactRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
    }
}