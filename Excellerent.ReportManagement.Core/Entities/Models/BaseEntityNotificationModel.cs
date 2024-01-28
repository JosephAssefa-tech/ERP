using Excellerent.ReportManagement.Core.Interfaces.IHelpers;
using System;
using System.Collections.Generic;

namespace Excellerent.ReportManagement.Core.Entities.Models
{
    public abstract class BaseEntityNotificationModel<T> : ICount
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; }
        public IEnumerable<T> Lists { get; set; }

        public abstract int Count();
    }
}
