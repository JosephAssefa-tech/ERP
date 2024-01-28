using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ReportManagement.Core.Entities.Configs;
using Excellerent.ReportManagement.Core.Entities.Models;
using Excellerent.ReportManagement.Core.Enums;
using Excellerent.ReportManagement.Core.Helpers;
using Excellerent.ReportManagement.Core.Interfaces.IRepositories;
using Excellerent.ReportManagement.Infrastructure.Extensions;
using Excellerent.SharedInfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Infrastructure.RepoServices
{
    public partial class TimesheetNotificationRepoService : ITimesheetNotificationRepoService
    {

        private readonly EPPContext _context;
        private readonly IMailRepoService _mailRepoService;
        private readonly ILogger<TimesheetNotificationRepoService> _logger;

        public TimesheetNotificationRepoService(
            EPPContext context,
            IMailRepoService mailService,
            ILogger<TimesheetNotificationRepoService> logger
            )
        {
            _context = context;
            _mailRepoService = mailService;
            _logger = logger;
        }

        //===============================================================================
        //
        // BATCH NOTIFICATIONS
        //
        //-------------------------------------------------------------------------------
        //
        // FETCH DATA AND SEND NOTIFICATIONS

        public async Task<bool> Notify(
            TimesheetNotificationConfig reminderConfig,
            TimesheetNotificationConfig firstEscalationConfig,
            TimesheetNotificationConfig secondEscalationConfig)
        {
            return !(await Task.WhenAll(
                new List<Task<bool>>()
                {
                    SendReminderNotifications(reminderConfig),
                    SendFirstEscalationNotifications(firstEscalationConfig)
                    //SendSecondEscalationNotifications(secondEscalationConfig)
                })).Where(r => !r).Any();
        }

        public async Task<bool> SendReminderNotifications([DisallowNull] TimesheetNotificationConfig config)
        {
            var notifications = await GetTimesheetResourceNotifications(config.StartQuery, config.EndQuery).ToListAsync();
            if (notifications.Count() == 0)
                return true;

            var result = await SendMultipleNotificationsForResource(notifications, config, EscalateType.Reminder);

            // here what to do with the Guids of the employees with failed notification.

            return !result.Any();
        }

        public async Task<bool> SendFirstEscalationNotifications([DisallowNull] TimesheetNotificationConfig config)
        {
            var notifications = await GetTimeFirstEscalationNotifications(config.StartQuery, config.EndQuery).ToListAsync();
            if (!notifications.Any())
                return true;

            var managerResult = await SendMultipleFirstEscalationNotificationsForManagement(
                notifications.ExtractFirstEscalationsToSupervisor(), config);
            var resourceResult = await SendMultipleNotificationsForResource(
                notifications.ExtractFirstEscalationsToResource(), config, EscalateType.First);

            // here what to do with the Guids of the employees with failed notification.

            return !(managerResult.Any() || resourceResult.Any());
        }

        public async Task<bool> SendSecondEscalationNotifications([DisallowNull] TimesheetNotificationConfig config)
        {
            var notifications = await GetTimesheetResourceNotifications(config.StartQuery, config.EndQuery).ToListAsync();
            var managers = await GetSecondEscalationNotifieds().ToListAsync();
            if (!managers.Any() || !notifications.Any())
                return true;

            var managerResult = await SendMultipleSecondEscalationNotificationsForManagement(
                    managers, notifications.Select(n => n.FullName), config);
            var resourceResult = await SendMultipleNotificationsForResource(
                notifications, config, EscalateType.Second);

            // here what to do with the Guids of the employees with failed notification.

            return !(managerResult.Any() || resourceResult.Any());
        }


        #region Notification query

        public IQueryable<TimesheetResourceNotification> GetTimesheetResourceNotifications(DateTime starts, DateTime ends)
        {
            string query = $@"
				select e.""Guid"" as ""Guid"" ,
					e.""FullName"" as ""FullName"",
					eo.""CompaynEmail"" as ""EmailAddress"" ,
					array_agg(p.""ProjectName"") as ""Projects""
					from public.""ClientDetails"" cd
					inner join public.""Project"" p 
						on cd.""Guid"" = p.""ClientGuid"" 
					inner join public.""ProjectStatus"" ps 
						on p.""ProjectStatusGuid"" = ps.""Guid""
					inner join public.""Employees"" se 
						on p.""SupervisorGuid"" = se.""Guid"" 
					inner join public.""EmployeeOrganizations"" seo 
						on se.""Guid"" = seo.""EmployeeId""
					inner join public.""AssignResources"" ar 
						on p.""Guid"" = ar.""ProjectGuid""
					inner join public.""Employees"" e 
						on ar.""EmployeeGuid"" = e.""Guid"" 
					inner join public.""EmployeeOrganizations"" eo 
						on e.""Guid"" = eo.""EmployeeId""
					left join public.""TimeSheet"" ts 
						on e.""Guid"" = ts.""EmployeeId"" 
							and not ts.""IsDeleted""
							and ts.""FromDate"" <= '{ends.ToString("yyyy-MM-dd")}'
							and ts.""ToDate""  >= '{starts.ToString("yyyy-MM-dd")}'
					left join public.""TimesheetAprovals"" ta 
						on p.""Guid"" = ta.""ProjectId"" and ts.""Guid"" = ta.""TimesheetId""
							and not ta.""IsDeleted""
							and ta.""Status"" != 0
							and ta.""Status"" != 1
					where not cd.""IsDeleted""
						and cd.""ClientName"" != 'Leave'
						and not p.""IsDeleted"" 
						and p.""StartDate"" < '{ends.ToString("yyyy-MM-dd")}'
						and (p.""EndDate"" is null or p.""EndDate"" > '{starts.ToString("yyyy-MM-dd")}')
						and p.""ProjectName"" not in ('Leave', 'Holiday')
						and not se.""IsDeleted"" 
						and seo.""Status"" = 'Active'
						and not ar.""IsDeleted"" 
						and ar.""AssignDate"" < '{ends.ToString("yyyy-MM-dd")}'
						and not e.""IsDeleted"" 
						and eo.""Status"" = 'Active'
						and eo.""CompaynEmail"" <> ''
					group by e.""Guid"" ,
						e.""FullName"" ,
						eo.""CompaynEmail""
			";

            return _context.TimesheetResourceNotifications.FromSqlRaw(query);
        }
        
        public IQueryable<ProjectResource> GetTimeFirstEscalationNotifications(DateTime starts, DateTime ends)
        {
            string query = $@"
				select cd.""Guid"" as ""ClientGuid"",
					cd.""ClientName"" as ""ClientName"",
					p.""Guid"" as ""ProjectGuid"",
					p.""ProjectName"" as ""ProjectName"",
					se.""Guid"" as ""SupervisorGuid"",
					se.""FullName"" as ""SupervisorFullName"",
					seo.""CompaynEmail"" as ""SupervisorEmailAddress"",
					e.""Guid"" as ""EmployeeGuid"",
					e.""FullName"" as ""EmployeeFullName"",
					eo.""CompaynEmail"" as ""EmployeeEmailAddress""
					from public.""ClientDetails"" cd
					inner join public.""Project"" p
						on cd.""Guid"" = p.""ClientGuid"" 
					inner join public.""ProjectStatus"" ps
						on p.""ProjectStatusGuid"" = ps.""Guid""
					inner join public.""Employees"" se
						on p.""SupervisorGuid"" = se.""Guid"" 
					inner join public.""EmployeeOrganizations"" seo
						on se.""Guid"" = seo.""EmployeeId""
					inner join public.""AssignResources"" ar
						on p.""Guid"" = ar.""ProjectGuid""
					inner join public.""Employees"" e
						on ar.""EmployeeGuid"" = e.""Guid"" 
					inner join public.""EmployeeOrganizations"" eo
						on e.""Guid"" = eo.""EmployeeId""
					left join public.""TimeSheet"" ts
						on e.""Guid"" = ts.""EmployeeId"" 
							and not ts.""IsDeleted""
							and ts.""FromDate"" <= '{ends.ToString("yyyy-MM-dd")}'
							and ts.""ToDate""  >= '{starts.ToString("yyyy-MM-dd")}'
					left join public.""TimesheetAprovals"" ta
						on p.""Guid"" = ta.""ProjectId"" and ts.""Guid"" = ta.""TimesheetId""
							and not ta.""IsDeleted""
							and (
								ta.""Status"" is null or
								ta.""Status"" > 1
							)
					where not cd.""IsDeleted""
						and cd.""ClientName"" != 'Leave'
						and not p.""IsDeleted"" 
						and p.""StartDate"" < '{ends.ToString("yyyy-MM-dd")}'
						and (p.""EndDate"" is null or p.""EndDate"" > '{starts.ToString("yyyy-MM-dd")}')
						and p.""ProjectName"" not in ('Leave', 'Holiday')
						and not se.""IsDeleted"" 
						and seo.""Status"" = 'Active'
						and not ar.""IsDeleted"" 
						and ar.""AssignDate"" < '{ends.ToString("yyyy-MM-dd")}' 
						and not e.""IsDeleted"" 
						and eo.""Status"" = 'Active'
						and eo.""CompaynEmail"" <> ''
			";

            return _context.ProjectResources.FromSqlRaw(query);
        }
        
        public IQueryable<EmployeeBrief> GetSecondEscalationNotifieds()
        {
            string permissionKey = "Second_Timesheet_Escalation_Notification";
            string query = $@"
				select distinct e.""Guid"" ,
					e.""FullName"" ,
					eo.""CompaynEmail"" as ""EmailAddress""
					from public.""Permissions"" p
					inner join public.""GroupSetPermissions"" gsp
						on p.""Guid"" = gsp.""PermissionId"" 
					inner join public.""UserGroups"" ug
						on gsp.""GroupSetId"" = ug.""GroupSetGuid"" 
					inner join public.""Users"" u
						on ug.""UserGuid"" = u.""Guid"" 
					inner join public.""Employees"" e
						on u.""EmployeeId"" = e.""Guid"" 
					inner join public.""EmployeeOrganizations"" eo
						on e.""Guid"" = eo.""EmployeeId"" 
					where p.""KeyValue"" = '{permissionKey}'
						and e.""FullName"" != 'Admin Admin'
						and not e.""IsDeleted"" 
						and eo.""Status"" = 'Active'
						and eo.""CompaynEmail"" <> ''
			";

            return _context.EmployeeBriefs.FromSqlRaw(query);
        }

        #endregion

        //-------------------------------------------------------------------------------
        //
        // SEND NOTIFICATIONS

        private async Task<IEnumerable<Guid>> SendMultipleNotificationsForResource(
            IEnumerable<TimesheetResourceNotification> notifications,
            TimesheetNotificationConfig config,
            EscalateType escalateType)
        {
            string template
                = escalateType == EscalateType.Reminder ? EmailTemplateFilesCollection.ReminderNotification
                : escalateType == EscalateType.First ? EmailTemplateFilesCollection.FirstEscalationResourceNotification
                : EmailTemplateFilesCollection.FirstEscalationResourceNotification;
            return await notifications
                 .Select(n =>
                     SendSingleNotificationForResource(n, config, template)
                         .ManageIndividualNotifications(n, _logger, config.EscalateType))
                 .ManageGroupNotifications();
        }

        private async Task<IEnumerable<Guid>> SendMultipleFirstEscalationNotificationsForManagement(
            IEnumerable<FirstEscalationNotificationModel> notifications,
            TimesheetNotificationConfig config)
        {
            string template = EmailTemplateFilesCollection.FirstEscalationNotification;
            return await notifications
                .Select(n =>
                    SendSingleFirstEscalationNotificationForManager(n, config, template)
                        .ManageIndividualNotifications(n, _logger, config.EscalateType))
                .ManageGroupNotifications();
        }

        private async Task<IEnumerable<Guid>> SendMultipleSecondEscalationNotificationsForManagement(
            IEnumerable<EmployeeBrief> notifications,
            IEnumerable<string> resources,
            TimesheetNotificationConfig config)
        {
            string template = EmailTemplateFilesCollection.SecondEscalationResourceNotification;
            return await notifications
                .Select(n =>
                    SendSingleSecondEscalationNotificationForManager(n, config, template, resources)
                        .ManageIndividualNotifications(n, _logger, config.EscalateType))
                .ManageGroupNotifications();
        }


        //===============================================================================
        //
        // SINGLE NOTIFICATION
        //
        //-------------------------------------------------------------------------------
        //
        // SEND NOTIFICATION

        // RESOURCES

        private async Task SendSingleNotificationForResource(
            TimesheetResourceNotification notification,
            TimesheetNotificationConfig config,
            string template)
        {
            string body = notification.ComposeResourceNotificationEmail(template, config);
            var mail = notification.ComposeEmail(body);
            mail.Subject = String.Format(
                "Time Entry not Submitted for the Week of {0} to {1} – {2} reminder",
                config.Start.ToString("MM/dd/YYYY"),
                config.End.ToString("MM/dd/YYYY"),
                config.EscalateType == EscalateType.Reminder ? "1st"
                : config.EscalateType == EscalateType.First ? "2nd"
                : "3rd"
                );
            await _mailRepoService.SendEmail(mail);
        }

        private async Task SendSingleFirstEscalationNotificationForManager
            (
            FirstEscalationNotificationModel notification,
            TimesheetNotificationConfig config,
            string template)
        {
            var body = notification.ComposeSupervisorNotificationEmail(template, config);
            var mail = notification.ComposeEmail(body);
            mail.Subject = String.Format(
                "Time Entry not Submitted for the Week of {0} to {1} by employees",
                config.Start.ToString("MM/dd/YYYY"),
                config.End.ToString("MM/dd/YYYY")
                );
            await _mailRepoService.SendEmail(mail);
        }

        private async Task SendSingleSecondEscalationNotificationForManager(
            EmployeeBrief notification,
            TimesheetNotificationConfig config,
            string template,
            IEnumerable<string> resources
            )
        {
            var body = notification.ComposeSecondEscalationNotificationEmail(template, config, resources);
            var mail = notification.ComposeEmail(body);

            mail.Subject = String.Format(
                "Time Entry not Submitted for the Week of {0} to {1} by employees across Excellerent",
                config.Start.ToString("MM/dd/YYYY"),
                config.End.ToString("MM/dd/YYYY")
                );
            await _mailRepoService.SendEmail(mail);
        }

    }
}
