using Excellerent.Usermanagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Models
{
    public class UserListView
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public DateTime LastActivityDate  { get; set; }

        public string Department { get; set; }

        public string  JobTitle { get; set; }

        public string Status { get; set; }
    }
}
