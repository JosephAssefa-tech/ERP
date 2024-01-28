
using System;

namespace Excellerent.UserManagement.Presentation.Models.GetModels
{
    public class PermissionGetDto
    {
        public Guid Guid { get; set; }
        public string PermissionCode { get; set; }
        public string Name { get; set; }
        public string KeyValue { get; set; }
        public string Level { get; set; }
        public string ParentCode { get; set; }
    }
}
