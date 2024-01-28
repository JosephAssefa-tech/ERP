using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Services;
using Excellerent.Timesheet.Domain.Mapping;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.Timesheet.Domain.Utilities;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;

namespace Excellerent.Timesheet.Unittests
{
    public class TimeSheetApprovalTests
    {
        private readonly TimeEntryService _timeEntryService;
        private readonly TimesheetApprovalService _timesheetAprovalService;
        private readonly Mock<ITimeEntryRepository> _timeEntryRepo = new Mock<ITimeEntryRepository>();
        private readonly Mock<ITimeSheetRepository> _iTimeSheetRepository = new Mock<ITimeSheetRepository>();
        private readonly Mock<ITimeSheetService> _itimesheetService = new Mock<ITimeSheetService>();
        private readonly Mock<ITimesheetApprovalService> _itimesheetApprovalService = new Mock<ITimesheetApprovalService>();
        private readonly Mock<ITimesheetApprovalRepository> _itimesheetApprovalRepo = new Mock<ITimesheetApprovalRepository>();
        private readonly Mock<ITimeSheetConfigService> _itimeSheetConfigService = new Mock<ITimeSheetConfigService>();
        private readonly Mock<ResourceManagement.Domain.Interfaces.Services.IEmployeeService> _iEmployeeService = new Mock<ResourceManagement.Domain.Interfaces.Services.IEmployeeService>();
        private readonly Mock<IProjectRepository> _iProjectRepository = new Mock<IProjectRepository>();
        private readonly Mock<IProjectService> _iProjectService = new Mock<IProjectService>();
        private readonly Mock<IEmployeeRepository> _iEmployeeRepository = new Mock<IEmployeeRepository>();
        private readonly Mock<IClientDetailsRepository> _iClientDetailsRepository = new Mock<IClientDetailsRepository>();

        private readonly Mock<IClientDetailsService> _clientDetailsService = new Mock<IClientDetailsService>();

        public TimeSheetApprovalTests()
        {
            _timeEntryService = new TimeEntryService(_timeEntryRepo.Object, _itimesheetService.Object, _itimesheetApprovalService.Object, _itimeSheetConfigService.Object, _iEmployeeService.Object, _iProjectService.Object);
            _timesheetAprovalService = new TimesheetApprovalService(_itimesheetApprovalRepo.Object, _iClientDetailsRepository.Object, _iProjectRepository.Object, _iEmployeeRepository.Object, _iTimeSheetRepository.Object, _itimesheetService.Object, _iProjectService.Object, _clientDetailsService.Object);
        }

        [Fact]
        public async Task ApproveTimeSheet_WithExistingTimeSheet_ReturnTimeEntry()
        {
            // Arrange TimeEntry
            var note = "Some Note";
            DateTime? date = null;
            var index = 0;
            var hour = 1;
            Guid? projectId = null;
            var timesheetGuid = Guid.NewGuid();

            List<TimeEntry> timeEntry = new List<TimeEntry>
                {
                    new TimeEntry
                    {
                        Note = note,
                        Date = date != null ? (DateTime)date : DateTime.Now,
                        Index = index,
                        Hour = hour,
                        ProjectId = projectId != null ? (Guid)projectId : Guid.NewGuid(),
                        Project = new Project(),
                        TimesheetGuid = timesheetGuid
                    }

                };

            _itimesheetService.Setup(repo => repo.GetTimeSheetById(timesheetGuid).Result).Returns(new TimeSheet
            {
                Guid = timesheetGuid,
                FromDate = DateTimeUtility.GetWeeksFirstDate(DateTime.Now),
                ToDate = DateTimeUtility.GetWeeksLastDate(DateTime.Now),
                TotalHours = hour,
                Status = 0,
                EmployeeId = Guid.NewGuid(),
                Employee = new ResourceManagement.Domain.Models.Employee()
            });
            _timeEntryRepo.Setup(repo => repo.GetTimeEntries((te => 
                    te.TimesheetGuid == timesheetGuid && 
                    (date == null || te.Date == date) &&
                    (projectId == null || te.ProjectId == projectId)
                )).Result).Returns(timeEntry);
            _itimeSheetConfigService.Setup(repo => repo.GetTimeSheetConfiguration().Result).Returns(new SharedModules.DTO.ResponseDTO
            {
                ResponseStatus = SharedModules.DTO.ResponseStatus.Success,
                Message = "",
                Data = new ConfigurationDto 
                {
                    WorkingDays = new List<string>(),
                    WorkingHours = new WorkingHours { Min = 0, Max = 24 }
                }
            });
            _iProjectService.Setup(service => service.GetPaginatedProject(timeEntry[0].ProjectId, string.Empty, null, null).Result).Returns(new SharedModules.DTO.PredicatedResponseDTO
            {
                Data = new List<ProjectManagement.Domain.Entities.ProjectEntity>
                {
                    new ProjectManagement.Domain.Entities.ProjectEntity(timeEntry[0].Project)
                }
            });
            // Arrang Timesheet Approval Entity

            List<TimesheetApprovalDto> timesheetApprovalDtos = new List<TimesheetApprovalDto>
            {
                new TimesheetApprovalDto {
                    TimesheetId = timeEntry[0].TimesheetGuid,
                    ProjectId = timeEntry[0].ProjectId,
                    Status = (int)ApprovalStatus.Requested
                }
            };

            // Act
            List<TimesheetApprovalDto> result = (await _timeEntryService.ApproveTimeSheet(timesheetGuid)).Data;

            // Assert
            Assert.Equal(timesheetApprovalDtos[0].TimesheetId, result[0].TimesheetId);
            Assert.Equal(timesheetApprovalDtos[0].ProjectId, result[0].ProjectId);
            Assert.Equal(timesheetApprovalDtos[0].Status, result[0].Status);
            //*/
        }

        [Fact]
        public async Task ApproveTimeSheet_WithUnexistingTimeSheet_ReturnNoContent()
        {

            // Arrange
            var Note = "Some Note";
            var Date = DateTime.Now;
            var Index = 0;
            var Hour = 1;
            var ProjectId = Guid.NewGuid();
            var TimesheetGuid = Guid.NewGuid();

            List<TimeEntry> timeEntry = new List<TimeEntry>
            {
                new TimeEntry
                {
                    Note = Note,
                    Date = Date,
                    Index = Index,
                    Hour = Hour,
                    ProjectId = ProjectId,
                    TimesheetGuid = TimesheetGuid
                }
            };

            _timeEntryRepo.Setup(repo => repo.FindAsync(x => x.TimesheetGuid == It.IsAny<Guid>()).Result).Returns(timeEntry);
            
            // Act
            List<TimesheetApprovalEntity> result = (await _timeEntryService.ApproveTimeSheet(TimesheetGuid)).Data;

            // Assert
            Assert.Null(result);
        }

        /*
        [Fact]
        public async Task GetTimesheetApprovalStatus_WithExistingTimeSheetStatus_TimesheetAproval()
        {

            // Arrange Timesheet Approval Model
            DateTime date = DateTime.Now.Date;
            Guid timesheetId = Guid.NewGuid();
            Guid projectId = Guid.NewGuid();
            ApprovalStatus status = ApprovalStatus.Requested;

            TimeSheet timesheet = new TimeSheet
            {
                Guid = timesheetId,
                FromDate = DateTimeUtility.GetWeeksFirstDate(date),
                ToDate = DateTimeUtility.GetWeeksLastDate(date),
                Status = 0,
                EmployeeId = Guid.NewGuid(),
                Employee = new ResourceManagement.Domain.Models.Employee()
            };

            Project project = new Project() { Guid = projectId };

            List<TimesheetApproval> timesheetApproval = new List<TimesheetApproval>
            {
                new TimesheetApproval
                {
                    TimesheetId = timesheetId,
                    ProjectId = projectId,
                    Status = status
                }
            };

            _itimesheetApprovalRepo.Setup(repo => repo.FindAsync(x => x.TimesheetId == timesheetId).Result).Returns(timesheetApproval);
            _iTimeSheetRepository.Setup(repo => repo.GetTimeSheet((ts => ts.Guid == timesheetId)).Result).Returns(timesheet);
            _iProjectRepository.Setup(repo => repo.GetProjectById(projectId).Result).Returns(project);
            // Arrange Timesheet Approval Dto

            List<TimesheetApprovalDto> timesheetApprovalDtos = new List<TimesheetApprovalDto>
            {
                new TimesheetApprovalDto
                {
                    TimesheetId = timesheetId,
                    ProjectId = projectId,
                    Status = ApprovalStatus.Requested,
                }
            };

            // Act
            List<TimesheetApprovalDto> result = (await _timesheetAprovalService.GetTimesheetApprovalStatus(timesheetId)).Select(tsa => tsa.MapToDto()).ToList();

            // Assert
            Assert.Equal(timesheetApprovalDtos[0].TimesheetId, result[0].TimesheetId);
            Assert.Equal(timesheetApprovalDtos[0].ProjectId, result[0].ProjectId);
            Assert.Equal(timesheetApprovalDtos[0].Status, result[0].Status);
        }
        //*/
    }
}
