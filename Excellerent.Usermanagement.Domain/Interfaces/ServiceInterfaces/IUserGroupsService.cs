using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces
{
    public interface IUserGroupsService : ICRUD<UserGroupsEntity, UserGroups>
    {
        Task<IEnumerable<GroupSetEntity>> GetUserByGroupId(Guid guid); 
        Task<IEnumerable<GroupSetEntity>> GetGroupSetByUserId(Guid guid);
        Task<IEnumerable<UserGroupsEntity>> GetAllByUserId(Guid guid);
        new Task<ResponseDTO> Update(UserGroupsEntity entity);
        Task<ResponseDTO> Delete(Guid id);
        Task<bool> Update(Guid userId, Guid[] groupIds);

        Task<ResponseDTO> GetGroupSetUser(Guid userGroupGuid);

        Task<ResponseDTO> RemoveGroupUser(Guid userGroupGuid);

        Task<ResponseDTO> AddUsersToGroup(string[] userGuidCollection, Guid groupGuid);

        Task<PredicatedResponseDTO> GetAllGroupUsersAsync(Guid groupId, int pageIndex, int pageSize);
    }
}
