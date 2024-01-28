using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Utilities
{
    public static class DateTimeUtility
    {
        public static readonly List<string> Month = new List<string>()
        {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };
        public static DateTime GetWeeksFirstDate(DateTime date)
        {
            DateTime firstDate = date.Date;

            firstDate = firstDate.DayOfWeek == DayOfWeek.Sunday ? firstDate.AddDays((-1 * 7) + 1) : firstDate.AddDays((-1 * (int)firstDate.DayOfWeek) + 1);

            return firstDate;
        }

        public static DateTime GetWeeksLastDate(DateTime date)
        {
            DateTime lastDate = GetWeeksFirstDate(date).AddDays(6);

            return lastDate;
        }
    }
}
