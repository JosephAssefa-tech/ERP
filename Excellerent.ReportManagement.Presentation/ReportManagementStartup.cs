using Excellerent.APIModularization;
using Excellerent.ReportManagement.Core.Entities.Options;
using Excellerent.ReportManagement.Core.Interfaces.IRepositories;
using Excellerent.ReportManagement.Core.Interfaces.IServices;
using Excellerent.ReportManagement.Core.Services;
using Excellerent.ReportManagement.Infrastructure.RepoServices;
using Excellerent.ReportManagement.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.ReportManagement.Presentation
{
    public class ReportManagementStartup : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.Configure<HostedNotificationServiceOptions>(Configuration.GetSection("NotificationOptions:HostedNotificationServiceOptions"));
            services.Configure<ResponseLinks>(Configuration.GetSection("NotificationOptions:ResponseLinks"));

            services.AddScoped<IMailRepoService, MailRepoService>();
            services.AddScoped<ITimesheetNotificationRepoService, TimesheetNotificationRepoService>();
            services.AddScoped<ITimesheetNotificationService, TimesheetNotificationService>();

            services.AddHostedService<EmployeeNotificationHostedService>();
        }
    }
}
