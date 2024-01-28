using Excellerent.APIModularization;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Services;
using Excellerent.Timesheet.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.Timesheet.Presentation
{
    public class TimesheetModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ITimesheetForNotificationRepository, TimesheetApprovalRepository>();
            services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
            services.AddScoped<ITimeSheetRepository, TimeSheetRepository>();
            services.AddScoped<ITimeSheetService, TimeSheetService>();
            services.AddScoped<ITimeEntryService, TimeEntryService>();
            services.AddScoped<ITimesheetApprovalRepository, TimesheetApprovalRepository>();
            services.AddScoped<ITimesheetApprovalService, TimesheetApprovalService>();
            services.AddScoped<ITimeSheetConfigRepository, TimeSheetConfigRepository>();
            services.AddScoped<ITimeSheetConfigService, TimeSheetConfigService>();
        }
    }
}
