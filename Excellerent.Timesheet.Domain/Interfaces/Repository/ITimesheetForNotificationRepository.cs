using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Repository
{
    public interface ITimesheetForNotificationRepository
    {
        Task<IEnumerable<TimesheetApproval>> GetForNotification(Guid projectGuid, Guid employeeGuid, DateTime start, DateTime end);
        Task<IEnumerable<TimesheetApproval>> GetEmployeeForNotification(Guid employeeGuid, DateTime start, DateTime end);
    }
}
