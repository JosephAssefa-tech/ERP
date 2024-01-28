using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.DTOs
{
    public class EmployeeCounts
    {
        public int billable { get; set; }
        public int nonBillable {get;set;} 
        public int internalEmp { get; set; }
        public int benchEmp { get; set; }
    }
}
