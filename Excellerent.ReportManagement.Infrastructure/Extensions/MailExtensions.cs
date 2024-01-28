using Excellerent.ReportManagement.Core.Entities.Configs;
using Excellerent.ReportManagement.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Excellerent.ReportManagement.Infrastructure.Extensions
{
    public static class MailExtensions
    {
        private static string FomatDate(DateTime dateTime) => dateTime.ToString("MM/dd/yyyy");

        private static string ReplaceCommonValues(EachTimesheetNotificationConfig config, string fullName, string template)
        {
            return template.Replace("{userName}", fullName)
                .Replace("{Start}", FomatDate(config.Start))
                .Replace("{End}", FomatDate(config.End))
                .Replace("{minWorkingHours}", String.Format("{0:d} Hours", config.MinimumHoursRequired))
                .Replace("{reminderDate}", FomatDate(config.RemindAt))
                .Replace("{firstEscalationDate}", FomatDate(config.FirstEscalationAt))
                .Replace("{secondEscalationDate}", FomatDate(config.FirstEscalationAt))
                .Replace("{responseLink}", config.ResponseLink)
                .Replace("{hidden}", config.Expired ? "hidden" : "");
        }

        public static string ComposeResourceNotificationEmail(
            this TimesheetResourceNotification source,
            string template,
            TimesheetNotificationConfig config)
        {
            return ReplaceCommonValues(
                new EachTimesheetNotificationConfig(config, true),
                        source.FullName, template);
        }

        public static string ComposeSupervisorNotificationEmail(
            this FirstEscalationNotificationModel source,
            string template,
            TimesheetNotificationConfig config)
        {
            return ReplaceCommonValues(
                new EachTimesheetNotificationConfig(config, false),
                        source.FullName, template)
                    .Replace("{employees}",
                        String.Join('\n',
                            source.Employees.Select(r => $"<li>{r}</li>")));
        }

        public static string ComposeSecondEscalationNotificationEmail(
            this EmployeeBrief source,
            string template,
            TimesheetNotificationConfig config,
            IEnumerable<string> resources)
        {
            return ReplaceCommonValues(
                new EachTimesheetNotificationConfig(config, false),
                        source.FullName, template)
                    .Replace("{employees}",
                        String.Join('\n',
                            resources.Select(r => $"<li>{r}</li>")));
        }

        public static MailMessage ComposeEmail<T>(
            this T notification, string body
        ) where T : BaseNotificationModel
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(notification.EmailAddress, $"{notification.FullName}"));
            mail.IsBodyHtml = true;
            mail.Body = body;
            return mail;
        }
    }
}
