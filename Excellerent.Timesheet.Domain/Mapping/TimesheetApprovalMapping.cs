using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Models;
using System.Linq;

namespace Excellerent.Timesheet.Domain.Mapping
{
    public static class TimesheetApprovalMapping
    {
        public static TimesheetApprovalEntity MapToEntity(this TimesheetApprovalDto timesheetAprovalDto)
        {
            TimesheetApprovalEntity timesheetApprovalEntity = new TimesheetApprovalEntity();

            timesheetApprovalEntity.TimesheetId = timesheetAprovalDto.TimesheetId;
            timesheetApprovalEntity.ProjectId = timesheetAprovalDto.ProjectId;
            timesheetApprovalEntity.Comment = timesheetAprovalDto.Comment;
            timesheetApprovalEntity.Status = timesheetAprovalDto.Status;
            return timesheetApprovalEntity;
        }
        public static TimesheetApprovalDto MapToDto(this TimesheetApprovalEntity timesheetApprovalEntity)
        {
            TimesheetApprovalDto timesheetAprovalDto = new TimesheetApprovalDto();

            timesheetAprovalDto.TimesheetId = timesheetApprovalEntity.TimesheetId;
            timesheetAprovalDto.ProjectId = timesheetApprovalEntity.ProjectId;
            //timesheetAprovalDto.ProjectName = timesheetApprovalEntity
            timesheetAprovalDto.Comment = timesheetApprovalEntity.Comment;
            timesheetAprovalDto.Status = timesheetApprovalEntity.Status;

            return timesheetAprovalDto;
        }
        public static TimesheetApprovalDto MapToDtoApproval(this TimesheetApprovalEntity timesheetApprovalEntity, ProjectEntity projectEntity, EmployeeEntity employee, TimeSheetEntity timeSheetEntity)
        {
            TimesheetApprovalDto timesheetAprovalDto = new TimesheetApprovalDto();

            timesheetAprovalDto.TimesheetId = timesheetApprovalEntity.TimesheetId;
            timesheetAprovalDto.ProjectId = timesheetApprovalEntity.ProjectId;
            timesheetAprovalDto.ToDate = timeSheetEntity.ToDate;
            timesheetAprovalDto.FromDate = timeSheetEntity.FromDate;
            timesheetAprovalDto.ProjectName = projectEntity.ProjectName;
            timesheetAprovalDto.ClientName = "Client Name Not Mapped";
            timesheetAprovalDto.EmployeeName = employee.Name;
            timesheetAprovalDto.Comment = timesheetApprovalEntity.Comment;
            timesheetAprovalDto.Status = timesheetApprovalEntity.Status;

            return timesheetAprovalDto;
        }

        public static TimesheetApprovalDto MapToDtoUserSubmisionHistory(this TimesheetApproval timesheetApproval)
        {
            TimesheetApprovalDto timesheetAprovalDto = new TimesheetApprovalDto();
            timesheetAprovalDto.ProjectId = timesheetApproval.ProjectId;
            timesheetAprovalDto.TimesheetId = timesheetApproval.TimesheetId;
            timesheetAprovalDto.ProjectName = timesheetApproval.Project.ProjectName;
            timesheetAprovalDto.ToDate = timesheetApproval.Timesheet.ToDate;
            timesheetAprovalDto.FromDate = timesheetApproval.Timesheet.FromDate;
            timesheetAprovalDto.ClientName = timesheetApproval.Project.Client.ClientName;
            timesheetAprovalDto.Comment = timesheetApproval.Comment;
            timesheetAprovalDto.Status = timesheetApproval.Status;
            timesheetAprovalDto.CreatedDate = timesheetApproval.CreatedDate;
            timesheetAprovalDto.TotalHours = (int)timesheetApproval.Timesheet.TimeEntry.Where(x => x.ProjectId == timesheetApproval.ProjectId).Sum(x => x.Hour);
            return timesheetAprovalDto;
        }

    }

}
