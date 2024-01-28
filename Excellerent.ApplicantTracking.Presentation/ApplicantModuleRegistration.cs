using Excellerent.APIModularization;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.Services;
using Excellerent.ApplicantTracking.Infrastructure.Repositories;
using Excellerent.ApplicantTracking.Presentation.AccoutService;
using Excellerent.ApplicantTracking.Presentation.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.ApplicantTracking.Presentation
{
    public class ApplicantModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IApplicantService, ApplicantService>();

            services.AddScoped<IApplicantAreaOfInterestRepository, ApplicantAreaOfInterestRepository>();
            services.AddScoped<IApplicantAreaOfInterestService, ApplicantAreaOfInterestService>();
            

            services.AddScoped<ILUFieldOfStudyRepository, LUFieldOfStudyRepository>();
            services.AddScoped<ILUFieldOfStudyService, LUFieldOfStudyService>();

            services.AddScoped<ILUPositionToApplyRepository, LUPositionToApplyRepository>();
            services.AddScoped<ILuPositionService, LUPositionToApplyService>();

            services.AddScoped<IEducationProgrammeRepository, EducationProgrammeRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();

            services.AddScoped<IEducationProgrammeService, EducationProgrammeService>();
            services.AddScoped<IEducationService, EducationService>();
            
            services.AddAutoMapper(typeof(ApplicantTrackingProfile));

            services.AddScoped<ILUSkillSetService, LUSkillSetService>();
            services.AddScoped<ILUSkillSetRepository, LUSkillSetRepository>();

            services.AddScoped<ILUProficiencyLevelService, LUProficiencyLevelService>();
            services.AddScoped<ILUProficiencyLevelRepository, LUProficiencyLevelRepository>();

            services.AddScoped<ILUPositionSkillSetService, LUPositionSkillSetService>();
            services.AddScoped<ILUPositionSkillSetRepository, LUPositionSkillSetRepository>(); 
             
            services.AddScoped<IApplicantAreaOfInterestService, ApplicantAreaOfInterestService>();
            services.AddScoped<IApplicantAreaOfInterestRepository, ApplicantAreaOfInterestRepository>();

        }
    }
}
