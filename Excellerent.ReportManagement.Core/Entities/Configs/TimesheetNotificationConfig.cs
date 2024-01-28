using Excellerent.ReportManagement.Core.Enums;
using System;

namespace Excellerent.ReportManagement.Core.Entities.Configs
{
    public class TimesheetNotificationConfig
    {
        public DateTime StartQuery { get; set; }
        public DateTime EndQuery { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime RemindAt { get; set; }
        public DateTime FirstEscalationAt { get; set; }
        public DateTime SecondEscaletionAt { get; set; }
        public EscalateType EscalateType { get; set; }
        public int MinimumHoursRequired { get; set; }
        public bool Expired { get => DateTime.Now - Start >= TimeSpan.FromDays(28); }
        public string ResourceLink { get; set; }
        public string ManagerLink { get; set; }
        public bool Active { get; set; }
    }
}
