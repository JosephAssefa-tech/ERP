using Excellerent.ReportManagement.Core.Entities.Configs;
using Excellerent.ReportManagement.Core.Entities.Options;
using Excellerent.ReportManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Extensions
{
    public static class ModifiedConfigurationDtoExtensions
    {
        public static async Task<TimesheetNotificationConfig>
            GetTimesheetNotificationConfig(
                this ModifiedConfigurationDto config,
                DateTime refDateTime,
                EscalateType escalateType)
        {
            var start = await config.GetStart(refDateTime, escalateType);
            return new TimesheetNotificationConfig()
            {
                StartQuery = start.AddDays(-1),
                EndQuery = start.AddDays(8),
                Start = start.AddDays((int)config.WeekDays.StartofTheWeek),
                End = start.AddDays((int)await config.WeekDays.GetLastWorkingDayoftheWeek()),
                RemindAt = start.ShiftForward(config.ReminderOffset, config.Time).ToHostTime(config.TimeZone),
                FirstEscalationAt = start.ShiftForward(config.FirstOffset, config.Time).ToHostTime(config.TimeZone),
                SecondEscaletionAt = start.ShiftForward(config.SecondOffset, config.Time).ToHostTime(config.TimeZone),
                EscalateType = escalateType,
                MinimumHoursRequired = config.MinimumHoursRequired
            };
        }

        public static async Task<TimesheetNotificationConfig>
            GetTimesheetNotificationConfig(
                this ModifiedConfigurationDto config,
                DateTime refDateTime,
                EscalateType escalateType,
                ResponseLinks _reponseLinks)
        {
            var notifyConfig = await config.GetTimesheetNotificationConfig(refDateTime, escalateType);
            notifyConfig.ResourceLink = escalateType == EscalateType.Reminder ? _reponseLinks.ReminderResourceLink
                        : escalateType == EscalateType.First ? _reponseLinks.FirstEscalationResourceLink
                        : _reponseLinks.SecondEscalationResourceLink;
            notifyConfig.ManagerLink = escalateType == EscalateType.Reminder ? _reponseLinks.ReminderManagerLink
                        : escalateType == EscalateType.First ? _reponseLinks.FirstEscalationManagerLink
                        : _reponseLinks.SecondEscalationManagerLink;
            return notifyConfig;
        }

        public static async Task<DateTime>
            GetNextSchedule(
                this ModifiedConfigurationDto config,
                DateTime refDateTime,
                TimeSpan refInterval)
        {
            var nextRefDateTime = refDateTime + refInterval;

            return new List<TimesheetNotificationConfig>()
            {
                await config.GetTimesheetNotificationConfig(nextRefDateTime, EscalateType.Reminder),
                await config.GetTimesheetNotificationConfig(nextRefDateTime, EscalateType.First),
                await config.GetTimesheetNotificationConfig(nextRefDateTime,  EscalateType.Second)
            }
            .Select(c => c.GetNearFutureImpemetedAt(refDateTime))
            .OrderBy(dt => dt).First();
        }

        private static async Task<DateTime>
            GetStart(
                this ModifiedConfigurationDto config,
                DateTime refDateTime,
                EscalateType escalateType) =>
                    refDateTime
                        .FromHostTime(config.TimeZone)
                        .ShiftBack(
                            escalateType == EscalateType.Reminder ? config.ReminderOffset
                            : escalateType == EscalateType.First ? config.FirstOffset
                            : config.SecondOffset,
                            config.Time
                        ).ToStartofTheImmidiateWeek();
    }
}
