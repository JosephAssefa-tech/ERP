using Excellerent.ApplicantTracking.Domain.Entities;
using FluentValidation;

namespace Excellerent.ApplicantTracking.Presentation.ModelValidation
{
    public class SignupValidation : AbstractValidator<ApplicantEntity>
    {
        public SignupValidation()
        {

            RuleFor(model => model.FirstName)

              .NotEmpty().WithMessage("{FirstName} is required.")
              .NotNull()
              .MaximumLength(20).WithMessage("{FirstName} must not exceed 20 characters.");



            RuleFor(model => model.LastName)
              .NotEmpty().WithMessage("{LastName} is required.")
              .NotNull()
              .MaximumLength(20).WithMessage("{LastName} must not exceed 20 characters.");



            RuleFor(model => model.Email)
              .NotEmpty().WithMessage("{Email} is required.")
              .NotNull()
              .MaximumLength(20).WithMessage("{Email} must not exceed 20 characters.")
              .EmailAddress();



            RuleFor(model => model.Password)
                .NotNull()
                .NotEmpty().WithMessage("{Password} is required.")
                .NotNull()
                .MaximumLength(40).WithMessage("{Password} must not below  10 characters.")
                .MinimumLength(4);
        }
    }
}
