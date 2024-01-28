
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.Usermanagement.Domain.Models
{
    public class GroupSetPermission: BaseAuditModel
    {
        public Guid GroupSetId { get; set; }
        public GroupSet GroupSet { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
 