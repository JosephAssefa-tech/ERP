

using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Services
{
    public class GroupSetPermissionService : CRUD<GroupSetPermissionEntity, GroupSetPermission>, IGroupSetPermissionService
    {
        private readonly IGroupSetPermissionRepository _groupSetPermissionRepository;
        public GroupSetPermissionService(IGroupSetPermissionRepository groupSetPermissionRepository) : base(groupSetPermissionRepository)
        {
            _groupSetPermissionRepository = groupSetPermissionRepository;
        }

        public async Task<IEnumerable<PermissionEntity>> GetPermissionsByGroupId(Guid guid)
        {
            var data = await _groupSetPermissionRepository.GetPermissionByGroupId(guid);
            return data?.Select(x => new PermissionEntity(x));
        }
        public async Task<ResponseDTO> Delete(Guid id)
        {
            try
            {
                var data =  _groupSetPermissionRepository.FindOneAsyncForDelete(x => x.Guid == id);
                if (data == null)
                {
                    return new ResponseDTO(ResponseStatus.Error, "Assigned permission not found", null);
                }
                await _groupSetPermissionRepository.DeleteAsync(data.Result);
            } catch (Exception ex) { string err = ex.Message; }
            return new ResponseDTO(ResponseStatus.Success, "Record deleted successfully", null);
        }

        public async Task<bool> removePermissions(Guid guid)
        {
           return await _groupSetPermissionRepository.RemovePermissionsByGroup(guid);           
        }
    }
}
