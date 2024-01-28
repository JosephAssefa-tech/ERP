using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Dtos
{
    public enum DeadlineWeek
    {
        current_week,
        next_week,
    }

    public class ConfigurationDto
    {
        public List<StartOfWeek> StartOfWeeks { get; set; } = new List<StartOfWeek>() { new StartOfWeek() };
        public List<string> WorkingDays { get; set; } = new List<string>() {
            DayOfWeek.Monday.ToString(),
            DayOfWeek.Tuesday.ToString(),
            DayOfWeek.Wednesday.ToString(),
            DayOfWeek.Thursday.ToString(),
            DayOfWeek.Friday.ToString(),
            DayOfWeek.Saturday.ToString(),
            DayOfWeek.Sunday.ToString()
        };
        public WorkingHours WorkingHours { get; set; } = new WorkingHours();
        public TimesheetEscalation TimesheetEscalation { get; set; } = new TimesheetEscalation();

        public TimesheetDeadline TimesheetDeadline { get; set; } = new TimesheetDeadline();
    }

    public class WorkingHours
    {
        public int Min { get; set; } = 8;
        public int Max { get; set; } = 24;
    }

    public class StartOfWeek
    {
        public DayOfWeek DayOfWeek { get; set; } = DayOfWeek.Monday;
        public DateTime EffectiveDate { get; set; } = new DateTime();
    }

    public class TimesheetEscalation
    {
        public int FirstEscalation { get; set; } = 1;
        public int SecondEscalation { get; set; } = 2;
    }
    public class TimesheetDeadline
    {
        public DayOfWeek DeadlineDate { get; set; } = DayOfWeek.Friday;

        public int DeadlineTime { get; set; } = 6;

        public DeadlineWeek Week { get; set; } = DeadlineWeek.current_week;

        public int TimeZone { get; set; }

    }

}
