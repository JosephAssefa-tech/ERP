using Excellerent.APIModularization;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.TestData
{
    public class TestDataModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ITestDataService, TestDataService>();
        }
    }
}
