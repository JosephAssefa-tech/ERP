using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Helpers;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Infrastructure.Specificationes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Infrastructure.Repositories
{
    public class TimesheetApprovalRepository : AsyncRepository<TimesheetApproval>, ITimesheetApprovalRepository, ITimesheetForNotificationRepository
    {
        private readonly EPPContext _context;

        public TimesheetApprovalRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimesheetApproval>> GetTimesheetApprovalsForUpdateOrDelete(Guid timesheetId)
        {
            return await _context.TimesheetAprovals.Where(tsa => tsa.TimesheetId == timesheetId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TimesheetApproval>> AllTimesheetAproval(Expression<Func<TimesheetApproval, bool>> predicate, PaginationParams paginationParams)
        {
            int itemPerPage = paginationParams.PageSize ?? 10;
            int PageIndex = paginationParams.PageIndex ?? 1;
            if (paginationParams.status == ApprovalStatus.Requested)
            {
                if (paginationParams.SortBy != null)
                {
                    switch (paginationParams.SortBy)
                    {
                        case "Name":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderByDescending(x => x.Timesheet.Employee.FullName) // .OrderByDescending(x => x.Timesheet.Employee.FirstName)
                                   .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderByDescending(x => x.Timesheet.Employee.FullName) // .OrderByDescending(x => x.Timesheet.Employee.FirstName)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Timesheet.Employee.FullName) //.OrderBy(x => x.Timesheet.Employee.FirstName)
                                 .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Timesheet.Employee.FullName) //.OrderBy(x => x.Timesheet.Employee.FirstName)
                                   .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        case "DateRange":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                                               .OrderByDescending(x => x.Timesheet.FromDate)
                                                             .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                                               .Include(y => y.Timesheet).ToListAsync())
                                                                : (await _context.TimesheetAprovals.
                                                                Where(predicate: predicate).OrderByDescending(x => x.Timesheet.FromDate)
                                                              .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Timesheet.FromDate)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Timesheet.FromDate)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        case "Client":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                .OrderByDescending(x => x.Project.Client.ClientName)
                               .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                .Include(y => y.Timesheet).ToListAsync())
                                 : (await _context.TimesheetAprovals.
                                 Where(predicate: predicate).OrderByDescending(x => x.Project.Client.ClientName)
                                 .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Project.Client.ClientName)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Project.Client.ClientName)
                                   .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }

                        case "Project":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                .OrderByDescending(x => x.Project.ProjectName)
                               .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                .Include(y => y.Timesheet).ToListAsync())
                                 : (await _context.TimesheetAprovals.
                                 Where(predicate: predicate).OrderByDescending(x => x.Project.ProjectName)
                                .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                .OrderBy(x => x.Project.ProjectName)
                               .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                .Include(y => y.Timesheet).ToListAsync())
                                 : (await _context.TimesheetAprovals.
                                 Where(predicate: predicate).OrderBy(x => x.Project.ProjectName)
                                .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }

                        default:
                            return predicate == null ? (await _context.TimesheetAprovals
                             .OrderByDescending(x => x.CreatedDate)
                            .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                             .Include(y => y.Timesheet).ToListAsync())
                              : (await _context.TimesheetAprovals.
                              Where(predicate: predicate).OrderByDescending(x => x.CreatedDate)
                              .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                    }
                }
                else
                {
                    return predicate == null ? (await _context.TimesheetAprovals.OrderBy(x => x.CreatedDate)
                        .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                        .Include(y => y.Timesheet).ToListAsync())
                        : (await _context.TimesheetAprovals.Where(predicate: predicate)
                        .OrderBy(x => x.CreatedDate)
                        .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                }

            }
            else
            {


                if (paginationParams.SortBy != null)
                {
                    switch (paginationParams.SortBy)
                    {
                        case "Name":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderByDescending(x => x.Timesheet.Employee.FullName) // .OrderByDescending(x => x.Timesheet.Employee.FirstName)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderByDescending(x => x.Timesheet.Employee.FullName) // OrderByDescending(x => x.Timesheet.Employee.FirstName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Timesheet.Employee.FullName) // .OrderBy(x => x.Timesheet.Employee.FirstName)
                                    .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Timesheet.Employee.FullName) //OrderBy(x => x.Timesheet.Employee.FirstName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        case "DateRange":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                                               .OrderByDescending(x => x.Timesheet.FromDate)
                                                             .Skip((PageIndex - 1) * itemPerPage)
                                                         .Take(itemPerPage).Include(x => x.Project)
                                                               .Include(y => y.Timesheet).ToListAsync())
                                                                : (await _context.TimesheetAprovals.
                                                                Where(predicate: predicate).OrderByDescending(x => x.Timesheet.FromDate)
                                                                 .Skip((PageIndex - 1) * itemPerPage)
                                                        .Take(itemPerPage)
                                                        .Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Timesheet.FromDate)
                                     .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Timesheet.FromDate)
                                     .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        case "Client":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                .OrderByDescending(x => x.Project.Client.ClientName)
                                .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                                .Include(y => y.Timesheet).ToListAsync())
                                 : (await _context.TimesheetAprovals.
                                 Where(predicate: predicate).OrderByDescending(x => x.Project.Client.ClientName)
                                 .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Project.Client.ClientName)
                                  .Skip((PageIndex - 1) * itemPerPage)
                                   .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Project.Client.ClientName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }

                        case "Project":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {

                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderByDescending(x => x.Project.ProjectName)
                                  .Skip((PageIndex - 1) * itemPerPage)
                            .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderByDescending(x => x.Project.ProjectName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                            .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Project.ProjectName)
                                  .Skip((PageIndex - 1) * itemPerPage)
                            .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Project.ProjectName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                            .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                        default:
                            return predicate == null ? (await _context.TimesheetAprovals
                             .OrderByDescending(x => x.CreatedDate)
                              .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                             .Include(y => y.Timesheet).ToListAsync())
                              : (await _context.TimesheetAprovals.
                              Where(predicate: predicate).OrderByDescending(x => x.CreatedDate)
                               .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                    }
                }
                else
                {
                    return predicate == null ? (await _context.TimesheetAprovals.OrderByDescending(x => x.Timesheet.FromDate)
                         .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                        .Include(y => y.Timesheet).ToListAsync())
                        : (await _context.TimesheetAprovals.Where(predicate: predicate)
                        .OrderByDescending(x => x.Timesheet.FromDate)
                        .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                }
            }

        }

        public async Task<IEnumerable<TimesheetApproval>> CountTotals(Expression<Func<TimesheetApproval, bool>> predicate)
        {
            return predicate == null ? (await _context.TimesheetAprovals.ToListAsync())
                                      : (await _context.TimesheetAprovals.Where(predicate: predicate).ToListAsync());
        }

        public async Task<int> GetTimesheetApprovalTotalHour(Expression<Func<TimeEntry, bool>> predicate)
        {
            return await _context.TimeEntry.Where(predicate).SumAsync<TimeEntry>(x => x.Hour);
        }

        public async Task<TimesheetApproval> Get(Guid id)
        {
            return await _context.TimesheetAprovals.FindAsync(id);
        }

        public async Task<bool> Update(TimesheetApproval t)
        {
            _context.TimesheetAprovals.Update(t);
            _context.SaveChanges();
            return true;
        }

        public async Task UpdateProjectApprovalStatus(TimesheetApproval timesheetApproval)
        {
            _context.TimesheetAprovals.Attach(timesheetApproval);

            _context.Entry(timesheetApproval).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TimesheetApproval>> GetUserTimesheetApprovals(UserTimesheetApprovalParamDto paginationParams, Guid employeeGuId)
        {

            IEnumerable<TimesheetApproval> userTimeApprovalHistory = null;
            var spec = new UserTimesheetApprovalsSpec(paginationParams, employeeGuId);
            if (paginationParams.SortField == "Project")
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.Project.ProjectName, paginationParams.PageIndex,
                                          paginationParams.PageSize, paginationParams.Sort);
            else if (paginationParams.SortField == "Client")
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.Project.Client.ClientName, paginationParams.PageIndex,
                                         paginationParams.PageSize, paginationParams.Sort);
            else if (paginationParams.SortField == "Status")
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.Status, paginationParams.PageIndex,
                                       paginationParams.PageSize, paginationParams.Sort);
            else if (paginationParams.SortField == "DateRange")
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.Timesheet.ToDate, paginationParams.PageIndex,
                                          paginationParams.PageSize, paginationParams.Sort);
            else
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.CreatedDate, paginationParams.PageIndex,
                                           paginationParams.PageSize, SortOrder.Descending);
            return userTimeApprovalHistory;
        }

        public async Task<dynamic> GetUserTimesheetApprovalsPageData(UserTimesheetApprovalParamDto paginationParams, Guid employeeGuId)
        {
            var spec = new UserTimesheetApprovalsSpec(paginationParams, employeeGuId);
            int totalCount = await CountAsync(spec);
            if (paginationParams.PageIndex == 1 && paginationParams.ProjectFilters == null
                    && paginationParams.ClientFilters == null && paginationParams.StatusFilter == null)
            {
                var userAllTimesheetHistory = await FindAsync(spec);
                var projectFilter = userAllTimesheetHistory.GroupBy(x => x.ProjectId).Select(x => x.First())
                                                      .Select(x => new { x.ProjectId, x.Project.ProjectName })
                                                       .OrderBy(x => x.ProjectName).ToList();
                var clientFilter = userAllTimesheetHistory.GroupBy(x => x.Project.Client.ClientName).Select(x => x.First())
                                                       .Select(x => new { x.Project.Client.Guid, x.Project.Client.ClientName })
                                                        .OrderBy(x => x.ClientName).ToList();
                var statusFilter = userAllTimesheetHistory.GroupBy(x => x.Status).Select(x => x.First()).Select(x => x.Status)
                                                          .OrderBy(x => x.ToString()).ToList();

                return new
                {
                    Filters = new
                    {
                        ProjectFilter = projectFilter,
                        ClientFilter = clientFilter,
                        StatusFilter = statusFilter
                    },
                    TotalDataCount = totalCount
                };
            }

            return new
            {
                Filters = new { },
                TotalDataCount = totalCount
            };

        }
        public async Task<int> CountTimesheetApprovals(Expression<Func<TimesheetApproval, bool>> predicate)
        {
            return await _context.TimesheetAprovals.Where(predicate).CountAsync();
        }

        public async Task<IEnumerable<TimesheetApproval>> GetForNotification(Guid projectGuid, Guid employeeGuid, DateTime start, DateTime end)
        {
            return (await GetEmployeeForNotification(employeeGuid, start, end))
                .Where(t =>
                    t.ProjectId == projectGuid);
        }

        public async Task<IEnumerable<TimesheetApproval>> GetEmployeeForNotification(Guid employeeGuid, DateTime start, DateTime end)
        {
            return await _context.Set<TimesheetApproval>().Include(t => t.Timesheet)
                .Where(t =>
                    !t.IsDeleted
                    && t.Timesheet.EmployeeId == employeeGuid
                    && t.Status != ApprovalStatus.Rejected
                    && t.Timesheet.FromDate <= end
                    && t.Timesheet.ToDate >= start).ToListAsync();
        }


        public async Task<List<TimesheetApproval>> DashboardTimesheetProjectApproved(DashboardProjectParams param)
        {
            var query = _context.TimesheetAprovals.
             Include(t => t.Project).ThenInclude(t => t.ProjectStatus).Include(p => p.Project.AssignResources)
            .Include(t => t.Project.Client).Include(t => t.Timesheet).ThenInclude(t => t.TimeEntry)
             .Include(t => t.Project.Supervisor)
             .Where(t => t.IsDeleted == false &&
             t.Project.ProjectStatus.StatusName == "Active" && t.Project.ProjectName != "Leave" &&
            t.Project.Client.ClientName != "Leave" && t.Status == ApprovalStatus.Approved &&
            t.Project.ProjectType == param.Projecttype).AsQueryable();

            if (param.SearchKey != null)
                query = query.Where(p => p.Project.ProjectName.ToLower().Trim().Contains(param.SearchKey.ToLower().Trim()));

            if (param.SupervisorIds != null)
                query = query.Where(p => param.SupervisorIds.Contains(p.Project.SupervisorGuid));

            if (param.ClientIds != null)
                query = query.Where(p => param.ClientIds.Contains(p.Project.ClientGuid));

            var d = (await _context.TimesheetAprovals.ToListAsync()).GroupBy(p => p.ProjectId).ToList();

            return await query.ToListAsync();

        }

        public async Task<dynamic> GetProjectDashboardData(DashboardProjectParams param)
        {
            var timesheetAprovals = await _context.TimesheetAprovals.
                  Include(t => t.Project).ThenInclude(t => t.ProjectStatus)
             .Include(t => t.Project.Client).Include(t => t.Project.Supervisor)
              .Where(t => t.IsDeleted == false && t.Project.ProjectStatus.StatusName == "Active" &
              t.Project.ProjectName != "Leave" && t.Project.Client.ClientName != "Leave" && t.Project.ProjectType == param.Projecttype &&
              t.Status == ApprovalStatus.Approved).ToListAsync();

            var data = timesheetAprovals.Where(t => t.Project.ProjectType == param.Projecttype).GroupBy(t => t.Project).ToList();

            int totalCount = 0;
            if (param.SupervisorIds != null && param.ClientIds != null && param.SearchKey != null)
            {
                totalCount = data.Where(p => p.Key.ProjectName.ToLower().Trim().Contains(param.SearchKey.ToLower().Trim())
                    && param.SupervisorIds.Contains(p.Key.SupervisorGuid) && param.ClientIds.Contains(p.Key.ClientGuid)).Count();
            }
            else if (param.SupervisorIds != null && param.ClientIds != null)
            {
                totalCount = data.Where(p => param.SupervisorIds.Contains(p.Key.SupervisorGuid) &&
                                  param.ClientIds.Contains(p.Key.ClientGuid)).Count();
            }
            else if (param.SupervisorIds != null && param.SearchKey != null)
                totalCount = data.Where(p => p.Key.ProjectName.ToLower().Trim().Contains(param.SearchKey.ToLower().Trim())
                 && param.SupervisorIds.Contains(p.Key.SupervisorGuid)).Count();
            else if (param.ClientIds != null && param.SearchKey != null)
            {
                totalCount = data.Where(p => p.Key.ProjectName.ToLower().Trim().Contains(param.SearchKey.ToLower().Trim())
                                   && param.ClientIds.Contains(p.Key.ClientGuid)).Count();
            }
            else if (param.SupervisorIds != null)
            {
                totalCount = data.Where(p => param.SupervisorIds.Contains(p.Key.SupervisorGuid)).Count();
            }
            else if (param.ClientIds != null)
            {
                totalCount = data.Where(p => param.ClientIds.Contains(p.Key.ClientGuid)).Count();
            }
            else if (param.SearchKey != null)
            {
                totalCount = data.Where(p => p.Key.ProjectName.ToLower().Trim().Contains(param.SearchKey.ToLower().Trim())).Count();
            }
            else
                totalCount = data.Count();



            if (param.Projecttype == ProjectType.External && param.SearchKey == null && param.PageIndex == 1 && param.SupervisorIds == null && param.ClientIds == null)
            {


                var suppervisoExternalFilter = data.GroupBy(x => x.Key.Supervisor).Select(x => x.First())
                                         .Select(x => new { x.Key.SupervisorGuid, x.Key.Supervisor.FullName })
                                          .OrderBy(x => x.FullName).ToList();

                var clientExternalFilter = data.GroupBy(x => x.Key.Client).Select(x => x.First())
                                             .Select(x => new { x.Key.Client.Guid, x.Key.Client.ClientName })
                                              .OrderBy(x => x.ClientName).ToList();

                return new
                {
                    Filters = new
                    {
                        SupervisorFilter = suppervisoExternalFilter,
                        ClientFilter = clientExternalFilter,

                    },
                    TotalDataCount = totalCount
                };
            }

            else if (param.Projecttype == ProjectType.Internal && param.SearchKey == null && param.PageIndex == 1 && param.SupervisorIds == null && param.ClientIds == null)
            {

                var suppervisodataInternalFilter = data.GroupBy(x => x.Key.Supervisor).Select(x => x.First())
                                 .Select(x => new { x.Key.SupervisorGuid, x.Key.Supervisor.FullName })
                                  .OrderBy(x => x.FullName).ToList();

                return new
                {
                    Filters = new
                    {
                        SupervisorFilter = suppervisodataInternalFilter
                    },
                    TotalDataCount = totalCount
                };
            }


            return new
            {
                Filters = new { },
                TotalDataCount = totalCount
            };


        }


    }

}




