using Excellerent.ApplicantTracking.Domain.Entities;
using FluentValidation;

namespace Excellerent.ApplicantTracking.Domain.ModelValidation
{
    public class ApplicantProfileValidation : AbstractValidator<ApplicantEntity>
    {
        public ApplicantProfileValidation()
        {
            RuleFor(model => model.FirstName)
             .NotEmpty().WithMessage("{FirstName} is required.")
             .NotNull();

            RuleFor(model => model.LastName)
              .NotEmpty().WithMessage("{LastName} is required.")
             .NotNull();

            RuleFor(model => model.Email)
                    .NotNull()
                    .NotEmpty().WithMessage("{Email} is required.")
                    .EmailAddress();

            RuleFor(model => model.Country)
                    .NotNull()
                    .NotEmpty().WithMessage("{Country} is required.")
                    .MaximumLength(20).WithMessage("{Country} must not exceed 40 characters.");


            RuleFor(model => model.ContactNumber)
                    .NotNull()
                    .NotEmpty().WithMessage("{ContactNumber} is required.");


            RuleFor(model => model.ResumeFile)
                    .NotNull();
            RuleFor(model => model.ProfileImage)
                    .NotNull();
        }
    }
}
