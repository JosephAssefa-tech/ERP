using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.APIModularization
{ /// <summary>
  /// This is base startup class. This initializes the database connection and provide abstract methods
  /// </summary>
    public abstract class ModuleStartup : IModuleStartup

    {
        protected IConfiguration Configuration { get; set; }

        public virtual void Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureDependencyInjection(services);
        }

        public abstract void ConfigureDependencyInjection(IServiceCollection services);

        /* this is not required for all Modules. Those needed will override
         */
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
