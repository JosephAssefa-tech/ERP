using Excellerent.APIModularization.Logging;
using Excellerent.SharedInfrastructure;
using Excellerent.SharedModules.Helpers;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using excellerent_epp_be.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace excellerent_epp_be
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSharedInfrastructureServices(Configuration);

            //services.AddDbContext<EPPContext>(options => options.UseNpgsql(
            //    Configuration.GetConnectionString("epp"), 
            //    b => b.MigrationsAssembly("excellerent-epp-be")));

            // services.AddControllersWithViews();
            AddModules(services);
            services.AddVersioning();
            //Authentication and Identity
            ConfigureAuth(services);
            services.AddControllers()
                  .AddJsonOptions(options =>
                  {
                      options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                      options.JsonSerializerOptions.PropertyNamingPolicy = null;
                      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                  });
            services.AddApiVersioning();
            services.AddCors();
            services.AddHttpContextAccessor();
            services.AddTransient<IBusinessLog, BusinessLog>();
            /* services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.Authority = "https://3.138.163.97:3030/api/v1/";
                opt.Audience = "companyApi";
            });
            */
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Excellerent EPP API v1", Version = "v1" });

            });

            services.Configure<MailSenderOptions>(Configuration.GetSection("SmtpOption"));
            services.Configure<MailConfigurationOptions>(Configuration.GetSection("SmtpOption"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(
               options =>
               {

                   options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                   foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                   {
                       options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                           description.GroupName.ToUpperInvariant());
                   }
                   options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false); //Turns off syntax highlight which causing performance issues...
                   options.ConfigObject.AdditionalItems.Add("theme", "agate");
               });

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            //app.UseHttpsRedirection();

            //Use JWT for this case         
            var jwtOptions = GetTokenProviderOptions();
            // app.UseSimpleTokenProvider(jwtOptions);
            // app.UseMiddleware<TokenProviderMiddleware>(Options.Create(jwtOptions));

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
