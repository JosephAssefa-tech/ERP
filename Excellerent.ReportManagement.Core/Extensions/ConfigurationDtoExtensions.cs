using Excellerent.ReportManagement.Core.Entities.Configs;
using Excellerent.ReportManagement.Core.Entities.Helpers;
using Excellerent.Timesheet.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Extensions
{
    public static class ConfigurationDtoExtensions
    {
        public static async Task<ModifiedConfigurationDto>
            GetModifiedConfigurationDto(this ConfigurationDto config)
        {
            WeekDays weekDays = await config.GetWeekDays();
            int remiderOffset = await weekDays.GetWorkingDaysCount(
                config.TimesheetDeadline.DeadlineDate,
                config.TimesheetDeadline.Week == DeadlineWeek.current_week
                );
            return new ModifiedConfigurationDto(
                weekDays: weekDays,
                reminderOffset: await weekDays.AddWorkingDays(remiderOffset),
                firstOffset: await weekDays.AddWorkingDays(remiderOffset + config.TimesheetEscalation.FirstEscalation),
                secondOffset: await weekDays.AddWorkingDays(remiderOffset + config.TimesheetEscalation.SecondEscalation),
                time: TimeSpan.FromHours(config.TimesheetDeadline.DeadlineTime),
                minimumHoursRequired: config.WorkingHours.Min * await weekDays.NoofWorkingDays(),
                timeZone: TimeSpan.FromHours(config.TimesheetDeadline.TimeZone)
                );
        }

        public static async Task<WeekDays> GetWeekDays(this ConfigurationDto config)
        {
            var starts = config.StartOfWeeks
                .Where(s => s.EffectiveDate <= DateTime.Now.FromHostTime(config.TimesheetDeadline.TimeZone));
            var start = !starts.Any() ? DayOfWeek.Monday : starts.First().DayOfWeek;
            var workingDays = config.WorkingDays.Select(d => Enum.Parse<DayOfWeek>(d));
            return new WeekDays(start, await GetWeekDays(start, workingDays));
        }

        private static async Task<IEnumerable<WeekDay>>
            GetWeekDays(DayOfWeek startofTheWeek, IEnumerable<DayOfWeek> dayOfWeeks)
        {
            var weekDays = new List<WeekDay>();
            int j = 0;
            for (int i = 0; i < 7; i++)
            {
                DayOfWeek day = (DayOfWeek)((i + (int)startofTheWeek) % 7);
                bool isWorking = dayOfWeeks.Where(d => d == day).Any();
                weekDays.Add(
                    new WeekDay(
                        day,
                        i,
                        isWorking ? j++ : -1
                    ));
            }
            return weekDays;
        }
    }
}
