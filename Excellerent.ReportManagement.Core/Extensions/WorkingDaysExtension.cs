using Excellerent.ReportManagement.Core.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Extensions
{
    public static class WorkingDaysExtension
    {
        public static async Task<int>
            NoofWorkingDays(this WeekDays weekDays) =>
                weekDays.DayOfWeeks.Where(d => d.IsWorking).Count();

        public static async Task<IEnumerable<DayOfWeek>>
            WorkingDays(this WeekDays weekDays) =>
                weekDays.DayOfWeeks.Where(d => d.IsWorking).Select(d => d.Day);

        public static async Task<int> GetWorkingDaysCount(this WeekDays weekDays,
            DayOfWeek day, bool isCurrentWeek)
        {
            int noofWorkingDays = await weekDays.NoofWorkingDays();
            if (isCurrentWeek) return noofWorkingDays - 1;
            var weekDay = weekDays.DayOfWeeks.Where(d => d.Day == day).First();
            return noofWorkingDays +
                    weekDays.DayOfWeeks
                        .Take(weekDays.DayOfWeeks.ToList().IndexOf(weekDay) + 1)
                        .Where(d => d.IsWorking).Count() - 1;
        }



        public static async Task<DayOfWeek> GetLastWorkingDayoftheWeek(this WeekDays weekDays) =>
            weekDays.DayOfWeeks
                    .Where(d => d.IsWorking)
                    .Last().Day;

        public static async Task<int> AddWorkingDays(this WeekDays weekDays, int noofWorkingDays)
        {
            int noofDays = 7 * (noofWorkingDays / await weekDays.NoofWorkingDays());
            int i = noofWorkingDays % await weekDays.NoofWorkingDays();
            foreach (var day in weekDays.DayOfWeeks)
            {
                if (i == 0) break;
                noofDays++;
                if (day.IsWorking) i--;
            }
            return noofDays + (int)weekDays.StartofTheWeek - (int)weekDays.RefStartoftheWeek;
        }
    }
}
