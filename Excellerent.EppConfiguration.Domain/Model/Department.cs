using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Model
{
    public class Department : BaseAuditModel
    {
        public string Name { get; set; }
        public Department()
        {
            Name = string.Empty;
        }

        public Department(Department department)
        {
            this.Name = department.Name;
        }
    }
}
