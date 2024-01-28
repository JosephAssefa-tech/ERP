using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Presentation.ModelValidation
{
    public class EducationPostModelValidator : AbstractValidator<EducationPostModelDto>
    {
        private readonly ILUFieldOfStudyService _fieldOfStudyervice;
        private readonly IEducationProgrammeService _programmeService;
        private readonly IEducationService _educationService;
        public EducationPostModelValidator(ILUFieldOfStudyService applicantService,
            IEducationProgrammeService programmeService,
            IEducationService educationService)
        {
            this._fieldOfStudyervice = applicantService;
            this._programmeService = programmeService;
            this._educationService = educationService;

            RuleFor(model => model.EducationProgrammeId)
                .NotNull();
            RuleFor(model => model.Institution)
                .NotNull()
                .MaximumLength(50);
            RuleFor(model => model.FieldOfStudyId)
                .NotNull().WhenAsync(async (model, cancellation) =>
                {
                    return !(await CheckIfHighSchoolDeplopmaSelectedAsync(model));
                });
            RuleFor(model => model.EducationProgrammeId)
                .NotNull();

            RuleFor(model => model.DateFrom)
                .NotNull()
                .LessThan(DateTime.Now);
            RuleFor(model => model.DateTo)
               .NotNull();

              
            RuleFor(model => model.EducationProgrammeId).NotNull()
                .MustAsync(async (model, programmeId, cancellation) =>
                {
                    return !await HighSchoolRecordExists(model);
                })
                .WithMessage("High school record already exist");
        }

        private async Task<bool> HighSchoolRecordExists(EducationPostModelDto model)
        {
            var educationProgramme = await _programmeService.GetByIdAsync(model.EducationProgrammeId);
            var exists = await _educationService.GetExistenceByProgrammeAsync(model.ApplicantId, model.EducationProgrammeId);
            return educationProgramme.Name.ToLower().Contains("high school") && exists;
        }

        private async Task<bool> CheckIfHighSchoolDeplopmaSelectedAsync(EducationPostModelDto model)
        {
            var educationProgramme = await _programmeService.GetByIdAsync(model.EducationProgrammeId);
            return educationProgramme.Name.ToLower().Contains("high school");
        }
    }
}
