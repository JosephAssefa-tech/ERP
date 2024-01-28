using Excellerent.ProjectManagement.Domain.DTOs;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.Services.Helpers;
using Excellerent.ProjectManagement.Infrastructure.Specificationes;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Infrastructure.Repositories
{
    public class ProjectRepository : AsyncRepository<Project>, IProjectRepository
    {
        private readonly EPPContext _context;
        private IUnitOfWork _unitOfWork;
        public ProjectRepository(EPPContext context) : base(context)
        {
            _context = context;
            _unitOfWork = UnitOfWork;
        }

        public async Task<IEnumerable<Project>> GetProjectByName(string ProjectName)
        {
            try
            {
                IEnumerable<Project> project = (await base.GetQueryAsync(x => x.ProjectName == ProjectName)).ToList();
                return project;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<Project> GetProjectById(Guid id)
        {
            try
            {
                return (await _context.Project.FindAsync(id));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Project>> GetProjectFullData()
        {
            try
            {
                try
                {
                    return _context.Project.Where(p => !p.IsDeleted).Include(x => x.Client).Include(x => x.ProjectStatus).ToList();

                }
                catch (Exception)
                {

                    throw;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Project>> GetPaginatedProject(Expression<Func<Project, bool>> predicate, PaginationParams paginationParams)

        {
            paginationParams.pageSize = paginationParams.pageSize;
            paginationParams.pageIndex = paginationParams.pageIndex;
            if (paginationParams.SortField != null)
            {
                switch (paginationParams.SortField)
                {
                    case "Project":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            return predicate == null ? (await _context.Project.OrderByDescending(x => x.ProjectName).Where(x => x.IsDeleted.Equals(false)).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                       : (await _context.Project.Where(predicate: predicate).OrderByDescending(x => x.ProjectName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());

                        }
                        else
                        {
                            return predicate == null ? (await _context.Project.OrderBy(x => x.ProjectName).Where(x => x.IsDeleted.Equals(false)).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                            : (await _context.Project.Where(predicate: predicate).OrderBy(x => x.ProjectName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());
                        }
                    case "Client":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            return predicate == null ? (await _context.Project.Where(x => x.IsDeleted.Equals(false)).OrderByDescending(x => x.Client.ClientName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                       : (await _context.Project.Where(predicate: predicate).OrderByDescending(x => x.Client.ClientName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());

                        }
                        else
                        {
                            return predicate == null ? (await _context.Project.Where(x => x.IsDeleted.Equals(false)).OrderBy(x => x.Client.ClientName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                            : (await _context.Project.Where(predicate: predicate).OrderBy(x => x.Client.ClientName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());
                        }
                    case "status":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            return predicate == null ? (await _context.Project.Where(x => x.IsDeleted.Equals(false)).OrderByDescending(x => x.ProjectStatus.StatusName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                       : (await _context.Project.Where(predicate: predicate).OrderByDescending(x => x.ProjectStatus.StatusName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());

                        }
                        else
                        {
                            return predicate == null ? (await _context.Project.Where(x => x.IsDeleted.Equals(false)).OrderBy(x => x.ProjectStatus.StatusName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                            : (await _context.Project.Where(predicate: predicate).OrderBy(x => x.ProjectStatus.StatusName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());
                        }
                    case "supervisor":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            return predicate == null ? (await _context.Project.Where(x => x.IsDeleted.Equals(false)).OrderByDescending(x => x.SupervisorGuid).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                       : (await _context.Project.Where(predicate: predicate).OrderByDescending(x => x.Supervisor.FirstName).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());

                        }
                        else
                        {
                            return predicate == null ? (await _context.Project.Where(x => x.IsDeleted.Equals(false)).OrderBy(x => x.SupervisorGuid).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                            : (await _context.Project.Where(predicate: predicate).OrderBy(x => x.SupervisorGuid).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());
                        }
                    default:
                        return predicate == null ? (await _context.Project.Where(x => x.IsDeleted.Equals(false)).OrderByDescending(x => x.CreatedDate).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
                            : (await _context.Project.Where(predicate: predicate).OrderByDescending(x => x.CreatedDate).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());


                }

            }
            else
            {
                return predicate == null ? (await _context.Project.Where(x => x.IsDeleted.Equals(false)).OrderByDescending(x => x.CreatedDate).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync())
             : (await _context.Project.Where(predicate: predicate).OrderByDescending(x => x.CreatedDate).Skip((int)((paginationParams.pageIndex - 1) * paginationParams.pageSize)).Take((int)paginationParams.pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());

            }
        }

        public async Task addProjectWithResources(Project project, List<AssignResourcEntity> assignResources, string email)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var user = await _context.Users.FirstAsync(u => u.Email == email);
                _context.Project.Add(project);
                await _context.SaveChangesAsync();
                project.CreatedbyUserGuid = user.Guid;
                await _context.SaveChangesAsync();
                foreach (var assignResource in assignResources)
                {
                    assignResource.ProjectGuid = project.Guid;
                    await _context.AssignResources.AddAsync(assignResource);
                    await _context.SaveChangesAsync();
                }

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransaction();
            }

        }

        public async Task<IEnumerable<Project>> GetUserPaginationOfProjects(PaginationParams paginationParams, string email, Boolean isProjectAdmin)
        {
            var user = await _context.Users.FirstAsync(u => u.Email == email);
            var userAssignedProjects = _context.AssignResources.Where(a => a.EmployeeGuid == user.EmployeeId).Select(p => p.ProjectGuid).ToList();
            var spec = new GetProjectsPaginationSpec(paginationParams, user.Guid, user.EmployeeId, userAssignedProjects, isProjectAdmin);
            IEnumerable<Project> projects = null;

            if (paginationParams.SortField == "Project")
            {
                if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                    projects = await GetAsync<object>(spec, x => x.ProjectName.ToLower(), paginationParams.pageIndex,
                                                  paginationParams.pageSize, SharedModules.Seed.SortOrder.Descending);
                else
                    projects = await GetAsync<object>(spec, x => x.ProjectName.ToLower(), paginationParams.pageIndex,
                                     paginationParams.pageSize, SharedModules.Seed.SortOrder.Ascending);
            }
            else if (paginationParams.SortField == "Client")
            {
                if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                    projects = await GetAsync<object>(spec, x => x.Client.ClientName.ToLower(), paginationParams.pageIndex,
                                                  paginationParams.pageSize, SharedModules.Seed.SortOrder.Descending);
                else
                    projects = await GetAsync<object>(spec, x => x.Client.ClientName.ToLower(), paginationParams.pageIndex,
                                     paginationParams.pageSize, SharedModules.Seed.SortOrder.Ascending);
            }
            else if (paginationParams.SortField == "status")
            {
                if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                    projects = await GetAsync<object>(spec, x => x.ProjectStatus.StatusName, paginationParams.pageIndex,
                                                  paginationParams.pageSize, SharedModules.Seed.SortOrder.Descending);
                else
                    projects = await GetAsync<object>(spec, x => x.ProjectStatus.StatusName, paginationParams.pageIndex,
                                     paginationParams.pageSize, SharedModules.Seed.SortOrder.Ascending);
            }
            else if (paginationParams.SortField == "supervisor")
            {
                if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                    projects = await GetAsync<object>(spec, x => x.SupervisorGuid, paginationParams.pageIndex,
                                                  paginationParams.pageSize, SharedModules.Seed.SortOrder.Descending);
                else
                    projects = await GetAsync<object>(spec, x => x.SupervisorGuid, paginationParams.pageIndex,
                                     paginationParams.pageSize, SharedModules.Seed.SortOrder.Ascending);
            }
            else
                projects = await GetAsync<object>(spec, x => x.CreatedDate, paginationParams.pageIndex,
                                                paginationParams.pageSize, SharedModules.Seed.SortOrder.Descending);

            return projects.ToList();

        }


        public async Task UpdateProject(Project project, string email)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var user = await _context.Users.FirstAsync(u => u.Email == email);
                _context.Project.Update(project);
                project.CreatedbyUserGuid = user.Guid;
                await _context.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransaction();
            }

        }
        public async Task<IEnumerable<Project>> GetUserAllProjects(PaginationParams paginationParams, string email, Boolean isProjectAdmin)
        {
            var user = await _context.Users.FirstAsync(u => u.Email == email);
            var userAssignedProjects = _context.AssignResources.Where(a => a.EmployeeGuid == user.EmployeeId).Select(p => p.ProjectGuid).ToList();
            var spec = new GetProjectsPaginationSpec(paginationParams, user.Guid, user.EmployeeId, userAssignedProjects, isProjectAdmin);

            return await FindAsync(spec);

        }

        public async Task<bool> ProjecWithtAssignedResourceExists(Guid projectGuid)
        {
            return await _context.AssignResources.AnyAsync(a => a.ProjectGuid == projectGuid &&  a.IsDeleted==false);

        }

        public async Task<bool> ProjectWithTimeesheetExists(Guid projectGuid)
        {
            var timeEntnryCheck = await _context.TimeEntry.AnyAsync(t => t.ProjectId == projectGuid);
            var tmpTimeEntrieskCheck = await _context.TmpTimeEntries.AnyAsync(t => t.ProjectId == projectGuid);

            return timeEntnryCheck || tmpTimeEntrieskCheck;
        }

        public async Task<ProjectEntity> GetByProjectGuidAsync(Guid id, Guid? empId)
        {
            var project = new ProjectEntity(await _context.Project.FindAsync(id));
            if (empId != null)
            {
                project.AssignDate = _context.AssignResources.Where(c => c.ProjectGuid == id && c.EmployeeGuid == empId).FirstOrDefault().AssignDate;
            }
            else
            {
                project.AssignDate = null;
            }

            return project;
        }




        public async Task<DashboardProjectReportDTO> GetDashbaordResport()
        {
            GetDashboardProjectSpec spec2 = new GetDashboardProjectSpec(ProjectType.Internal);
            int countInternalProjects = await this.CountAsync(spec2);
            GetDashboardProjectSpec spec = new GetDashboardProjectSpec(ProjectType.External);
            int countProjectExternal = await this.CountAsync(spec);

            return  new DashboardProjectReportDTO(countInternalProjects, countProjectExternal);
        }

    }
}
    