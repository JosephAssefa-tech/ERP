using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface
{
    public interface IProjectStatusService : ICRUD<ProjectStatusEntity, ProjectStatus>

    {
        Task<ProjectStatus> GetProjectStatusById(Guid id);
    }
}
