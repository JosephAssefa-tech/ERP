using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces
{
   public interface IUserGroupsRepository : IAsyncRepository<UserGroups>
    { 
        Task<IEnumerable<GroupSet>> GetUserByGroupId(Guid guid);

        Task<IEnumerable<GroupSet>> GetGroupSetByUserId(Guid guid);
        Task<bool> Put(Guid userId, Guid[] groupIds);

        Task<UserGroups> GetGroupSetUser(Guid userGroupGuid);

        Task<bool> RemoveGroupUser(UserGroups groupSetUsers);

        Task<bool> AddUsersToGroup(string[] userGuidCollection, Guid groupGuid);

        Task<List<GroupUsersViewModel>> GetAllGroupUsersAsync(Guid groupId, int pageIndex, int pageSize);

        Task<int> GetAllGroupUsersCountAsync(Guid groupId);
    }
}
