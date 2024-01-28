using System;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Interfaces.IServices
{
    public interface ITimesheetNotificationService
    {
        Task<DateTime> Notify(DateTime refDateTime, TimeSpan refInterval);
    }
}
