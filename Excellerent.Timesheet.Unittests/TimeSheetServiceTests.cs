using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.SharedModules.DTO;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Services;
using Excellerent.Timesheet.Domain.Utilities;
using LinqKit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.Timesheet.Unittests
{
    public class TimeSheetServiceTests
    {
        private readonly TimeSheetService _timeSheetService;
        private readonly Mock<ITimeSheetRepository> _mockTimeSheetRepository;
        private readonly Mock<ITimesheetApprovalRepository> _mockTimesheetApprovalRepository;
        private readonly Mock<IAssignResourceRepository> _mockAssignResourceRepository;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IClientDetailsService> _mockClientDetailsService;
        private readonly Mock<IProjectService> _mockProjectService;

        public TimeSheetServiceTests()
        {
            _mockTimeSheetRepository = new Mock<ITimeSheetRepository>();
            _mockTimesheetApprovalRepository = new Mock<ITimesheetApprovalRepository>();
            _mockAssignResourceRepository = new Mock<IAssignResourceRepository>();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockClientDetailsService = new Mock<IClientDetailsService>();
            _mockProjectService = new Mock<IProjectService>();

            _timeSheetService = new TimeSheetService(
                    _mockTimeSheetRepository.Object,
                    _mockTimesheetApprovalRepository.Object,
                    _mockAssignResourceRepository.Object,
                    _mockEmployeeRepository.Object,
                    _mockClientDetailsService.Object,
                    _mockProjectService.Object
                );
        }

        #region GetTimeSheet

        [Fact]
        public async Task GetTimeSheet_ByTimeSheetId_ReturnsTimeSheetResponseDto()
        {
            // Arrange
            Guid guid = Guid.NewGuid();

            TimeSheet timeSheet = new TimeSheet
            {
                Guid = guid,
                FromDate = new DateTime(2021, 11, 22),
                ToDate = new DateTime(2021, 11, 28),
                TotalHours = 0,
                Status = 0,
                EmployeeId = Guid.NewGuid()
            };

            _mockTimeSheetRepository.Setup(repo => repo.GetTimeSheet((ts => ts.Guid == guid)).Result).Returns(timeSheet);

            ResponseDTO timeSheetResponse = new ResponseDTO(
                ResponseStatus.Success
                , ""
                , timeSheet.MapToDto()
            );

            // Act
            ResponseDTO result = await _timeSheetService.GetTimeSheet(guid);

            // Assert
            Assert.Equal(timeSheetResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Guid, (result.Data as TimeSheetDto).Guid);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).FromDate, (result.Data as TimeSheetDto).FromDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).ToDate, (result.Data as TimeSheetDto).ToDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).TotalHours, (result.Data as TimeSheetDto).TotalHours);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Status, (result.Data as TimeSheetDto).Status);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).EmployeeId, (result.Data as TimeSheetDto).EmployeeId);
        }

        [Fact]
        public async Task GetTimeSheet_ByEmployeeId_ReturnsTimeSheetResponseDto()
        {
            // Arrange
            Guid timesheetGuid = Guid.NewGuid();
            DateTime date = DateTime.Now.Date;
            DateTime fromDate = DateTimeUtility.GetWeeksFirstDate(date);
            DateTime toDate = DateTimeUtility.GetWeeksLastDate(date);
            Guid employeeId = Guid.NewGuid();

            TimeSheet timeSheet = new TimeSheet
            {
                Guid = timesheetGuid,
                FromDate = fromDate,
                ToDate = toDate,
                TotalHours = 0,
                Status = 0,
                EmployeeId = employeeId,
                TmpTimeEntry = new List<TmpTimeEntry>
                {
                    new TmpTimeEntry
                    {
                        Guid = Guid.NewGuid(),
                        Note = "note",
                        Date = date,
                        Index = 1,
                        Hour = 8,
                        ProjectId = Guid.NewGuid(),
                        TimesheetGuid = timesheetGuid
                    }
                }
            };

            _mockTimeSheetRepository.Setup(repo => repo.GetTimeSheet((ts => ts.EmployeeId == employeeId && ts.FromDate == fromDate && ts.ToDate == toDate)).Result).Returns(timeSheet);

            ResponseDTO timeSheetResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeSheet.MapToDto(),
                Ex = null
            };

            // Act
            ResponseDTO result = await _timeSheetService.GetTimeSheet(employeeId, null);

            // Assert
            Assert.Equal(timeSheetResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Guid, (result.Data as TimeSheetDto).Guid);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).FromDate, (result.Data as TimeSheetDto).FromDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).ToDate, (result.Data as TimeSheetDto).ToDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).TotalHours, (result.Data as TimeSheetDto).TotalHours);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Status, (result.Data as TimeSheetDto).Status);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).EmployeeId, (result.Data as TimeSheetDto).EmployeeId);
        }

        [Fact]
        public async Task GetTimeSheet_ByEmployeeIdAndDate_ReturnsTimeSheetResponseDto()
        {
            // Arrange
            Guid timesheetGuid = Guid.NewGuid();
            DateTime date = DateTime.Now.Date;
            DateTime fromDate = DateTimeUtility.GetWeeksFirstDate(date);
            DateTime toDate = DateTimeUtility.GetWeeksLastDate(date);
            Guid employeeId = Guid.NewGuid();

            TimeSheet timeSheet = new TimeSheet
            {
                Guid = Guid.NewGuid(),
                FromDate = fromDate,
                ToDate = toDate,
                TotalHours = 0,
                Status = 0,
                EmployeeId = employeeId,
                TmpTimeEntry = new List<TmpTimeEntry>
                {
                    new TmpTimeEntry
                    {
                        Guid = Guid.NewGuid(),
                        Note = "note",
                        Date = date,
                        Index = 1,
                        Hour = 8,
                        ProjectId = Guid.NewGuid(),
                        TimesheetGuid = timesheetGuid
                    }
                }
            };

            _mockTimeSheetRepository.Setup(repo => repo.GetTimeSheet((ts => ts.EmployeeId == employeeId && ts.FromDate == fromDate && ts.ToDate == toDate)).Result).Returns(timeSheet);

            ResponseDTO timeSheetResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeSheet.MapToDto(),
                Ex = null
            };

            // Act
            ResponseDTO result = await _timeSheetService.GetTimeSheet(employeeId, DateTime.Now);

            // Assert
            Assert.Equal(timeSheetResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Guid, (result.Data as TimeSheetDto).Guid);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).FromDate, (result.Data as TimeSheetDto).FromDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).ToDate, (result.Data as TimeSheetDto).ToDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).TotalHours, (result.Data as TimeSheetDto).TotalHours);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Status, (result.Data as TimeSheetDto).Status);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).EmployeeId, (result.Data as TimeSheetDto).EmployeeId);
        }

        [Fact]
        public async Task GetTimeSheet_ByEmployeeIdFromDateToDate_ReturnsTimeSheetResponseDto()
        {
            // Arrange
            DateTime date = DateTime.Now;
            date = date.AddDays((-1 * (int)date.DayOfWeek) + 1);
            DateTime fromDate = new DateTime(date.Year, date.Month, date.Day);
            DateTime toDate = fromDate.AddDays(6);
            Guid employeeId = Guid.NewGuid();

            TimeSheet timeSheet = new TimeSheet
            {
                Guid = Guid.NewGuid(),
                FromDate = fromDate,
                ToDate = toDate,
                TotalHours = 0,
                Status = 0,
                EmployeeId = employeeId
            };

            _mockTimeSheetRepository.Setup(repo => repo.GetTimeSheet((ts => ts.EmployeeId == employeeId && ts.FromDate == fromDate && ts.ToDate == toDate)).Result).Returns(timeSheet);

            ResponseDTO timeSheetResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeSheet.MapToDto(),
                Ex = null
            };

            // Act
            ResponseDTO result = await _timeSheetService.GetTimeSheet(employeeId, fromDate, toDate);

            // Assert
            Assert.Equal(timeSheetResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Guid, (result.Data as TimeSheetDto).Guid);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).FromDate, (result.Data as TimeSheetDto).FromDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).ToDate, (result.Data as TimeSheetDto).ToDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).TotalHours, (result.Data as TimeSheetDto).TotalHours);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Status, (result.Data as TimeSheetDto).Status);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).EmployeeId, (result.Data as TimeSheetDto).EmployeeId);
        }

        #endregion

        #region AddTimeSheet

        [Fact]
        public async Task AddTimeSheet_ByTimeSheetDtoAndTimeEntryDto_ReurnsTimeSheetResponseDto()
        {
            // Arrange

            DateTime date = DateTime.Now;
            date = date.AddDays((-1 * (int)date.DayOfWeek) + 1);

            //TimesheetDto
            Guid timeSheetId = Guid.NewGuid();
            DateTime fromDate = new DateTime(date.Year, date.Month, date.Day);
            DateTime toDate = new DateTime(date.Year, date.Month, date.Day).AddDays(6);
            int totalHours = 4;
            int status = 0;
            Guid employeeId = Guid.NewGuid();

            TimeSheetDto timeSheetDto = new TimeSheetDto
            {
                Guid = timeSheetId,
                FromDate = fromDate,
                ToDate = toDate,
                TotalHours = totalHours,
                Status = status,
                EmployeeId = employeeId
            };

            //TimeEntryDto
            Guid timeEntryId = Guid.NewGuid();
            string note = "Note";
            DateTime timeEntryDate = new DateTime(date.Year, date.Month, date.Day);
            int index = 1;
            int hour = 4;
            Guid projectId = Guid.NewGuid();

            TmpTimeEntryDto timeEntryDto = new TmpTimeEntryDto
            {
                Guid = timeEntryId,
                Note = note,
                Date = timeEntryDate,
                Index = index,
                Hour = hour,
                ProjectId = projectId,
                TimeSheetId = timeSheetId
            };

            //Timesheet ResponseDTO

            TimeSheet timeSheet = new TimeSheet
            {
                Guid = timeSheetId,
                FromDate = fromDate,
                ToDate = toDate,
                TotalHours = totalHours,
                Status = status,
                EmployeeId = employeeId
            };

            _mockTimeSheetRepository.Setup(service => service.AddTimeSheet(timeSheetDto.MapToModel(timeEntryDto)).Result).Returns(timeSheet);


            ResponseDTO timeSheetResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeSheetDto,
                Ex = null
            };

            // Act
            ResponseDTO result = await _timeSheetService.AddTimeSheet(timeSheetDto, timeEntryDto);

            // Assert
            Assert.Equal(timeSheetResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Guid, (result.Data as TimeSheetDto).Guid);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).FromDate, (result.Data as TimeSheetDto).FromDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).ToDate, (result.Data as TimeSheetDto).ToDate);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).TotalHours, (result.Data as TimeSheetDto).TotalHours);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).Status, (result.Data as TimeSheetDto).Status);
            Assert.Equal((timeSheetResponse.Data as TimeSheetDto).EmployeeId, (result.Data as TimeSheetDto).EmployeeId);
            //*/
        }

        #endregion
    }
}
