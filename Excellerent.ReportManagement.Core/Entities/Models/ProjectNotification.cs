using System.Linq;

namespace Excellerent.ReportManagement.Core.Entities.Models
{
    public class ProjectNotification : BaseEntityNotificationModel<EmployeeBrief>
    {
        public override int Count()
        {
            return Lists.Count();
        }
    }
}
