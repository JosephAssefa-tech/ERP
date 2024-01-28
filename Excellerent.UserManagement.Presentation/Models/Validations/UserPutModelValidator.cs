using Excellerent.UserManagement.Presentation.Models.PutModels;
using FluentValidation;

namespace Excellerent.UserManagement.Presentation.Models.Validations
{
    public class UserPutModelValidator : AbstractValidator<UserPutModel>
    {
        public UserPutModelValidator()
        {
            RuleFor(_ => _.Guid)
               .NotNull();
            RuleFor(_ => _.Email)
                .NotNull()
                .EmailAddress();
            RuleFor(_ => _.FullName) // RuleFor(_ => _.FirstName)
                .NotNull();
            RuleFor(_ => _.Tel)
              .NotNull();
        }
    }
}
