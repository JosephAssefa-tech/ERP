using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface
{
    public interface IBillingAddressRepository : IAsyncRepository<BillingAddress>
    {
    }
}