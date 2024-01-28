using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;

namespace Excellerent.ClientManagement.Infrastructure.Repositories
{
    public class OperatingAddressRepository : AsyncRepository<OperatingAddress>, IOperatingAddressRepository
    {
        private readonly EPPContext _context;

        public OperatingAddressRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
    }
}