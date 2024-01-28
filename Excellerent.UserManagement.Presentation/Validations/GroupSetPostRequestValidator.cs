using Excellerent.UserManagement.Presentation.Models.RequestDtos;
using FluentValidation;

namespace Excellerent.UserManagement.Presentation.Validations
{
    public class GroupSetPostRequestValidator : AbstractValidator<GroupSetPostRequestDto>
    {
        public GroupSetPostRequestValidator()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .MinimumLength(2);
        }
    }
}
