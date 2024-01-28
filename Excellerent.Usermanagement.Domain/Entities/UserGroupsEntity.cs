using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;

namespace Excellerent.Usermanagement.Domain.Entities
{
    public class UserGroupsEntity : BaseEntity<UserGroups>
    {
        public Guid GroupSetGuid { get; set; }
        public Guid UserGuid { get; set; }
        public UserGroupsEntity()
        {

        }
        public UserGroupsEntity(UserGroups userGroups) : base(userGroups)
        {
            Guid = userGroups.Guid;
            GroupSetGuid = userGroups.GroupSetGuid;
            UserGuid = userGroups.UserGuid;
        }
        public override UserGroups MapToModel()
        {
            UserGroups userGroup = new UserGroups();
            userGroup.Guid = Guid.NewGuid();
            userGroup.GroupSetGuid = GroupSetGuid;
            userGroup.UserGuid = UserGuid;
            return userGroup;
        }

        public override UserGroups MapToModel(UserGroups t)
        {
            UserGroups userGroup = new UserGroups();
            userGroup.Guid = Guid;
            userGroup.GroupSetGuid = GroupSetGuid;
            userGroup.UserGuid = UserGuid;
            return userGroup;
        }
    }
}
