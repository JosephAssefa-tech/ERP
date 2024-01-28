using System;
using System.Collections.Generic;

namespace Excellerent.ReportManagement.Core.Entities.Helpers
{
    public class WeekDays
    {
        public IEnumerable<WeekDay> DayOfWeeks { get; }
        public DayOfWeek StartofTheWeek { get; }
        public DayOfWeek RefStartoftheWeek { get; }

        public WeekDays(
            DayOfWeek startofTheWeek,
            IEnumerable<WeekDay> daysofWeek,
            DayOfWeek refStartoftheWeek = DayOfWeek.Sunday)
        {
            DayOfWeeks = daysofWeek;
            StartofTheWeek = startofTheWeek;
            RefStartoftheWeek = refStartoftheWeek;
        }
    }
}
