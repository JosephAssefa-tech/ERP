
using Excellerent.Usermanagement.Domain.Entities;
using FluentValidation;

namespace Excellerent.UserManagement.Presentation.Validations
{
    public class UserCredentialValidations : AbstractValidator<UserEntity>
    {
        public UserCredentialValidations()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(model => model.Password)
                .NotNull();


        }
    }
}
