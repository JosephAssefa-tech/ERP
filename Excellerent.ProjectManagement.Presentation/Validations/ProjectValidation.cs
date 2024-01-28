using Excellerent.ProjectManagement.Presentation.Models.PostModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Presentation.Validations
{
    public class ProjectValidation : AbstractValidator<ProjectPostModel>
    {
        public ProjectValidation()
        {
            RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Project name must not be empty.");
            RuleFor(x=>x.ProjectName).MaximumLength(70);
            RuleFor(x => x.ProjectName).MinimumLength(2);
            When(x=>x.EndDate.ToString()!="" , () =>
            {
                RuleFor(x => Convert.ToDateTime(x.EndDate)).GreaterThan(x => x.StartDate).WithMessage("End date should be greater than start date.");
            });
            RuleFor(x => x.ClientGuid).NotEmpty().WithMessage("Client name must not be empty.");
            RuleFor(x => x.ProjectStatusGuid).NotEmpty().WithMessage("Project status must not be empty.");
            RuleFor(x => x.SupervisorGuid).NotEmpty().WithMessage("Supervisor must not be empty.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date must not be empty.");
            RuleFor(x => x.ClientGuid).NotEmpty().WithMessage("Client Name must not be empty.");
        }

      
    }
}
