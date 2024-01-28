using AutoMapper;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Presentation.Models;
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using Excellerent.ApplicantTracking.Presentation.Models.PutModels;
using Excellerent.ApplicantTracking.Presentation.Models.ViewModels;
using Excellerent.ApplicantTracking.Presentation.Modlels.ViewModels;

namespace Excellerent.ApplicantTracking.Presentation.Helper
{
    public class ApplicantTrackingProfile : Profile
    {
        public ApplicantTrackingProfile()
        {
            CreateMap<ApplicantEntity, SigninViewModel>();
            CreateMap<ApplicantEntity, SignUpModelDto>();
            CreateMap<EducationProgrammeEntity, EducationProgramPostModel>().ReverseMap();
            CreateMap<EducationProgrammeEntity, EducationProgrammeGetModelDto>().ReverseMap();
            CreateMap<LUFieldOfStudyEntity, LUFieldOfStudyGetModelDto>().ReverseMap();
            CreateMap<LUFieldOfStudyEntity, LUFieldOfStudyModelDto>()
                .ForMember(e => e.Name, opt => opt.MapFrom(s => s.Name));

            CreateMap<EducationEntity, EducationGetModelDto>()
                .ForMember(e => e.FieldOfStudy, opt => opt.MapFrom(s => s.FieldOfStudy))
                .ForMember(e => e.EducationProgramme, opt => opt.MapFrom(s => s.EducationProgramme));

            CreateMap<EducationEntity, EducationPostModelDto>().ReverseMap();
            CreateMap<EducationPutModel, EducationEntity>();
        }
    }
}
