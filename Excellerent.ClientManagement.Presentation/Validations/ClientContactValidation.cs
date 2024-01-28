using Excellerent.ClientManagement.Presentation.Models.PostModels;
using FluentValidation;

namespace Excellerent.ClientManagement.Presentation.Validations
{
    public class ClientContactValidation : AbstractValidator<ClientContactPostModel>
    {
        public ClientContactValidation()
        {
            RuleFor(x => x.ContactPersonName).NotEmpty().WithMessage("Client Contact Person name must not be empty.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Client Contact Person Email) must not be empty.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Client Contact Person PhoneNumber must not be empty.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid Client Contact Person Email");
        }
    }
}