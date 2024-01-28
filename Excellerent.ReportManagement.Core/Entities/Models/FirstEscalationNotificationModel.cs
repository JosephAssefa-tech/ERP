using System.Collections.Generic;

namespace Excellerent.ReportManagement.Core.Entities.Models
{
    public class FirstEscalationNotificationModel : BaseNotificationModel
    {
        public IEnumerable<string> Employees { get; set; }
    }
}
