using Excellerent.ReportManagement.Core.Entities.Configs;
using Excellerent.ReportManagement.Core.Entities.Options;
using Excellerent.ReportManagement.Core.Enums;
using Excellerent.ReportManagement.Core.Extensions;
using Excellerent.ReportManagement.Core.Interfaces.IRepositories;
using Excellerent.ReportManagement.Core.Interfaces.IServices;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Services
{
    public partial class TimesheetNotificationService : ITimesheetNotificationService
    {
        private readonly ITimeSheetConfigService _timesheetConfigService;
        private readonly ITimesheetNotificationRepoService _timesheetNotificationRepoService;
        private readonly IOptions<ResponseLinks> _linkOptions;
        private readonly ResponseLinks _reponseLinks;

        public TimesheetNotificationService(
            IOptions<ResponseLinks> linkOptions,
            ITimeSheetConfigService timesheetConfigService,
            ITimesheetNotificationRepoService timesheetNotificationRepoService)
        {
            _linkOptions = linkOptions;
            _timesheetConfigService = timesheetConfigService;
            _timesheetNotificationRepoService = timesheetNotificationRepoService;
            _reponseLinks = linkOptions.Value;
        }

        public async Task<DateTime> Notify(DateTime refDateTime, TimeSpan refInterval)
        {
            var timesheetConfig
                = (await _timesheetConfigService.GetTimeSheetConfiguration())
                    .Data as ConfigurationDto;
            var modifiedConfig = await timesheetConfig.GetModifiedConfigurationDto();

            var tasks = new List<Task<bool>>();
            // Resource
            var resourceConfig = await modifiedConfig.GetTimesheetNotificationConfig(
                                        refDateTime, EscalateType.Reminder, _linkOptions.Value);
            if (resourceConfig.RemindAt - refDateTime < refInterval)
                tasks.Add(_timesheetNotificationRepoService.SendReminderNotifications(resourceConfig));
            // First Escalation
            var firstEscalationConfig = await modifiedConfig.GetTimesheetNotificationConfig(
                                            refDateTime, EscalateType.First, _linkOptions.Value);
            if (firstEscalationConfig.FirstEscalationAt - refDateTime < refInterval)
                tasks.Add(_timesheetNotificationRepoService.SendFirstEscalationNotifications(firstEscalationConfig));
            // Second Escalation
            var secondEscalationConfig = await modifiedConfig.GetTimesheetNotificationConfig(
                                            refDateTime, EscalateType.Second, _linkOptions.Value);
            if (secondEscalationConfig.SecondEscaletionAt - refDateTime < refInterval)
                tasks.Add(_timesheetNotificationRepoService.SendSecondEscalationNotifications(secondEscalationConfig));

            var result = await Task.WhenAll(tasks);

            // Here code to handele error

            return await modifiedConfig.GetNextSchedule(refDateTime, refInterval);
        }
    }
}
