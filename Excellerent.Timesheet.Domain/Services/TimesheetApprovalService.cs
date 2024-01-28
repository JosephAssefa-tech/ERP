using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Helpers;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Utilities;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Services
{
    public class TimesheetApprovalService : CRUD<TimesheetApprovalEntity, TimesheetApproval>, ITimesheetApprovalService
    {
        private readonly IEmployeeRepository _iEmployeeRepository;
        private readonly IClientDetailsRepository _iClientDetailsRepository;
        private readonly ITimesheetApprovalRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly ITimeSheetService _timeSheetService;
        private readonly IProjectService _projectService;
        private readonly IClientDetailsService _clientDetailsService;


        public TimesheetApprovalService(ITimesheetApprovalRepository repository, IClientDetailsRepository clientDetailsRepository, IProjectRepository projectRepository, IEmployeeRepository employeeRepository, ITimeSheetRepository timeSheetRepository, ITimeSheetService timeSheetService, IProjectService projectService, IClientDetailsService clientDetailsService) : base(repository)

        {

            _iClientDetailsRepository = clientDetailsRepository;
            _repository = repository;
            _projectRepository = projectRepository;
            _iEmployeeRepository = employeeRepository;
            _timeSheetRepository = timeSheetRepository;
            _timeSheetService = timeSheetService;
            _projectService = projectService;
            _clientDetailsService = clientDetailsService;
        }

        public async Task<IEnumerable<TimesheetApprovalEntity>> GetTimesheetApprovalStatus(Guid tsGuid)
        {
            var timesheetApprovals = (await _repository.FindAsync(x => x.TimesheetId == tsGuid)).ToList();

            foreach (TimesheetApproval tsa in timesheetApprovals)
            {
                tsa.Timesheet = await _timeSheetRepository.GetTimeSheet((ts => ts.Guid == tsa.TimesheetId));
                if (tsa.Timesheet != null)
                {
                    tsa.Timesheet.Employee = _iEmployeeRepository.GetEmployeesById(tsa.Timesheet.EmployeeId);
                }
                tsa.Project = await _projectRepository.GetProjectById(tsa.ProjectId);
            }

            return timesheetApprovals.Select(tsa => new TimesheetApprovalEntity(tsa));
        }

        public async Task<IEnumerable<TimesheetApprovalEntity>> GetTimesheetApprovalForUpdateOrDelete(Guid timesheetId)
        {
            var timesheetApprovals = (await _repository.GetTimesheetApprovalsForUpdateOrDelete(timesheetId));

            foreach (TimesheetApproval tsa in timesheetApprovals)
            {
                tsa.Timesheet = await _timeSheetRepository.GetTimeSheet((ts => ts.Guid == tsa.TimesheetId));
                if (tsa.Timesheet != null)
                {
                    tsa.Timesheet.Employee = _iEmployeeRepository.GetEmployeesById(tsa.Timesheet.EmployeeId);
                }
                tsa.Project = await _projectRepository.GetProjectById(tsa.ProjectId);
            }

            return timesheetApprovals.Select(tsa => new TimesheetApprovalEntity(tsa));
        }

        public async Task<IEnumerable<TimesheetApproval>> GetAllTimesheetApproval()
        {
            var timesheetApprovals = (await _repository.GetAllAsync()).ToList();

            return timesheetApprovals;
        }

        public async Task<PredicatedResponseDTO> GetTimesheetApprovals(PaginationParams paginationParams)
        {

            int itemPerPage = paginationParams.PageSize ?? 10;
            int PageIndex = paginationParams.PageIndex ?? 1;



            // return (await _repository.FindAsync(x => x.Status == status)).ToList().Select(a => new TimesheetApprovalEntity(a));
            var timesheetApprovals = await _repository.GetAsync<object>(x => x.Status == paginationParams.status, x => x.CreatedDate, PageIndex, itemPerPage, paginationParams.sort);
            if (paginationParams.Week != null)

            {
                DateTime localDateTime = (DateTime)paginationParams.Week;
                localDateTime = (DateTime)localDateTime.Date;
                DateTime fromDate = DateTimeUtility.GetWeeksFirstDate(localDateTime);
                DateTime toDate = DateTimeUtility.GetWeeksLastDate(localDateTime);
                timesheetApprovals = timesheetApprovals.Where(x => (x.Timesheet.FromDate == fromDate) && (x.Timesheet.ToDate == toDate));
            }

            int TotalRowCount = await _repository.CountAsync();
            return new PredicatedResponseDTO
            {
                Data = timesheetApprovals.Select(a => new TimesheetApprovalEntity(a).MapToDto()),
                PageIndex = PageIndex,
                PageSize = itemPerPage,
                TotalRecord = TotalRowCount,
                TotalPage = TotalRowCount % itemPerPage == 0 ? TotalRowCount / itemPerPage : TotalRowCount / itemPerPage + 1
            };
        }

        public async Task<ResponseDTO> TimesheetApprovalBulkApprove(List<Guid> guidList)
        {
            List<Guid> approvedTimesheetId = new List<Guid>();
            foreach (Guid guid in guidList)
            {
                TimesheetApproval timesheetApproval = await _repository.FindOneAsync(t => t.Guid == guid);
                if (timesheetApproval != null)
                {
                    timesheetApproval.Status = ApprovalStatus.Approved;
                    await _repository.Update(timesheetApproval);
                    int notApprovedCount = await _repository.CountAsync(t => (t.Status != ApprovalStatus.Approved && t.TimesheetId == timesheetApproval.TimesheetId));
                    // int notApprovedCount = await _repository.CountTimesheetApprovals(t => (t.Status != ApprovalStatus.Approved && t.TimesheetId == timesheetApproval.TimesheetId));
                    if (notApprovedCount == 0)
                    {
                        TimeSheet timesheet = await _timeSheetService.GetTimeSheetById(timesheetApproval.TimesheetId);
                        if (timesheet != null)
                        {
                            timesheet.Status = 1; //TODO: Update to approve status
                            // await _timeSheetService.UpdateAsync(timesheet);
                            await _timeSheetService.Update(timesheet);
                        }
                    }

                    if (!approvedTimesheetId.Contains(timesheetApproval.TimesheetId))
                    {
                        approvedTimesheetId.Add(timesheetApproval.TimesheetId);
                    }
                }
            }
            return new ResponseDTO(ResponseStatus.Success, "Bulk approval", approvedTimesheetId);
        }

        public async Task<TimesheetApproval> Get(Guid id)
        {
            TimesheetApproval m = await _repository.Get(id);
            return m;
        }

        public async Task<bool> Update(TimesheetApproval t)
        {
            return await _repository.Update(t);
        }

        public async Task<ResponseDTO> GetProjectApprovalStatus(Guid timesheetId, Guid projectId)
        {
            try
            {
                return new ResponseDTO(ResponseStatus.Success, "project approval request", (await GetTimesheetApprovalStatus(timesheetId)).ToList().Find(x => x.ProjectId == projectId));
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Success, ex.Message, null);
            }
        }
        public async Task<ResponseDTO> UpdateProjectApprovalStatus(TimesheetApprovalEntity entity)
        {
            try
            {
                await _repository.UpdateProjectApprovalStatus(entity.MapToModel());

                var approvedProjects = await GetTimesheetApprovalStatus(entity.TimesheetId);

                return new ResponseDTO(ResponseStatus.Success, "status updated successfully", entity);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.InnerException.ToString(), entity);
            }

        }

        public async Task<PredicatedResponseDTO> AllTimesheetAproval(Expression<Func<TimesheetApproval, bool>> predicate, PaginationParams paginationParams)

        {

            int itemPerPage = paginationParams.PageSize ?? 10;
            int PageIndex = paginationParams.PageIndex ?? 1;


            List<TimesheetApproval> listapproved = new List<TimesheetApproval>();
            try
            {

                var allApprovalTimesheets = await _repository.AllTimesheetAproval(predicate, paginationParams);
                foreach (var allApprovalTimesheet in allApprovalTimesheets)
                {
                    allApprovalTimesheet.Project.Client = _iClientDetailsRepository.GetByGuidAsync(allApprovalTimesheet.Project.ClientGuid).Result;
                    allApprovalTimesheet.Timesheet.Employee = _iEmployeeRepository.GetByGuidAsync(allApprovalTimesheet.Timesheet.EmployeeId).Result;
                    listapproved.Add(allApprovalTimesheet);
                }
                var totalItems = await _repository.CountTotals(predicate);

                List<TimesheetApprovalDto> timesheetApprovalDtos = new List<TimesheetApprovalDto>();
                foreach (var d in listapproved)
                {
                    // var hours = d.Timesheet.TimeEntry.Sum(x => x.Hour);
                    var hours = GetTimesheetApprovalTotalHour(d.TimesheetId, d.ProjectId);
                    timesheetApprovalDtos.Add(new TimesheetApprovalDto
                    {
                        TimesheetApprovalGuid = d.Guid,
                        ClientName = d.Project.Client.ClientName,
                        ProjectName = d.Project.ProjectName,
                        ProjectId = d.ProjectId,
                        TimesheetId = d.TimesheetId,
                        EmployeeName = d.Timesheet.Employee.FullName, // d.Timesheet.Employee.FirstName + "  " + d.Timesheet.Employee.FatherName,
                        Status = d.Status,
                        Comment = d.Comment,
                        ToDate = d.Timesheet.ToDate,
                        FromDate = d.Timesheet.FromDate,
                        CreatedDate = d.CreatedDate,
                        TotalHours = hours.Result
                    });
                }



                int TotalRowCount = totalItems.Count();

                return new PredicatedResponseDTO
                {
                    Data = timesheetApprovalDtos,
                    TotalRecord = TotalRowCount,
                    PageIndex = PageIndex,
                    PageSize = itemPerPage,
                    TotalPage = TotalRowCount % itemPerPage == 0 ? TotalRowCount / itemPerPage : TotalRowCount / itemPerPage + 1
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> GetTimesheetApprovalTotalHour(Guid timesheetId, Guid projectId)
        {
            // var predicate = PredicateBuilder.New<TimesheetApproval>();
            // predicate = predicate.And(x => x.Timesheet.Guid == timesheetId).And(x => x.ProjectId == projectId);

            var predicate = PredicateBuilder.New<TimeEntry>();
            predicate = predicate.And(x => x.TimesheetGuid == timesheetId).And(x => x.ProjectId == projectId);

            return await _repository.GetTimesheetApprovalTotalHour(predicate);
        }

        public async Task<ResponseDTO> GetAprovalProject()
        {
            List<TimesheetApproval> listApproved = new List<TimesheetApproval>();
            List<ProjectEntity> listProject = new List<ProjectEntity>();


            try
            {
                var approvalData = (await GetAllTimesheetApproval());
                foreach (var data in approvalData)
                {
                    listApproved.Add(data);
                    var singleProject = await _projectService.GetProjectById(data.ProjectId);
                    ProjectEntity projectEntity = new ProjectEntity(singleProject);
                    listProject.Add(projectEntity);
                }

                return new ResponseDTO(ResponseStatus.Success, "Project by id", listProject);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseDTO> GetAprovalClient()
        {
            List<TimesheetApproval> listApproved = new List<TimesheetApproval>();
            List<ClientDetailsEntity> listClient = new List<ClientDetailsEntity>();


            try
            {
                var approvalData = (await GetAllTimesheetApproval());
                foreach (var data in approvalData)
                {
                    listApproved.Add(data);
                    var singleProject = await _projectService.GetProjectById(data.ProjectId);
                    var singleClient = await _clientDetailsService.GetClientById(singleProject.ClientGuid);
                    ClientDetailsEntity clientDetailsEntity = new ClientDetailsEntity(singleClient);
                    listClient.Add(clientDetailsEntity);
                }

                return new ResponseDTO(ResponseStatus.Success, "Project by id", listClient);

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ResponseDTO> GetUserTimesheetApprovalHistory(UserTimesheetApprovalParamDto paginationParams, Guid employeeGuId)
        {
            try
            {
                IEnumerable<TimesheetApproval> userTimeApprovalHistory = await _repository.GetUserTimesheetApprovals(
                                                                       paginationParams, employeeGuId);

                var pageData = await _repository.GetUserTimesheetApprovalsPageData(paginationParams, employeeGuId);
                var filters = pageData.GetType().GetProperty("Filters").GetValue(pageData, null);
                var totalDataCount = pageData.GetType().GetProperty("TotalDataCount").GetValue(pageData, null);

                return new ResponseDTO(ResponseStatus.Success, "Timesheet Approval's data on request successfull",
                      new
                      {
                          UserTimesheetApprovals = userTimeApprovalHistory.Select(t => t.MapToDtoUserSubmisionHistory()),
                          Filters = filters,
                          PageIndex = paginationParams.PageIndex,
                          PageSize = paginationParams.PageSize,
                          TotalRecord = totalDataCount,
                          TotalPage = totalDataCount % paginationParams.PageSize == 0 ? totalDataCount
                                     / paginationParams.PageSize : totalDataCount / paginationParams.PageSize + 1
                      });

            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }

        }

        public async Task<ResponseDTO> GetDashboardTimesheetApprovedProject(DashboardProjectParams param)
        {
            try
            {
                var data = await this._repository.DashboardTimesheetProjectApproved(param);
                var projectTimesheetApproved = data.GroupBy(t => t.Project).Skip((param.PageIndex - 1) * param.PageSize)
                                              .Take(param.PageSize).OrderBy(p=>p.Key.ProjectName).ToList();
                List<DashboardProjectDto> dashboardProjectDtoList = new List<DashboardProjectDto>();

                foreach (var pro in projectTimesheetApproved)
                {
                    var timesheetApproval = pro.ToList();

                    int billabelHours = 0;
                    int billabelNonHours = 0;
 
                    timesheetApproval.ForEach(tip =>
                    {
                        var projectTimeEntery = tip.Timesheet.TimeEntry.Where(t => t.ProjectId == pro.Key.Guid).ToList();

                        projectTimeEntery.ForEach(e =>
                        {
                            if (pro.Key.AssignResources.Select(q => q.EmployeeGuid).ToList().Contains(e.TimeSheet.EmployeeId))
                            {
                                var bilablity = pro.Key.AssignResources.Where(a => a.EmployeeGuid == e.TimeSheet.EmployeeId).First();

                                if (bilablity.Billable == null || bilablity.Billable == false)
                                    billabelNonHours = billabelNonHours + e.Hour;
                                else
                                    billabelHours = billabelHours + e.Hour;
                            }
                        });

                    }
                    );

                    DashboardProjectDto dashboardProjectDto = new DashboardProjectDto()
                    { ProjectName = pro.Key.ProjectName, ClientName = pro.Key.Client.ClientName,
                    SupervisorName = pro.Key.Supervisor.FullName,
                      StartDate = pro.Key.StartDate,
                        EndDate = pro.Key.EndDate,
                       BillableHrs = billabelHours,
                        NonBillableHrs = billabelNonHours };

                    dashboardProjectDtoList.Add(dashboardProjectDto);
                }

                var pageData = await this._repository.GetProjectDashboardData(param);
                var filters = pageData.GetType().GetProperty("Filters").GetValue(pageData, null);
                var totalDataCount = pageData.GetType().GetProperty("TotalDataCount").GetValue(pageData, null);

                return new ResponseDTO(ResponseStatus.Success, "Dashboard project data on request successfully get",
                              new
                              {
                                  projectOfDashboard = dashboardProjectDtoList,
                                  Filters = filters,
                                  PageIndex = param.PageIndex,
                                  PageSize = param.PageSize,
                                  TotalRecord = totalDataCount,
                                  TotalPage = totalDataCount % param.PageSize == 0 ? totalDataCount
                                             / param.PageSize : totalDataCount / param.PageSize + 1
                              });
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }

        }

       
    }
}
