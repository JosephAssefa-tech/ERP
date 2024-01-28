using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Infrastructure.Repositories
{
   public class UserGroupsRepository : AsyncRepository<UserGroups>, IUserGroupsRepository
    {
        private readonly EPPContext _context;

        public UserGroupsRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupSet>> GetUserByGroupId(Guid guid) 
        {
            return await _context.UserGroups.Include(x => x.GroupSet)
                                .Where(entry => entry.GroupSetGuid == guid)
                                .Select(entry => entry.GroupSet).ToListAsync();
        }

        public async Task<IEnumerable<GroupSet>> GetGroupSetByUserId(Guid guid)
        {
            return await _context.UserGroups.Include(x => x.GroupSet)
                                .Where(entry => entry.UserGuid == guid)
                                .Select(entry => entry.GroupSet).ToListAsync();
        }
        public async Task<bool> Put(Guid userId, Guid [] groupIds)
        {
            var models = _context.UserGroups
                .Where(u => u.UserGuid == userId);
            //_context.Attach(models);
            var previousUserGroups = await models.ToListAsync();
            
            var currentUserGroups = new List<UserGroups>();

            List<UserGroups> createUserGroups = new List<UserGroups>();
            List<UserGroups> deleteUserGroups = new List<UserGroups>();

            foreach (Guid groupId in groupIds)
            {
                var contains = previousUserGroups.FirstOrDefault(x => x.GroupSetGuid == groupId);
                if (contains == null)
                    _context.UserGroups.Add(new UserGroups() { UserGuid = userId, GroupSetGuid = groupId });
            }

            foreach (UserGroups prevGroup in previousUserGroups)
            {
                var contains = groupIds.Contains(prevGroup.GroupSetGuid);
                if (!contains)
                {
                    _context.UserGroups.Remove(prevGroup);
                }
            }
            
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> RemoveGroupUser(UserGroups groupSetUsers)
        {
            _context.UserGroups.Remove(groupSetUsers);
            return (await _context.SaveChangesAsync() > 0);
        }
        public async Task<UserGroups> GetGroupSetUser(Guid userGroupGuid)
        {
            return await _context.UserGroups.Where(u => u.Guid == userGroupGuid).FirstOrDefaultAsync();
        }

        public async Task<bool> AddUsersToGroup(string[] userGuidCollection, Guid groupGuid)
        {
            List<UserGroups> userGroupList = new List<UserGroups>();
            foreach (var userGuid in userGuidCollection)
            {
                UserGroups userGroup = new UserGroups();
                userGroup.UserGuid = Guid.Parse(userGuid);
                userGroup.GroupSetGuid = groupGuid;
                userGroupList.Add(userGroup);
                await _context.UserGroups.AddAsync(userGroup);
            }
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<List<GroupUsersViewModel>> GetAllGroupUsersAsync(Guid groupId, int pageIndex, int pageSize)
        {
            var groupUserList = await _context.UserGroups.Include(u => u.User).Where(g => g.GroupSetGuid == groupId)
                                                        .OrderByDescending(o => o.CreatedDate).ToListAsync();
            var paginatedGroupUserList = groupUserList.Skip(pageIndex * pageSize).Take(pageSize);
            List<GroupUsersViewModel> groupUsersViewModelList = new List<GroupUsersViewModel>();
            StringBuilder builder = new StringBuilder();
            foreach (UserGroups userGroup in paginatedGroupUserList)
            {
                /* builder.Clear();
                builder.Append(userGroup.User.FirstName);
                builder.Append(" ");
                builder.Append(userGroup.User.MiddleName);
                builder.Append(" ");
                builder.Append(userGroup.User.LastName);
                builder.Append(" "); */
                groupUsersViewModelList.Add(new GroupUsersViewModel()
                {
                    Guid = userGroup.Guid,
                    FullName = userGroup.User.FullName // builder.ToString()
                }); ;
            }

            return groupUsersViewModelList;
        }

        public async Task<int> GetAllGroupUsersCountAsync(Guid groupId)
        {
            return await _context.UserGroups.Include(u => u.User).Where(g => g.GroupSetGuid == groupId).OrderByDescending(o => o.CreatedDate).CountAsync();
        }
    }
}
