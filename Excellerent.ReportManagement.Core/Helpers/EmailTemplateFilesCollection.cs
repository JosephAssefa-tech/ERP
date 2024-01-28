using Excellerent.ReportManagement.Core.Resource;
using System;
using System.Collections.Generic;

namespace Excellerent.ReportManagement.Core.Helpers
{
    public class EmailTemplateFilesCollection
    {
        private static readonly List<String> EmailTemplateResourceKeys = new List<string>()
        {
            "ReminderResourceNotification",
            "FirstEscalationSupervisorNotification",
            "FirstEscalationResourceNotification",
            "SecondEscalationManagementNotification",
            "SecondEscalationResourceNotification",
            "WrapperGeneral"
        };

        public static string ReminderNotification { get => GetEmailTemplateFromResource(0); }
        public static string FirstEscalationNotification { get => GetEmailTemplateFromResource(1); }
        public static string FirstEscalationResourceNotification { get => GetEmailTemplateFromResource(2); }
        public static string SecondEscalationNotification { get => GetEmailTemplateFromResource(3); }
        public static string SecondEscalationResourceNotification { get => GetEmailTemplateFromResource(4); }

        private static string GetEmailTemplateFromResource(int index)
        {
            string key = EmailTemplateResourceKeys[index];

            return NotificationEmailTemplateResourceReader.GetResourceData(key).Result;
        }
    }
}
