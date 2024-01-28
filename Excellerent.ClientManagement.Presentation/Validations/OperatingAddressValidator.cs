using Excellerent.ClientManagement.Presentation.Models.PostModels;
using FluentValidation;

namespace Excellerent.ClientManagement.Presentation.Validations
{
    public class OperatingAddressValidator : FluentValidation.AbstractValidator<OperatingAddressPostModel>
    {
        public OperatingAddressValidator()
        {
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country must not be empty.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City must not be empty.");
        }
    }
}