using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.SharedInfrastructure
{
    public static class SharedInfrastructureServiceRegistration
    {
        public static IServiceCollection AddSharedInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

            services.AddDbContext<EPPContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("epp"),
                b => b.MigrationsAssembly("Excellerent.SharedInfrastructure")));
            return services;
        }
    }
}
