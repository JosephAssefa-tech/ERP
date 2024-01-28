using Excellerent.ReportManagement.Core.Entities.Models;
using Excellerent.ReportManagement.Core.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Infrastructure.Extensions
{
    public static class TimesheetNotificationRepositoryExtensions
    {
        public static IEnumerable<FirstEscalationNotificationModel>
            ExtractFirstEscalationsToSupervisor(
                this IEnumerable<ProjectResource> notifications
            )
        {
            return notifications
                .GroupBy(n => new
                {
                    SupervisorGuid = n.SupervisorGuid,
                    SupervisorFullName = n.SupervisorFullName,
                    SupervisorEmailAddress = n.SupervisorEmailAddress
                })
                .Select(n => new FirstEscalationNotificationModel()
                {
                    Guid = n.Key.SupervisorGuid,
                    FullName = n.Key.SupervisorFullName,
                    EmailAddress = n.Key.SupervisorEmailAddress,
                    Employees = n.ToList()
                                .Select(r => r.EmployeeFullName)
                                .Distinct()
                });
        }

        public static IEnumerable<TimesheetResourceNotification>
            ExtractFirstEscalationsToResource(this IEnumerable<ProjectResource> notifications)
        {
            return notifications
                .GroupBy(n => new
                {
                    EmployeeGuid = n.EmployeeGuid,
                    EmployeeFullName = n.EmployeeFullName,
                    EmployeeEmailAddress = n.EmployeeEmailAddress
                })
                .Select(n => new TimesheetResourceNotification()
                {
                    Guid = n.Key.EmployeeGuid,
                    FullName = n.Key.EmployeeFullName,
                    EmailAddress = n.Key.EmployeeEmailAddress,
                    Projects = n.ToList().Select(p => p.ProjectName).ToArray()
                });
        }

        public static async Task<Guid?>
            ManageIndividualNotifications<TIn>(
                this Task task, TIn notificationModel
            ) where TIn : BaseNotificationModel
        {
            try
            {
                await task;
                return null;
            }
            catch (Exception)
            {
                return notificationModel.Guid;
            }
        }

        public static async Task<Guid?>
            ManageIndividualNotifications<TIn>(
                this Task task, TIn notificationModel,
                ILogger logger,
                EscalateType escalateType
            ) where TIn : BaseNotificationModel
        {
            try
            {
                await task;
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Timesheet {0} Escalation Email Notification Error to Employee with Id {1}.\n {2}",
                    escalateType, notificationModel.Guid, ex);
                return notificationModel.Guid;
            }
        }

        public static async Task<IEnumerable<Guid>> ManageGroupNotifications(this IEnumerable<Task<Guid?>> tasks)
        {
            return (await Task.WhenAll(tasks))
                .Where(id => id != null)
                .Select(id => Guid.Parse(id.ToString()));
        }
    }
}
