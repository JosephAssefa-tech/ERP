using System;

namespace Excellerent.ReportManagement.Core.Entities.Helpers
{
    public class WeekDay
    {
        public WeekDay(DayOfWeek day, int index, int order)
        {
            Day = day;
            Index = index;
            Order = order;
        }

        public DayOfWeek Day { get; }
        public int Order { get; }
        public int Index { get; }
        public bool IsWorking { get => Order >= 0; }
    }
}
