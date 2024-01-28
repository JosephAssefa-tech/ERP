using Excellerent.APIModularization;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.UserManagement.API
{
    public class UserManagementModule : ModuleStartup
    {


        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            //services.AddDbContext<UserManagementsContext>(options =>
            //   options.UseNpgsql(Configuration.GetConnectionString("UserManagement"),
            //   b => b.MigrationsAssembly("AIC.UserManagment.Infrastructure")));

            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddEntityFrameworkStores<UserManagementsContext>()
            //    .AddDefaultTokenProviders();

            //services.AddScoped<IEmployeeManagementRepository, EmployeeManagementRepository>();

            //services.AddTransient<IAccountManager, AccountManager>();
            //services.AddTransient<UserManager<ApplicationUser>>();
        }
    }
}
