using Excellerent.ReportManagement.Core.Enums;
using System;

namespace Excellerent.ReportManagement.Core.Entities.Configs
{
    public class EachTimesheetNotificationConfig
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime RemindAt { get; set; }
        public DateTime FirstEscalationAt { get; set; }
        public DateTime SecondEscaletionAt { get; set; }
        public EscalateType EscalateType { get; set; }
        public int MinimumHoursRequired { get; set; }
        public bool Expired { get; set; }
        public string ResponseLink { get; set; }

        public EachTimesheetNotificationConfig(
            TimesheetNotificationConfig config,
            bool isResource)
        {
            Start = config.Start;
            End = config.End;
            RemindAt = config.RemindAt;
            FirstEscalationAt = config.FirstEscalationAt;
            SecondEscaletionAt = config.SecondEscaletionAt;
            EscalateType = config.EscalateType;
            MinimumHoursRequired = config.MinimumHoursRequired;
            Expired = config.Expired;
            ResponseLink = isResource ? config.ResourceLink : config.ManagerLink;
        }
    }
}
