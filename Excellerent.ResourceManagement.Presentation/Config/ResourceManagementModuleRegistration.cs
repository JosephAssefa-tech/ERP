using Excellerent.APIModularization;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Service;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Repository.Interfaces;
using Excellerent.ResourceManagement.Domain.Services;
using Excellerent.ResourceManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.ResourceManagement.Presentation.Config
{
    public class ResourceManagementModuleRegistration : ModuleStartup
    {


        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IEmergencyContactRepository, EmergencyContactRepository>();
            services.AddScoped<IEmergencyContactService, EmergencyContactService>();

            services.AddScoped<IAsyncPersonalAddressRepository, AsyncPersonalAddressRepository>();
            services.AddScoped<IPersonalAddressService, PersonalAddressService>();

            services.AddScoped<IEmergencyContactsRepository, EmergencyConactsRepository>();
            services.AddScoped<IEmergencyContactsService, EmergencyContactsService>();


            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IEmployeeOrganizationRepository, EmployeeOrganizationRepository>();
            services.AddScoped<IEmployeeOrganizationService, EmployeeOrganizationService>();
            

            services.AddScoped<IFamilyDetailRepository, FamilyDetailRepository>();
            services.AddScoped<IRelationshipRepository, RelationshipRepository>();
            services.AddScoped<IFamilyDetailService, FamilyDetailService>();
            services.AddScoped<IRelationshipService, RelationshipService>();

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryRepository, CountryRepository>();

            services.AddScoped<IDutyBranchService, DutyBranchService>();
            services.AddScoped<IDutyBranchRepository, DutyBranchRepository>();

            services.AddScoped<IDeviceDetailsRepository, DeviceDetailsRepository>();
            services.AddScoped<IDeviceDetailsService, DeviceDetailsService>();

            services.AddScoped<IDeviceClassificationService, DeviceClassificationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
        }
    }
}
