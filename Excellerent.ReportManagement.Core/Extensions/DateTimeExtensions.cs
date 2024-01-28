using System;

namespace Excellerent.ReportManagement.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static readonly TimeSpan HostTimeZone
            = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);

        public static DateTime ToStartofTheDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static DateTime ToStartofTheWeek(this DateTime dateTime)
        {
            return dateTime.ToStartofTheDay().AddDays(-1 * (int)dateTime.DayOfWeek);
        }

        public static DateTime ToStartofTheImmidiateWeek(this DateTime dateTime)
        {
            var current = dateTime.ToStartofTheWeek();
            return current + (current < dateTime ? TimeSpan.FromDays(7) : TimeSpan.Zero);
        }

        public static DateTime
            ShiftBack(this DateTime dateTime, int days, TimeSpan time) =>
                dateTime.AddDays(-1 * days) - time;

        public static DateTime
            ShiftForward(this DateTime dateTime, int days, TimeSpan time) =>
                dateTime.AddDays(days) + time;

        public static DateTime ToHostTime(this DateTime dateTime, TimeSpan? timeZone = null)
        {
            return dateTime + HostTimeZone - (timeZone ?? TimeSpan.Zero);
        }

        public static DateTime ToHostTime(this DateTime dateTime, int timeZone)
        {
            return dateTime.ToHostTime(TimeSpan.FromHours(timeZone));
        }

        public static DateTime FromHostTime(this DateTime dateTime, TimeSpan? timeZone = null)
        {
            return dateTime - HostTimeZone + (timeZone ?? TimeSpan.Zero);
        }

        public static DateTime FromHostTime(this DateTime dateTime, int timeZone)
        {
            return dateTime.FromHostTime(TimeSpan.FromHours(timeZone));
        }
    }
}
