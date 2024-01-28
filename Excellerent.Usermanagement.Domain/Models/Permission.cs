using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Models
{
   public class Permission : BaseAuditModel
    {
        public string PermissionCode { get; set; }
        public string Name { get; set; }
        public string KeyValue { get; set; }
        public string Level { get; set; }
        public string ParentCode { get; set; }
    }
}
