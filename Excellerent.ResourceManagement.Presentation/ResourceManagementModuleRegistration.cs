using Excellerent.APIModularization;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Services;
using Excellerent.ResourceManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.ResourceManagement.Presentation
{
    public class ResourceManagementModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
        
            services.AddScoped<IEmergencyContactRepository, EmergencyContactRepository>();
            services.AddScoped<IEmergencyContactService, EmergencyContactService>();
            services.AddScoped<IAsyncPersonalAddressRepository, AsyncPersonalAddressRepository>();
            services.AddScoped<IPersonalAddressService, PersonalAddressService>();
              
            services.AddControllersWithViews()
             .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


        }
    }
}
