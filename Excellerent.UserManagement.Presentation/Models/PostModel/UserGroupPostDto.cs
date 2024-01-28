using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Models.PostModel
{
   public class UserGroupPostDto
    {
        public Guid UserGuid { get; set; }
        public Guid[] GroupIDArray { get; set; } 
    }
}
