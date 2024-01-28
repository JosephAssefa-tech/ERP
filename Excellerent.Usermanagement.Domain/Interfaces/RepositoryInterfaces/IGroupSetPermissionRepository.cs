
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces
{
    public interface IGroupSetPermissionRepository : IAsyncRepository<GroupSetPermission>
    {
        Task<IEnumerable<Permission>> GetPermissionByGroupId(Guid guid);

        Task<bool> RemovePermissionsByGroup(Guid guid);
    }
}  
