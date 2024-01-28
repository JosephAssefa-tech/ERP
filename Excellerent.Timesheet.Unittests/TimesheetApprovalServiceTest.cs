
using Excellerent.SharedModules.DTO;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.Timesheet.Unittests
{
 
    public class TimesheetApprovalServiceTest
    {
        private readonly Mock<ClientManagement.Domain.Interfaces.RepositoryInterface.IClientDetailsRepository> _mockClientDetailsRepository = new Mock<ClientManagement.Domain.Interfaces.RepositoryInterface.IClientDetailsRepository>();
        private readonly Mock<ClientManagement.Domain.Interfaces.ServiceInterface.IClientDetailsService> _mockClientDetailsService = new Mock<ClientManagement.Domain.Interfaces.ServiceInterface.IClientDetailsService>();
        private readonly Mock<ITimeSheetRepository> _mockTimeSheetRepository = new Mock<ITimeSheetRepository>();
        private readonly Mock<ITimeSheetService> _mockTimeSheetService = new Mock<ITimeSheetService>();
        private readonly Mock<ResourceManagement.Domain.Interfaces.Repository.IEmployeeRepository > _mockEmployeeRepository = new Mock<ResourceManagement.Domain.Interfaces.Repository.IEmployeeRepository>();
        private readonly Mock<ProjectManagement.Domain.Interfaces.ServiceInterface.IProjectService> _mockProjectService = new Mock<ProjectManagement.Domain.Interfaces.ServiceInterface.IProjectService>();
        private readonly Mock<ProjectManagement.Domain.Interfaces.RepositoryInterface.IProjectRepository> _mockProjectRepository = new Mock<ProjectManagement.Domain.Interfaces.RepositoryInterface.IProjectRepository>();
        private readonly Mock<ITimesheetApprovalRepository> _mockTimesheetApprovalRepository = new Mock<ITimesheetApprovalRepository>();
       private  TimesheetApprovalService _timesheetApprovalService;

        public TimesheetApprovalServiceTest()
        {
            _timesheetApprovalService = new TimesheetApprovalService(
         _mockTimesheetApprovalRepository.Object,
         _mockClientDetailsRepository.Object,
         _mockProjectRepository.Object,
         _mockEmployeeRepository.Object,
         _mockTimeSheetRepository.Object,
         _mockTimeSheetService.Object,
         _mockProjectService.Object,
         _mockClientDetailsService.Object
         );
        }

        [Fact]
        public async Task GetDasboardProjectFromTimeApproval()
        {
    
         List <TimesheetApproval> TimesheetApprovalList = new List<TimesheetApproval>();
            DashboardProjectParams parm = new DashboardProjectParams();
;
            _mockTimesheetApprovalRepository.Setup(repo => repo.DashboardTimesheetProjectApproved(parm).Result).Returns(TimesheetApprovalList);
            _mockTimesheetApprovalRepository.Setup(repo => repo.GetProjectDashboardData(parm).Result).Returns( new
           {
               Filters = new { },
               TotalDataCount = 0
           });
            var response=  await _timesheetApprovalService.GetDashboardTimesheetApprovedProject(parm);

            ResponseDTO responseExpected = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "Dashboard project data on request successfully get",
                Data = null,
                Ex = null
            };

            Assert.Equal(responseExpected.ResponseStatus , response.ResponseStatus);
            Assert.Equal(responseExpected.Ex, response.Ex);
            Assert.Equal(responseExpected.Message, response.Message);
            Assert.NotEqual(responseExpected.Data, response.Data);
        }

  }
}
