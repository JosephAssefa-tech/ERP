using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Repository
{
    public interface IDeviceDetailsRepository : IAsyncRepository<DeviceDetails>
    {
          Task<IEnumerable<DeviceDetails>> GetWithPredicateAsync(Expression<Func<DeviceDetails, Boolean>> predicate, int pageIndex, int pageSize);
          Task<DeviceDetails> Get(Guid id);
    }
}
