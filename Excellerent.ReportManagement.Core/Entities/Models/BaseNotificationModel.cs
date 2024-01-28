using System;

#nullable disable

namespace Excellerent.ReportManagement.Core.Entities.Models
{
    public abstract class BaseNotificationModel
    {
        public Guid? Guid { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }
}
