using Excellerent.Usermanagement.Domain.Models;
using Excellerent.UserManagement.Presentation.Models.PutModels;
using Excellerent.UserManagement.Presentation.Models.RequestDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Validations
{
    public class GroupSetPatchRequestValidator : AbstractValidator<GroupSetPatchModel>
    {
        public GroupSetPatchRequestValidator()
        {
            RuleFor(model => model.Description)
                .NotNull()
                .MinimumLength(2);
        }
    }
}
