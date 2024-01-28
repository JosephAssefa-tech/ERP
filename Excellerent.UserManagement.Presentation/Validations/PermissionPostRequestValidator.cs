
using Excellerent.UserManagement.Presentation.Models.RequestDtos;
using FluentValidation;

namespace Excellerent.UserManagement.Presentation.Validations
{
    public class PermissionPostRequestValidator : AbstractValidator<PermissionPostRequestDto>
    {
        public PermissionPostRequestValidator()
        {
            RuleFor(model => model.PermissionCode)
                .NotNull()
                .MinimumLength(2);
            RuleFor(model => model.Name)
                .NotNull()
                .MinimumLength(2);
            RuleFor(model => model.KeyValue)
                .NotNull()
                .MinimumLength(2);
            RuleFor(model => model.Level)
                .NotNull();
        }
    }
}
