using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Models
{
   public class UserGroups: BaseAuditModel
    {
        public Guid GroupSetGuid { get; set; } 
        public GroupSet GroupSet { get; set; }
        public Guid UserGuid { get; set; }
        public User User { get; set; }
    }
}
