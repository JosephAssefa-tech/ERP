
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;

namespace Excellerent.Usermanagement.Domain.Entities
{
    public class GroupSetEntity : BaseEntity<GroupSet>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public GroupSetEntity()  {}
        public GroupSetEntity(GroupSet groupSet): base(groupSet)
        {
            Guid = groupSet.Guid;
            Name = groupSet.Name;
            Description = groupSet.Description;
            CreatedDate = groupSet.CreatedDate;
        }
        public override GroupSet MapToModel()
        {
            GroupSet groupSet = new GroupSet();
            groupSet.Guid = Guid.NewGuid();
            groupSet.Name = Name;
            groupSet.Description = Description;
            return groupSet;
        }

        public override GroupSet MapToModel(GroupSet t)
        {
            GroupSet groupSetMapped = t;
            groupSetMapped.Guid = Guid;
            groupSetMapped.Name = Name;
            groupSetMapped.Description = Description;
            return groupSetMapped;
        }
    }
       
}
