using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using Excellerent.ApplicantTracking.Presentation.Models.PutModels;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Presentation.ModelValidation
{
    public class EducationPutModelValidator :AbstractValidator<EducationPutModel>
    {
        private readonly ILUFieldOfStudyService _fieldOfStudyervice;
        private readonly IEducationProgrammeService _programmeService;
        public EducationPutModelValidator(ILUFieldOfStudyService fieldOfStudyervice,
            IEducationProgrammeService programmeService) 
        {
            _fieldOfStudyervice = fieldOfStudyervice;
            _programmeService = programmeService;
            RuleFor(_ => _.Guid).NotNull();

            RuleFor(model => model.EducationProgrammeId)
                .NotNull();
            RuleFor(model => model.Institution)
                .NotNull();
            RuleFor(model => model.FieldOfStudyId)
                .NotNull();
              
            RuleFor(model => model.EducationProgrammeId)
                .NotNull();
            RuleFor(model => model.DateFrom)
                .NotNull()
                .LessThan(DateTime.Now);
            RuleFor(model => model.DateTo)
               .NotNull();
        }
    }
}
