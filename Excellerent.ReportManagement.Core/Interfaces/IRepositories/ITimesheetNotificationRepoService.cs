using Excellerent.ReportManagement.Core.Entities.Configs;
using System;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Interfaces.IRepositories
{
    public interface ITimesheetNotificationRepoService
    {
        Task<bool> Notify(
            TimesheetNotificationConfig reminderConfig,
            TimesheetNotificationConfig firstEscalationConfig,
            TimesheetNotificationConfig secondEscalationConfig);

        Task<bool> SendReminderNotifications(TimesheetNotificationConfig config);

        Task<bool> SendFirstEscalationNotifications(TimesheetNotificationConfig config);

        Task<bool> SendSecondEscalationNotifications(TimesheetNotificationConfig config);

    }
}
