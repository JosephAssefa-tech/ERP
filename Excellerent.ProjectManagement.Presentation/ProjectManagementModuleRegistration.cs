using Excellerent.APIModularization;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Services;
using Excellerent.ProjectManagement.Infrastructure.Repositories;
using Excellerent.ProjectManagement.Presentation.Validations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.ProjectManagement.Presentation
{
    public class ProjectManagementModuleRegistration : ModuleStartup
    {

        public override void ConfigureDependencyInjection(IServiceCollection services)
        {

            services.AddScoped<IAssignResourceRepository, AssignResourceRepository>();
            services.AddScoped<IAssignResourceService, AssignResourceService>();
            services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
            services.AddScoped<IProjectStatusService, ProjectStatusService>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IEmployeeService,EmployeeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProjectModuleService, ProjectModuleService>();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
     .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<ProjectManagementModuleRegistration>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
        }


    }
}
