using Excellerent.UserManagement.Presentation.Models.PostModel;
using FluentValidation;

namespace Excellerent.UserManagement.Presentation.Validations
{
    public  class UserValidator: AbstractValidator<UserPostModel>
    {
        public UserValidator()
        {
            RuleFor(_ => _.Email)
                .NotNull()
                .EmailAddress();
            RuleFor(_ => _.FullName) // RuleFor(_ => _.FirstName)
                .NotNull();
            RuleFor(_ => _.EmployeeId)
               .NotNull()
               .NotEmpty();
            RuleFor(_ => _.Tel)
              .NotNull();
        }
    }
}
