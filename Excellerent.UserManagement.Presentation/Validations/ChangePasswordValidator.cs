
using Excellerent.UserManagement.Presentation.Models.PostModel;
using FluentValidation;

namespace Excellerent.UserManagement.Presentation.Validations
{
    public class ChangePasswordValidator: AbstractValidator<ChangePasswordPostDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(model => model.OldPassword)
                .NotNull()
                .NotEmpty().WithMessage("{Password} is required.")
                .MinimumLength(8).WithMessage("{Password} must not be below 8 characters.");
            RuleFor(model => model.Password)
                .NotNull()
                .NotEmpty().WithMessage("{Password} is required.")
                .MinimumLength(8).WithMessage("{Password} must not be below 8 characters.");
            RuleFor(model => model.ConfirmPassword)
                .NotNull()
                .NotEmpty().WithMessage("{Password} is required.")
                .MinimumLength(8).WithMessage("{Password} must not be below 8 characters.")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
