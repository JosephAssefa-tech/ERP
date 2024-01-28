using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Services;
using LinqKit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.Timesheet.Unittests
{
    public class TimeEntryServiceTests
    {
        private readonly Mock<ITimeEntryRepository> _mockTimeEntryRepository = new Mock<ITimeEntryRepository>();
        private readonly Mock<ITimeSheetService> _mockTimeSheetService = new Mock<ITimeSheetService>();
        private readonly Mock<ITimesheetApprovalService> _mockTimesheetApprovalService = new Mock<ITimesheetApprovalService>();
        private readonly Mock<ITimeSheetConfigService> _mocktimeSheetConfigService = new Mock<ITimeSheetConfigService>();
        private readonly Mock<IEmployeeService> _mockEmployeeService = new Mock<IEmployeeService>();
        private readonly Mock<ProjectManagement.Domain.Interfaces.ServiceInterface.IProjectService> _mockProjectService = new Mock<ProjectManagement.Domain.Interfaces.ServiceInterface.IProjectService>();
        private readonly TimeEntryService _timeEntryService;

        public TimeEntryServiceTests()
        {
            _timeEntryService = new TimeEntryService(
                    _mockTimeEntryRepository.Object,
                    _mockTimeSheetService.Object,
                    _mockTimesheetApprovalService.Object,
                   _mocktimeSheetConfigService.Object,
                   _mockEmployeeService.Object,
                   _mockProjectService.Object
                );
        }

        #region GetTimeEntry

        [Fact]
        public async Task GetTimeEntry_WithTimeEntryId_ReturnsTimeEntryResponseDTO()
        {
            // Arrange

            //      TimeEntry
            Guid guid = Guid.NewGuid();
            string note = "note";
            DateTime date = DateTime.Now.Date;
            int index = 1;
            int hour = 8;
            Guid projectId = Guid.NewGuid();
            Guid timeSheetGuid = Guid.NewGuid();

            TmpTimeEntry timeEntry = new TmpTimeEntry
            {
                Guid = guid,
                Note = note,
                Date = date,
                Index = index,
                Hour = hour,
                ProjectId = projectId,
                TimesheetGuid = timeSheetGuid
            };

            _mockTimeEntryRepository.Setup(repo => repo.GetTimeEntry((te => te.Guid == guid)).Result).Returns(timeEntry);

            ResponseDTO timeEntryResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeEntry.MapToDto(),
                Ex = null
            };

            // Act

            ResponseDTO result = await _timeEntryService.GetTimeEntry(guid);

            // Assert
            Assert.Equal(timeEntryResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Guid, (result.Data as TimeEntryDto).Guid);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Note, (result.Data as TimeEntryDto).Note);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Date, (result.Data as TimeEntryDto).Date);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Index, (result.Data as TimeEntryDto).Index);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Hour, (result.Data as TimeEntryDto).Hour);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).ProjectId, (result.Data as TimeEntryDto).ProjectId);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).TimeSheetId, (result.Data as TimeEntryDto).TimeSheetId);
        }

        #endregion

        #region GetTimeEntryForUpdateOrDelete

        [Fact]
        public async Task GetTimeEntryForUpdateOrDelete_WithTimeEntryId_ReturnsTimeEntryResponseDTO()
        {
            // Arrange

            //      TimeEntry
            Guid guid = Guid.NewGuid();
            string note = "note";
            DateTime date = DateTime.Now.Date;
            int index = 1;
            int hour = 8;
            Guid projectId = Guid.NewGuid();
            Guid timeSheetGuid = Guid.NewGuid();

            TmpTimeEntry timeEntry = new TmpTimeEntry
            {
                Guid = guid,
                Note = note,
                Date = date,
                Index = index,
                Hour = hour,
                ProjectId = projectId,
                TimesheetGuid = timeSheetGuid
            };

            _mockTimeEntryRepository.Setup(repo => repo.GetTimeEntryForUpdateOrDelete((te => te.Guid == guid)).Result).Returns(timeEntry);

            ResponseDTO timeEntryResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeEntry.MapToDto(),
                Ex = null
            };

            // Act

            ResponseDTO result = await _timeEntryService.GetTimeEntryForUpdateOrDelete(guid);

            // Assert
            Assert.Equal(timeEntryResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Guid, (result.Data as TimeEntryDto).Guid);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Note, (result.Data as TimeEntryDto).Note);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Date, (result.Data as TimeEntryDto).Date);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Index, (result.Data as TimeEntryDto).Index);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).Hour, (result.Data as TimeEntryDto).Hour);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).ProjectId, (result.Data as TimeEntryDto).ProjectId);
            Assert.Equal((timeEntryResponse.Data as TimeEntryDto).TimeSheetId, (result.Data as TimeEntryDto).TimeSheetId);
        }

        #endregion

        #region GetTimeEntries

        [Fact]
        public async Task GetTimeEntries_WithTimeSheetId_ReturnsTimeEntriesResponseDTO()
        {
            // Arrange
            
            //      TimeEntry
            Guid guid = Guid.NewGuid();
            string note = "note";
            DateTime? date = null;
            int index = 1;
            int hour = 8;
            Guid? projectId = null;
            Guid timeSheetId = Guid.NewGuid();

            List<TmpTimeEntry> timeEntries = new List<TmpTimeEntry>
            {
                new TmpTimeEntry
                {
                    Guid = guid,
                    Note = note,
                    Date = date != null ? (DateTime)date : DateTime.Now,
                    Index = index,
                    Hour = hour,
                    ProjectId = projectId != null ? (Guid)projectId : Guid.NewGuid(),
                    TimesheetGuid = timeSheetId
                }
            };

            _mockTimeEntryRepository.Setup(repo => repo.GetTimeEntries((te => 
                    te.TimesheetGuid == timeSheetId &&
                    (date == null || te.Date == date) &&
                    (projectId == null || te.ProjectId == projectId)
                )).Result).Returns(timeEntries);

            ResponseDTO timeEntriesResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeEntries.Select(te => te.MapToDto()).ToList(),
                Ex = null
            };

            // Act

            ResponseDTO result = await _timeEntryService.GetTimeEntries(timeSheetId, null, null);

            // Assert
            Assert.Equal(timeEntriesResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>).Count(), (result.Data as List<TmpTimeEntryDto>).Count());
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Guid, (result.Data as List<TmpTimeEntryDto>)[0].Guid);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Note, (result.Data as List<TmpTimeEntryDto>)[0].Note);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Date, (result.Data as List<TmpTimeEntryDto>)[0].Date);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Index, (result.Data as List<TmpTimeEntryDto>)[0].Index);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Hour, (result.Data as List<TmpTimeEntryDto>)[0].Hour);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].ProjectId, (result.Data as List<TmpTimeEntryDto>)[0].ProjectId);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].TimeSheetId, (result.Data as List<TmpTimeEntryDto>)[0].TimeSheetId);
        }

        [Fact]
        public async Task GetTimeEntries_WithTimeSheetIdDate_ReturnsTimeEntriesResponseDTO()
        {
            // Arrange

            //      TimeEntry
            Guid guid = Guid.NewGuid();
            string note = "note";
            DateTime? date = DateTime.Now.Date;
            int index = 1;
            int hour = 8;
            Guid? projectId = null;
            Guid timeSheetId = Guid.NewGuid();

            List<TmpTimeEntry> timeEntries = new List<TmpTimeEntry>
            {
                new TmpTimeEntry
                {
                    Guid = guid,
                    Note = note,
                    Date = date != null ? (DateTime)date : DateTime.Now.Date,
                    Index = index,
                    Hour = hour,
                    ProjectId = projectId != null ? (Guid)projectId : Guid.NewGuid(),
                    TimesheetGuid = timeSheetId
                }
            };

            _mockTimeEntryRepository.Setup(repo => repo.GetTimeEntries((te => 
                te.TimesheetGuid == timeSheetId &&
                (date == null || te.Date == date) &&
                (projectId == null || te.ProjectId == projectId)
            )).Result).Returns(timeEntries);

            ResponseDTO timeEntriesResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeEntries.Select(te => te.MapToDto()).ToList(),
                Ex = null
            };

            // Act
            ResponseDTO result = await _timeEntryService.GetTimeEntries(timeSheetId, date, null);

            // Assert
            Assert.Equal(timeEntriesResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>).Count(), (result.Data as List<TmpTimeEntryDto>).Count());
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Guid, (result.Data as List<TmpTimeEntryDto>)[0].Guid);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Note, (result.Data as List<TmpTimeEntryDto>)[0].Note);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Date, (result.Data as List<TmpTimeEntryDto>)[0].Date);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Index, (result.Data as List<TmpTimeEntryDto>)[0].Index);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Hour, (result.Data as List<TmpTimeEntryDto>)[0].Hour);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].ProjectId, (result.Data as List<TmpTimeEntryDto>)[0].ProjectId);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].TimeSheetId, (result.Data as List<TmpTimeEntryDto>)[0].TimeSheetId);
            //*/
        }

        [Fact]
        public async Task GetTimeEntries_WithTimeSheetIdDateProjectId_ReturnsTimeEntriesResponseDTO()
        {
            // Arrange

            //      TimeEntry
            Guid guid = Guid.NewGuid();
            string note = "note";
            DateTime? date = DateTime.Now.Date;
            int index = 1;
            int hour = 8;
            Guid? projectId = Guid.NewGuid();
            Guid timeSheetId = Guid.NewGuid();

            List<TmpTimeEntry> timeEntries = new List<TmpTimeEntry>
            {
                new TmpTimeEntry
                {
                    Guid = guid,
                    Note = note,
                    Date = date != null? (DateTime)date : DateTime.Now.Date,
                    Index = index,
                    Hour = hour,
                    ProjectId = projectId != null ? (Guid)projectId : Guid.NewGuid(),
                    TimesheetGuid = timeSheetId
                }
            };

            _mockTimeEntryRepository.Setup(repo => repo.GetTimeEntries((te => 
                    te.TimesheetGuid == timeSheetId && 
                    (date == null || te.Date == date) &&
                    (projectId == null || te.ProjectId == projectId)
                )).Result).Returns(timeEntries);

            ResponseDTO timeEntriesResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeEntries.Select(te => te.MapToDto()).ToList(),
                Ex = null
            };

            // Act
            ResponseDTO result = await _timeEntryService.GetTimeEntries(timeSheetId, date, projectId);

            // Assert
            Assert.Equal(timeEntriesResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>).Count(), (result.Data as List<TmpTimeEntryDto>).Count());
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Guid, (result.Data as List<TmpTimeEntryDto>)[0].Guid);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Note, (result.Data as List<TmpTimeEntryDto>)[0].Note);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Date, (result.Data as List<TmpTimeEntryDto>)[0].Date);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Index, (result.Data as List<TmpTimeEntryDto>)[0].Index);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].Hour, (result.Data as List<TmpTimeEntryDto>)[0].Hour);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].ProjectId, (result.Data as List<TmpTimeEntryDto>)[0].ProjectId);
            Assert.Equal((timeEntriesResponse.Data as List<TmpTimeEntryDto>)[0].TimeSheetId, (result.Data as List<TmpTimeEntryDto>)[0].TimeSheetId);
            //*/
        }

        #endregion

        #region AddTimeEntry

        [Fact]
        public async Task AddTimeEntry_WithEmployeeIdTimeEntryDto_ReturnsTimeSheetResponseDTO()
        {
            // Arrange

            Guid employeeId = Guid.NewGuid();

            //  TimeEntryDto
            Guid guid = Guid.NewGuid();
            string note = "Note";
            DateTime date = DateTime.Now.Date.AddDays((-1 * (int)DateTime.Now.Date.DayOfWeek) + 1);
            int index = 1;
            int hour = 4;
            Guid projectId = Guid.NewGuid();
            Guid timeSheetId = Guid.NewGuid();

            TmpTimeEntryDto timeEntryDto = new TmpTimeEntryDto
            {
                Guid = guid,
                Note = note,
                Index = index,
                Hour = hour,
                ProjectId = projectId,
                TimeSheetId = timeSheetId
            };

            TimeSheetDto timeSheetDto = new TimeSheetDto
            {
                Guid = timeSheetId,
                FromDate = date,
                ToDate = date.AddDays(6),
                TotalHours = hour,
                Status = 0,
                EmployeeId = employeeId
            };

            _mockTimeSheetService.Setup(service => service.GetTimeSheet(employeeId, timeEntryDto.Date).Result)
                .Returns(new ResponseDTO
                {
                    ResponseStatus = ResponseStatus.Success,
                    Message = "",
                    Data = null,
                    Ex = null
                });

            _mockTimeSheetService.Setup(service => service.AddTimeSheet(employeeId, timeEntryDto).Result)
                .Returns(new ResponseDTO 
                {
                    ResponseStatus = ResponseStatus.Success,
                    Message = "",
                    Data = timeSheetDto,
                    Ex = null
                });

            ResponseDTO timeSheetResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeSheetDto,
                Ex = null
            };

            // Act

            ResponseDTO result = await _timeEntryService.AddTimeEntry(employeeId, timeEntryDto);

            // Assert
            Assert.Equal(timeSheetResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal(timeSheetResponse.Data, result.Data);
        }

        [Fact]
        public async Task AddTimeEntry_WithEmployeeIdTimeEntryDto_ReturnsTimeEntryResponseDTO()
        {
            // Arrange

            Guid employeeId = Guid.NewGuid();

            //  TimeEntryDto
            Guid guid = Guid.NewGuid();
            string note = "Note";
            DateTime date = DateTime.Now.Date.AddDays((-1 * (int)DateTime.Now.Date.DayOfWeek) + 1);
            int index = 1;
            int hour = 4;
            Guid projectId = Guid.NewGuid();
            Guid timeSheetId = Guid.NewGuid();

            TmpTimeEntryDto timeEntryDto = new TmpTimeEntryDto
            {
                Guid = guid,
                Note = note,
                Index = index,
                Hour = hour,
                ProjectId = projectId,
                TimeSheetId = timeSheetId
            };

            TimeSheetDto timeSheetDto = new TimeSheetDto
            {
                Guid = timeSheetId,
                FromDate = date,
                ToDate = date.AddDays(6),
                TotalHours = hour,
                Status = 0,
                EmployeeId = employeeId
            };

            _mockTimeSheetService.Setup(service => service.GetTimeSheet(employeeId, timeEntryDto.Date).Result)
                .Returns(new ResponseDTO
                {
                    ResponseStatus = ResponseStatus.Success,
                    Message = "",
                    Data = timeSheetDto,
                    Ex = null
                });

            _mockTimeSheetService.Setup(service => service.AddTimeSheet(employeeId, timeEntryDto).Result)
                .Returns(new ResponseDTO
                {
                    ResponseStatus = ResponseStatus.Success,
                    Message = "",
                    Data = timeEntryDto,
                    Ex = null
                });

            ResponseDTO timeSheetResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = timeEntryDto,
                Ex = null
            };

            // Act

            ResponseDTO result = await _timeEntryService.AddTimeEntry(employeeId, timeEntryDto);

            // Assert
            Assert.Equal(timeSheetResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal(timeSheetResponse.Data, result.Data);
        }

        #endregion

        #region UpdateTimeEntry

        [Fact]
        public async Task UpdateTimeEntry_WithTimeEntryDto_ReturnsSuccessResponseDTO() 
        {
            // Arrange

            // TimeEntryDto

            Guid guid = Guid.NewGuid();
            string note = "note";
            DateTime date = DateTime.Now.Date;
            int index = 1;
            int hour = 4;
            Guid projectId = Guid.NewGuid();
            Guid timeSheetId = Guid.NewGuid();

            TmpTimeEntryDto timeEntryDto = new TmpTimeEntryDto 
            {
                Guid = guid,
                Note = note,
                Date = date,
                Index = index,
                Hour = hour,
                ProjectId = projectId,
                TimeSheetId = timeSheetId
            };

            _mockTimeEntryRepository.Setup(repo => repo.GetTimeEntryForUpdateOrDelete((te => te.Guid == guid)).Result)
                .Returns(new TmpTimeEntry
                {
                    Guid = guid,
                    Note = note,
                    Date = date,
                    Index = index,
                    Hour = hour,
                    ProjectId = projectId,
                    TimesheetGuid = timeSheetId
                });
            _mockTimeEntryRepository.Setup(repo => repo.UpdateTimeEntry(timeEntryDto.MapToModel()));

            ResponseDTO updateResponse = new ResponseDTO 
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "TimeEntry updated successfully.",
                Data = null,
                Ex = null
            };

            // Act

            ResponseDTO result = await _timeEntryService.UpdateTimeEntry(timeEntryDto);

            // Assert
            Assert.Equal(updateResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal(updateResponse.Message, result.Message);
            Assert.Null(result.Data);
            Assert.Null(result.Ex);
        }

        [Fact]
        public async Task UpdateTimeEntry_WithTimeEntryDto_ReturnsInfoResponseDTO()
        {
            // Arrange

            // TimeEntryDto

            Guid guid = Guid.NewGuid();
            string note = "note";
            DateTime date = DateTime.Now.Date;
            int index = 1;
            int hour = 4;
            Guid projectId = Guid.NewGuid();
            Guid timeSheetId = Guid.NewGuid();

            TmpTimeEntryDto timeEntryDto = new TmpTimeEntryDto
            {
                Guid = guid,
                Note = note,
                Date = date,
                Index = index,
                Hour = hour,
                ProjectId = projectId,
                TimeSheetId = timeSheetId
            };

            _mockTimeEntryRepository.Setup(repo => repo.UpdateTimeEntry(timeEntryDto.MapToModel()));

            ResponseDTO updateResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Info,
                Message = "Can not update nonexistent TimeEntry",
                Data = null,
                Ex = null
            };

            // Act

            ResponseDTO result = await _timeEntryService.UpdateTimeEntry(timeEntryDto);

            // Assert
            Assert.Equal(updateResponse.ResponseStatus, result.ResponseStatus);
            Assert.Equal(updateResponse.Message, result.Message);
            Assert.Null(result.Data);
            Assert.Null(result.Ex);
        }

        #endregion
    }
}
