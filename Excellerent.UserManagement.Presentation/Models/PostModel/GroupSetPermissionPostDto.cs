

using System;

namespace Excellerent.UserManagement.Presentation.Models.PostModel
{
    public class GroupSetPermissionPostDto
    {
        public Guid GroupSetId { get; set; }
        public Guid[] PermissionIDArray { get; set; }
    }
}
