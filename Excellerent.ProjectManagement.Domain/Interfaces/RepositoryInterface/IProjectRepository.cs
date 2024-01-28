using Excellerent.ProjectManagement.Domain.DTOs;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.Services.Helpers;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface
{
    public interface IProjectRepository : IAsyncRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectByName(string ProjectName);
        Task<Project> GetProjectById(Guid id);
        Task<IEnumerable<Project>> GetProjectFullData();
        Task<IEnumerable<Project>> GetPaginatedProject(Expression<Func<Project, Boolean>> predicate, PaginationParams paginationParams);
        Task addProjectWithResources(Project project, List<AssignResourcEntity> assignResources, string email);
        Task<IEnumerable< Project>> GetUserPaginationOfProjects(PaginationParams paginationParams, string email, Boolean  isProjectAdmin);
        Task UpdateProject(Project project, string email);
        Task<IEnumerable<Project>>  GetUserAllProjects(PaginationParams paginationParams, string email, Boolean isProjectAdmin);

        Task<bool> ProjecWithtAssignedResourceExists(Guid projectGuid);
        Task<bool> ProjectWithTimeesheetExists(Guid projectGuid);
        Task<ProjectEntity> GetByProjectGuidAsync(Guid id,Guid? guid);
        Task<DashboardProjectReportDTO> GetDashbaordResport();
    }
}
