using Excellerent.APIModularization;
using Excellerent.ClientManagement.Domain.Interfaces;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Services;
using Excellerent.ClientManagement.Infrastructure.Repositories;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Services;
using Excellerent.ProjectManagement.Presentation.Validations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.ClientManagement.Presentation
{
    public class ClientManagementModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IClientDetailsRepository, ClientDetailsRepository>();
            services.AddScoped<IClientDetailsService, ClientDetailsService>();
            services.AddScoped<IClientStatusRepository, ClientStatusRepository>();
            services.AddScoped<IClientStatusService, ClientStatusService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IOperatingAddressRepository, OperatingAddressRepository>();
            services.AddScoped<IOperatingAddressService, OperatingAddressService>();
            services.AddScoped<IBillingAddressRepository, BillingAddressRepository>();
            services.AddScoped<IBillingAddressService, BillingAddressService>();

            services.AddScoped<IClientContactService, ClientContactService>();
            services.AddScoped<ICompanyContactService, CompanyContactService>();
            services.AddScoped<ICompanyContactRepository, CompanyContactRepository>();
            services.AddScoped<IClientContactRepository, ClientContactRepository>();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
      .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<ClientManagementModuleRegistration>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}