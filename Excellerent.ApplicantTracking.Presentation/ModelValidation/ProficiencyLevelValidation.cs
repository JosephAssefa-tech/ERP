using Excellerent.ApplicantTracking.Domain.Entities;
using FluentValidation;

namespace Excellerent.ApplicantTracking.Presentation.ModelValidation
{
    public class ProficiencyLevelValidation : AbstractValidator<LUProficiencyLevelEntity>
    {
        public ProficiencyLevelValidation()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .MaximumLength(15);
        }
    }
}
