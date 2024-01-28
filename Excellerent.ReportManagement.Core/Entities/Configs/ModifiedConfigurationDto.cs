using Excellerent.ReportManagement.Core.Entities.Helpers;
using System;
using System.Collections.Generic;

namespace Excellerent.ReportManagement.Core.Entities.Configs
{
    public class ModifiedConfigurationDto
    {
        public ModifiedConfigurationDto()
        {

        }

        public ModifiedConfigurationDto(
            WeekDays weekDays,
            int reminderOffset,
            int firstOffset,
            int secondOffset,
            TimeSpan time,
            int minimumHoursRequired,
            TimeSpan timeZone)
        {
            WeekDays = weekDays;
            ReminderOffset = reminderOffset;
            FirstOffset = firstOffset;
            SecondOffset = secondOffset;
            Time = time;
            MinimumHoursRequired = minimumHoursRequired;
            TimeZone = timeZone;
        }

        public WeekDays WeekDays { get; set; }
        public IEnumerable<DayOfWeek> WorkingDays { get; set; }
        public int ReminderOffset { get; set; }
        public int FirstOffset { get; set; }
        public int SecondOffset { get; set; }
        public TimeSpan Time { get; set; }
        public int MinimumHoursRequired { get; set; }
        public TimeSpan TimeZone { get; set; }

    }
}
