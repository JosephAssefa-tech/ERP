using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Helpers;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Service
{
    public interface ITimesheetApprovalService : ICRUD<TimesheetApprovalEntity, TimesheetApproval>
    {
        Task<IEnumerable<TimesheetApprovalEntity>> GetTimesheetApprovalStatus(Guid tsGuid);

        Task<IEnumerable<TimesheetApprovalEntity>> GetTimesheetApprovalForUpdateOrDelete(Guid timesheetId);

        Task<IEnumerable<TimesheetApproval>> GetAllTimesheetApproval();

        Task<PredicatedResponseDTO> GetTimesheetApprovals(PaginationParams paginationParams);

        Task<ResponseDTO> GetProjectApprovalStatus(Guid timesheetId, Guid projectId);

        Task<ResponseDTO> UpdateProjectApprovalStatus(TimesheetApprovalEntity entity);

        Task<ResponseDTO> TimesheetApprovalBulkApprove(List<Guid> guidList);

        Task<TimesheetApproval> Get(Guid id);
        
        Task<bool> Update(TimesheetApproval t);
        Task<PredicatedResponseDTO> AllTimesheetAproval(Expression<Func<TimesheetApproval, bool>> predicate, PaginationParams paginationParams);
        Task<ResponseDTO> GetAprovalProject();
        Task<ResponseDTO> GetAprovalClient();
        Task<ResponseDTO> GetUserTimesheetApprovalHistory(UserTimesheetApprovalParamDto paginationParams, Guid employeeGuId);
        Task<ResponseDTO> GetDashboardTimesheetApprovedProject(DashboardProjectParams param);
    }
}
