using Excellerent.ReportManagement.Core.Entities.Configs;
using Excellerent.ReportManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Extensions
{
    public static class TimesheetNotificationConfigExtensions
    {
        public static async Task<DateTime>
            GetImpemetedAt(
                this TimesheetNotificationConfig config,
                DateTime refDateTime)
        {
            return config.EscalateType == EscalateType.Reminder ? config.RemindAt
                : config.EscalateType == EscalateType.First ? config.FirstEscalationAt
                : config.SecondEscaletionAt;
        }

        public static DateTime
            GetNearFutureImpemetedAt(
                this TimesheetNotificationConfig config,
                DateTime refDateTime)
        {
            return (new List<DateTime?>()
            {
                config.RemindAt >= refDateTime ? config.RemindAt : null,
                config.FirstEscalationAt >= refDateTime ? config.FirstEscalationAt : null,
                config.SecondEscaletionAt >= refDateTime ? config.SecondEscaletionAt : null,
            })
            .Where(dt => dt != null)
            .Select(dt => (DateTime)dt)
            .OrderBy(dt => dt).First();
        }
    }
}
