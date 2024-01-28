using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using FluentValidation;

namespace Excellerent.ApplicantTracking.Presentation.ModelValidation
{
    public class EducationProgramValidation : AbstractValidator<EducationProgramPostModel>
    {
        public EducationProgramValidation()
        {
            RuleFor(_ => _.Name)
                .NotNull()
                .MinimumLength(2).WithMessage("Please use names greater than two(2) words.")
                .MaximumLength(100).WithMessage("Please use names less than hundred (100) words");
        }
    }
}
