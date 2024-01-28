
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using FluentValidation;

namespace Excellerent.ApplicantTracking.Presentation.ModelValidation
{
    public class AreaOfInterestPostValidation : AbstractValidator<ApplicantAreaOfInterestPostModelDto>
    {
        public AreaOfInterestPostValidation()
        {
            RuleFor(model => model.ApplicantId)
                .NotNull();
            RuleFor(model => model.PositionToApplyID)
                .NotNull();
            RuleFor(model => model.ProficiencyLevelID)
                .NotNull();
            RuleFor(model => model.MonthOfExpierence)
                .InclusiveBetween(0, 11)
                .NotNull();
            RuleFor(model => model.YearsOfExpierence)
                .GreaterThanOrEqualTo(0)
                .NotNull();
            RuleFor(x => x.SelectedIDArray)
                .NotNull()
                .Custom((list, context) => {
                if (list.Length > 3)
                {
                    context.AddFailure("You can only select 3 or less.");
                }
            });
            RuleFor(x => x.SelectedIDSecondArray)
                .Custom((list, context) => {
                if (list.Length > 5)
                {
                    context.AddFailure("You can only select 5 or less.");
                }
            });
            RuleFor(x => x.SelectedIDOtherArray)
                .Custom((list, context) => {
                if (list.Length > 10)
                {
                    context.AddFailure("You can only select 10 or less.");
                }
           });
        }

    }
}
