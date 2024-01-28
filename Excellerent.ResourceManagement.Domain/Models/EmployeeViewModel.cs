using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class EmployeeViewModel
    {
        public Guid EmployeeGUid { get; set; }

        public string FullName { get; set; }

        public string JobTitle { get; set; }

        public string Location { get; set; }

        public DateTime JoiningDate { get; set; }

        public string Status { get; set; }

        public string? OrganizationEmail { get; set; }
    }
}
