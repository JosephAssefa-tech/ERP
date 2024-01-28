
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces
{
    public interface IGroupSetPermissionService : ICRUD<GroupSetPermissionEntity, GroupSetPermission>
    {
        Task<IEnumerable<PermissionEntity>> GetPermissionsByGroupId(Guid guid);
        Task<bool> removePermissions(Guid guid);
        Task<ResponseDTO> Delete(Guid id);

    }
}
  