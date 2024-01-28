using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;

namespace Excellerent.ClientManagement.Infrastructure.Repositories
{
    public class BillingAddressRepository : AsyncRepository<BillingAddress>, IBillingAddressRepository
    {
        private readonly EPPContext _context;

        public BillingAddressRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
    }
}