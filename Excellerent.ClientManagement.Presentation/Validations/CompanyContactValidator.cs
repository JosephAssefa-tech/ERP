using Excellerent.ClientManagement.Presentation.Models.PostModels;
using FluentValidation;

namespace Excellerent.ClientManagement.Presentation.Validations
{
    public class CompanyContactValidator : AbstractValidator<ComapanyContactPostModel>
    {
        public CompanyContactValidator()
        {
            RuleFor(x => x.ContactPersonGuid).NotEmpty().WithMessage("ContactPersonGuid must not be empty");
        }
    }
}