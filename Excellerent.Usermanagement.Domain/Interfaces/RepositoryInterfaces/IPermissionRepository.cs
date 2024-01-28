
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPermissionRepository : IAsyncRepository<Permission>
    {
        Task<IEnumerable<Permission>> GetAllPermission();
    }
}
