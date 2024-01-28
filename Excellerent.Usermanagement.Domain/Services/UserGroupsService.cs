using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Services
{
   public class UserGroupsService : CRUD<UserGroupsEntity, UserGroups>, IUserGroupsService
    {
        private readonly IUserGroupsRepository _usersGroupRepository;
        public UserGroupsService(IUserGroupsRepository userGroupRepository) : base(userGroupRepository)
        {
            _usersGroupRepository = userGroupRepository;
        }

        public async Task<IEnumerable<GroupSetEntity>> GetUserByGroupId(Guid guid) 
        {
            var data = await _usersGroupRepository.GetUserByGroupId(guid);
            return data?.Select(x => new GroupSetEntity(x));
        }
        public async Task<IEnumerable<GroupSetEntity>> GetGroupSetByUserId(Guid guid)
        {
            var data = await _usersGroupRepository.GetGroupSetByUserId(guid);
            return data?.Select(x => new GroupSetEntity(x));
        } 
        public async Task<IEnumerable<UserGroupsEntity>> GetAllByUserId(Guid guid)
        {
            var data = await _usersGroupRepository.FindAsync(x => x.UserGuid == guid);
            return data?.Select(x => new UserGroupsEntity(x));
        }
        public async Task<ResponseDTO> Delete(Guid id)
        {
            var data = await _usersGroupRepository.FindOneAsync(x => x.Guid == id);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "User Group not found", null);
            }
            await _usersGroupRepository.DeleteAsync(data);
            return new ResponseDTO(ResponseStatus.Success, "Record deleted successfully", null);
        }

        public new async Task<ResponseDTO> Add(UserGroupsEntity entity)
        {
            try
            {
                var model = await _usersGroupRepository.AddAsync(entity.MapToModel());
                return new ResponseDTO(ResponseStatus.Success, "Successfully added", model.Guid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDTO();
            }
        }


        public new async Task<ResponseDTO> Update(UserGroupsEntity entity)
        {
            var data = await _usersGroupRepository.FindOneAsync(x => x.Guid == entity.Guid);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "User not found", null);
            }
            var model = entity.MapToModel(data);
            await _usersGroupRepository.UpdateAsync(model);
            return new ResponseDTO(ResponseStatus.Success, "Record updated successfully", null);
        }
        public async Task<bool> Update(Guid userId, Guid[] groupIds)
        {
            return await _usersGroupRepository.Put(userId, groupIds);
        }

        public async Task<ResponseDTO> RemoveGroupUser(Guid userGroupGuid)
        {
            var data = await _usersGroupRepository.GetGroupSetUser(userGroupGuid);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "No user found in the group", null);
            }
            await _usersGroupRepository.RemoveGroupUser(data);
            return new ResponseDTO(ResponseStatus.Success, "User removed from group successfully", null);
        }

        public async Task<ResponseDTO> GetGroupSetUser(Guid userGroupGuid)
        {
            var data = await _usersGroupRepository.GetGroupSetUser(userGroupGuid);
            return new ResponseDTO(ResponseStatus.Success, "User group is found", data);
        }

        public async Task<ResponseDTO> AddUsersToGroup(string[] userGuidCollection, Guid groupGuid)
        {
            try
            {
                await _usersGroupRepository.AddUsersToGroup(userGuidCollection, groupGuid);
                return new ResponseDTO(ResponseStatus.Success, "Users are added to a group successfully", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDTO();
            }
        }

        public async Task<PredicatedResponseDTO> GetAllGroupUsersAsync(Guid groupId, int pageIndex, int pageSize)
        {
            var result = await _usersGroupRepository.GetAllGroupUsersAsync(groupId, pageIndex - 1, pageSize);

            if (result != null)
            {
                List<GroupUsersViewModel> groupList = result;
                int totalRowCount = await _usersGroupRepository.GetAllGroupUsersCountAsync(groupId);
                return new PredicatedResponseDTO
                {
                    Data = groupList,
                    TotalRecord = totalRowCount,//total row count
                    PageIndex = pageIndex,
                    PageSize = pageSize,  // itemPerPage,
                    TotalPage = groupList.Count
                };
            }
            else
            {
                return new PredicatedResponseDTO
                {
                    Data = null,
                    TotalRecord = 0,//total row count
                    PageIndex = 0,
                    PageSize = 0,  // itemPerPage,
                    TotalPage = 0
                };
            }
        }
    }
}
