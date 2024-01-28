using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Models
{
    public class GroupSetUser
    {
        public Guid GroupSetsGuid { get; set; }

        public Guid UserGuid { get; set; }

        public GroupSet GroupSet { get; set; }

        public User User { get; set; }
    }
}
