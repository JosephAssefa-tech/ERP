using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface
{
    public interface IProjectStatusRepository : IAsyncRepository<ProjectStatus>
    {
       Task<ProjectStatus> GetProjectStatusById(Guid statusId);
    }
}
