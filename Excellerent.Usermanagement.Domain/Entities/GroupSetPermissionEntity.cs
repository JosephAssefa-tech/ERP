
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;

namespace Excellerent.Usermanagement.Domain.Entities
{
    public class GroupSetPermissionEntity : BaseEntity<GroupSetPermission>
    {
        public Guid GroupSetId { get; set; }
        public Guid PermissionId { get; set; }
        public GroupSetPermissionEntity() 
        {

        }
        public GroupSetPermissionEntity(GroupSetPermission groupSetPermission) : base(groupSetPermission)
        {
            Guid = groupSetPermission.Guid;
            GroupSetId = groupSetPermission.GroupSetId;
            PermissionId = groupSetPermission.PermissionId;
        }
        public override GroupSetPermission MapToModel()
        {
            GroupSetPermission groupSetPermission = new GroupSetPermission();
            groupSetPermission.Guid = Guid.NewGuid();
            groupSetPermission.GroupSetId = GroupSetId;
            groupSetPermission.PermissionId = PermissionId;
            return groupSetPermission;
        }

        public override GroupSetPermission MapToModel(GroupSetPermission t)
        {
            GroupSetPermission groupSetPermission = new GroupSetPermission();
            groupSetPermission.Guid = Guid;
            groupSetPermission.GroupSetId = GroupSetId;
            groupSetPermission.PermissionId = PermissionId;
            return groupSetPermission;
        }
    }
}

    