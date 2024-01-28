
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces
{
    public interface IPermissionService : ICRUD<PermissionEntity, Permission>
    {
        Task<IEnumerable<PermissionEntity>> GetAllPermissions(); 
         Task<ResponseDTO> GetZeroLevelssions();
        Task<ResponseDTO> GetModulePermission();
        Task<ResponseDTO> GetPermissionCategoryByGroupId(Guid guid);
        Task<Boolean> CheckUserPermmsion(string email, string permissonKeyValue);
    }
}
