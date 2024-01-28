using System;

namespace Excellerent.ReportManagement.Core.Entities.Options
{
    public class HostedNotificationServiceOptions
    {
        public bool IsPeriodic { get; set; }

        public TimeSpan TimeZone { get; set; }

        public TimeSpan Interval { get; set; }

        public TimeSpan StartAt { get; set; }
    }
}

