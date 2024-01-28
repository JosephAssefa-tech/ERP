
using Excellerent.APIModularization;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Services;
using Excellerent.Usermanagement.Infrastructure.Repositories;
using Excellerent.Usermanagement.Presentation.MailService;
using Excellerent.UserManagement.Presentation.AccoutService;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.UserManagement.Presentation
{
    public class UserManagementModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(UserManagementProfile));
            services.AddScoped<IGroupSetRepository, GroupSetRepository>();
            services.AddScoped<IGroupSetService, GroupSetService>();

            services.AddScoped<IUserGroupsRepository, UserGroupsRepository>();
            services.AddScoped<IUserGroupsService, UserGroupsService>();
      

            //////////////////////////////////////////////////////////
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserGroupsService, UserGroupsService>();
            services.AddScoped<IUserGroupsRepository,UserGroupsRepository>();

            services.AddScoped<IGroupSetPermissionRepository, GroupSetPermissionRepository>();
            services.AddScoped<IGroupSetPermissionService, GroupSetPermissionService>();
            services.AddScoped<IUserGroupsRepository, UserGroupsRepository>();
            services.AddScoped<IUserGroupsService, UserGroupsService>();
            services.AddScoped<ISendMailService, SendMailService>();
            services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
            services.AddScoped<IPasswordResetTokenService, PasswordResetTokenService>();
        }
    }
}