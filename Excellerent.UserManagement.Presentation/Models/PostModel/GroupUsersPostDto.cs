using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Models.PostModel
{
    public class GroupUsersPostDto
    {
        public string GroupGuid { get; set; }

        public string[] UserGuidCollection { get; set; }
    }
}
