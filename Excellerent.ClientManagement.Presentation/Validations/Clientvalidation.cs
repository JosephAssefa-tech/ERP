using Excellerent.ClientManagement.Presentation.Models.PostModels;
using FluentValidation;

namespace Excellerent.ClientManagement.Presentation.Validations
{
    public class ClientValidation : AbstractValidator<ClientPostModel>
    {
        public ClientValidation()
        {
            RuleFor(x => x.ClientName).NotEmpty().WithMessage("Client name must not be empty");
            RuleFor(x => x.ClientName).Length(2, 70).WithMessage("Client name invalid length ");
            RuleFor(x => x.Description).MaximumLength(250).WithMessage("Maximum charactures allowed is 250.");
            RuleFor(x => x.SalesPersonGuid).NotEmpty().WithMessage("Salesperson must not empty");
            RuleForEach(x => x.ClientContacts).SetValidator(new ClientContactValidation());
            RuleForEach(x => x.BillingAddress).SetValidator(new BillingAddressValidator());
            RuleForEach(x => x.OperatingAddress).SetValidator(new OperatingAddressValidator());
            RuleForEach(x => x.CompanyContacts).SetValidator(new CompanyContactValidator());
        }

    }
}