using Excellerent.ReportManagement.Core.Entities.Options;
using Excellerent.ReportManagement.Core.Extensions;
using Excellerent.ReportManagement.Core.Interfaces.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Presentation.Services
{
    public partial class EmployeeNotificationHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<HostedNotificationServiceOptions> _options;
        private HostedNotificationServiceOptions _oldOption;
        private Timer _timer;
        private DateTime _refDateTime;

        public EmployeeNotificationHostedService(
            IServiceProvider serviceProvider,
            IOptions<HostedNotificationServiceOptions> options
            )
        {
            _serviceProvider = serviceProvider;

            _options = options;
            _oldOption = _options.Value;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _refDateTime = _oldOption.ExecuteAt(DateTime.Now);

            _timer = new Timer(
                DoWork,
                null,
                _oldOption.WaitFor(_refDateTime),
                _oldOption.Interval);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public void DoWork(object state)
        {
            try
            {
                if (IsConfigurationChanged()) return;
                using IServiceScope scope = _serviceProvider.CreateScope();
                ITimesheetNotificationService timesheetMailNotificationService
                    = scope.ServiceProvider.GetRequiredService<ITimesheetNotificationService>();
                var task = timesheetMailNotificationService.Notify(
                    _refDateTime,
                    _oldOption.BatchNotificationSpan()
                );
                task.Wait();
                Reconfigure(task.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool IsConfigurationChanged()
        {
            if (_options.Value.TimeZone != _oldOption.TimeZone
                || _options.Value.IsPeriodic != _oldOption.IsPeriodic
                || _options.Value.Interval != _oldOption.Interval
                || (_options.Value.IsPeriodic && (_options.Value.StartAt != _oldOption.StartAt)))
            {

                Reconfigure(DateTime.Now, true);
                return true;
            }
            return false;
        }

        private void Reconfigure(DateTime nextSchedule, bool reset = false)
        {
            _refDateTime = _oldOption.ExecuteAt(nextSchedule, reset ? null : _refDateTime);
            if (!reset && _oldOption.IsPeriodic) return;
            _timer?.Change(
                _oldOption.WaitFor(_refDateTime),
                _oldOption.BatchNotificationSpan());
        }
    }
}
