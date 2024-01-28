using Excellerent.ClientManagement.Presentation.Models.PostModels;
using FluentValidation;

namespace Excellerent.ClientManagement.Presentation.Validations
{
    public class BillingAddressValidator : AbstractValidator<BillingAddressPostModel>
    {
        public BillingAddressValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Billing Address name must not be empty.");

            RuleFor(x => x.Affliation).NotEmpty().WithMessage("Affliation must not be empty.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country must not be empty.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City must not be empty.");
        }
    }
}