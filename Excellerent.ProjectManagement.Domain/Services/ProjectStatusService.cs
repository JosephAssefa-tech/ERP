using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Services
{
    public class ProjectStatusService : CRUD<ProjectStatusEntity, ProjectStatus>, IProjectStatusService
    {


        private readonly IProjectStatusRepository _projectStatusRepository;


        public ProjectStatusService(IProjectStatusRepository projectStatusRepository) : base(projectStatusRepository)
        {
            _projectStatusRepository = projectStatusRepository;

        }

     

        public async Task<ProjectStatus> GetProjectStatusById(Guid id)
        {
            var status = await _projectStatusRepository.GetProjectStatusById(id);

            return status;
        }
      
    }
}
