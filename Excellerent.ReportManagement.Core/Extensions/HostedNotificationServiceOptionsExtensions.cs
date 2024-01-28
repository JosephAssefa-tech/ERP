using Excellerent.ReportManagement.Core.Entities.Options;
using System;

namespace Excellerent.ReportManagement.Core.Extensions
{
    public static class HostedNotificationServiceOptionsExtensions
    {
        public static DateTime ExecuteAt(
            this HostedNotificationServiceOptions source,
            DateTime scheduledAt,
            DateTime? refDateTime = null)
        {
            if (source.IsPeriodic)
            {
                if (refDateTime == null)
                {
                    var dateTime = (DateTime.Now.ToStartofTheDay() + source.StartAt).ToHostTime(source.TimeZone);
                    return dateTime + (dateTime < DateTime.Now ? TimeSpan.FromDays(1) : TimeSpan.Zero);
                }
                else return (DateTime)refDateTime + source.Interval;
            }
            else return refDateTime == null ? DateTime.Now : scheduledAt;
        }

        public static TimeSpan WaitFor(
           this HostedNotificationServiceOptions source,
           DateTime scheduledAt)
        {
            return scheduledAt - DateTime.Now < TimeSpan.FromMinutes(1) ? TimeSpan.Zero : scheduledAt - DateTime.Now;
        }

        public static TimeSpan BatchNotificationSpan(
           this HostedNotificationServiceOptions source)
        {
            return source.IsPeriodic ? source.Interval : TimeSpan.FromHours(1);
        }

        public static TimeSpan ExecuteAfter(
            this HostedNotificationServiceOptions source,
            DateTime scheduledAt,
            DateTime? refDateTime = null)
        {
            var now = DateTime.Now;
            if (source.IsPeriodic)
            {
                if (refDateTime == null)
                {
                    var nowUTC = now.ToUniversalTime();
                    var calcuDT = new DateTime(nowUTC.Year, nowUTC.Month, nowUTC.Day) + source.StartAt;
                    if (calcuDT < nowUTC) calcuDT = calcuDT.AddDays(1);
                    return calcuDT - nowUTC;
                }
                else return (DateTime)refDateTime - now + source.Interval;
            }
            else
            {
                var interval = scheduledAt - now;
                return interval > TimeSpan.Zero ? interval : TimeSpan.Zero;
            }
        }
    }

}

