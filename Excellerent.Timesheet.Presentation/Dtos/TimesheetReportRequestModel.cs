using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Presentation.Dtos
{
   public class TimesheetReportFilterModel
    {
       public Guid? ClientId { get; set; }
       public Guid? [] ProjectIds { get; set; }
       public DateTime DateFrom { get; set; }
       public DateTime DateTo { get; set; }
    }
}
