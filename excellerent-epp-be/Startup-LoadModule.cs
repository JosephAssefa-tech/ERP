using Excellerent.APIModularization;
using Excellerent.ApplicantTracking.Presentation;
using Excellerent.ClientManagement.Presentation;
using Excellerent.EppConfiguration.Presentation;
using Excellerent.ProjectManagement.Presentation;
using Excellerent.ReportManagement.Presentation;
using Excellerent.ResourceManagement.Presentation.Config;
using Excellerent.TestData;
using Excellerent.Timesheet.Presentation;
using Excellerent.UserManagement.Presentation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace excellerent_epp_be
{
    public partial class Startup
    {
        private IList<IModuleStartup> modules = new List<IModuleStartup>();

        public void AddModules(IServiceCollection services)
        {
            modules.Add(new UserManagementModuleRegistration());
            modules.Add(new ApplicantModuleRegistration());
            modules.Add(new TimesheetModuleRegistration());
            modules.Add(new ProjectManagementModuleRegistration());
            modules.Add(new ResourceManagementModuleRegistration());
            modules.Add(new ClientManagementModuleRegistration());
            modules.Add(new EppConfigurationModuleRegistration());
            modules.Add(new TestDataModuleRegistration()); // Used to add Initial data.
            modules.Add(new ReportManagementStartup());
            ConfigureModules(services);
        }

        public void ConfigureModules(IServiceCollection services)
        {
            // find all controllers
            var Controllers = from a in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                              from t in a.GetTypes()
                              let attributes = t.GetCustomAttributes(typeof(ControllerAttribute), true)
                              where attributes?.Length > 0
                              select new { Type = t };
            var ControllersList = Controllers.ToList();

            // register them
            foreach (var Controller in ControllersList)
            {
                services
                    .AddMvc()
                    .AddApplicationPart(Controller.Type.Assembly);
            }

            // configuring services for all modules
            foreach (var module in modules)
            {
                module.Startup(Configuration);
                module.ConfigureServices(services);
            }
        }
    }
}
