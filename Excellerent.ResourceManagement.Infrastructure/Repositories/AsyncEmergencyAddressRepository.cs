using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
    public class AsyncEmergencyAddressRepository :
        AsyncRepository<EmergencyAddress>,
        IAsyncEmergencyAddressRepository
    {
        public AsyncEmergencyAddressRepository(EPPContext context) : base(context)
        {

        }
    }
}
