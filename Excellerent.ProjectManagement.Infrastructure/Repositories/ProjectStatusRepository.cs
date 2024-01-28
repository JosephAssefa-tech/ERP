using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Infrastructure.Repositories
{
    public class ProjectStatusRepository : AsyncRepository<ProjectStatus>, IProjectStatusRepository
    {
        private readonly EPPContext _context;
        public ProjectStatusRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProjectStatus> GetProjectStatusById(Guid statusId)
        {
            var status = await _context.ProjectStatus.FindAsync(statusId);
            return status;
        }
    }
}
