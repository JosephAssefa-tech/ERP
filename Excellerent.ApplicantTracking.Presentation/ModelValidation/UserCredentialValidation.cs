using Excellerent.ApplicantTracking.Domain.Entities;
using FluentValidation;

namespace Excellerent.ApplicantTracking.Domain.ModelValidation
{
    public class UserCredentialValidation : AbstractValidator<ApplicantEntity>
    {
        public UserCredentialValidation()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(model => model.Password)
                .NotNull();


        }
    }
}
