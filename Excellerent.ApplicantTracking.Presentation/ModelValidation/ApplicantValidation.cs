using Excellerent.ApplicantTracking.Presentation.Models;
using FluentValidation;

namespace Excellerent.ApplicantTracking.Presentation.ModelValidation
{
    public class ApplicantValidation : AbstractValidator<ApplicantPostModel>
    {
        public ApplicantValidation()
        {
            RuleFor(model => model.FirstName)
                .NotNull()
                .MaximumLength(50);

            RuleFor(model => model.LastName)
                .NotNull()
                .MaximumLength(50);

            RuleFor(model => model.Email)
                .NotNull()
                .EmailAddress()
                .MaximumLength(50);


            RuleFor(model => model.Country)
                .NotNull()
                .MaximumLength(50);

            RuleFor(model => model.ContactNumber)
                .NotNull()
                .Length(12)
                .MaximumLength(50);

        }

    }
}
