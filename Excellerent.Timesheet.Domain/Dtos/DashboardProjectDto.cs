using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Dtos
{
    public  class DashboardProjectDto
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string SupervisorName { get; set; }
        public int BillableHrs { get; set; }
        public int NonBillableHrs { get; set; }
        public DateTime StartDate { get; set; }
        public string? Description { get; set; }    
        public DateTime? EndDate { get; set; }

    }
}
