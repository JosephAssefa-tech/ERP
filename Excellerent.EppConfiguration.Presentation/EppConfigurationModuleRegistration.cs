
using Excellerent.APIModularization;
using Excellerent.EppConfiguration.Domain.Interfaces.Repository;
using Excellerent.EppConfiguration.Domain.Interfaces.Service;
using Excellerent.EppConfiguration.Domain.Services;
using Excellerent.EppConfiguration.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.EppConfiguration.Presentation
{
    public class EppConfigurationModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IEppConfigurationRepository, EppConfigurationRepository>();
            services.AddScoped<IEppConfigurationService, EppConfigurationService>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
