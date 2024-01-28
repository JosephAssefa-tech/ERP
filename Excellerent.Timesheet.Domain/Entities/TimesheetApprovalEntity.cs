using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;

namespace Excellerent.Timesheet.Domain.Entities
{
    public class TimesheetApprovalEntity : BaseEntity<TimesheetApproval>
    {
        public Guid TimesheetId { get; set; }
        public TimeSheetEntity TimeSheet { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public ApprovalStatus Status { get; set; }
        public string Comment { get; set; }

        public TimesheetApprovalEntity()
        { }

        public TimesheetApprovalEntity(Guid timesheetId, Guid projectId, ApprovalStatus? status, string comment)
        {
            this.TimesheetId = timesheetId;
            this.ProjectId = projectId;
            this.Status = status ?? ApprovalStatus.Requested;
            this.Comment = comment;
        }

        public TimesheetApprovalEntity(TimesheetApproval timesheetApproval) :base(timesheetApproval)
        {
            TimesheetId = timesheetApproval.TimesheetId;
            ProjectId = timesheetApproval.ProjectId;            
            Status = timesheetApproval.Status;
            Comment = timesheetApproval.Comment;
            Project = new ProjectEntity (timesheetApproval.Project);
            TimeSheet =  new TimeSheetEntity(timesheetApproval.Timesheet);
        }

        public override TimesheetApproval MapToModel()
        {
            TimesheetApproval timesheetApproval = new TimesheetApproval();

            timesheetApproval.Guid = Guid;
            timesheetApproval.TimesheetId = TimesheetId;
            timesheetApproval.ProjectId = ProjectId;
            timesheetApproval.Status = Status;
            timesheetApproval.Comment = Comment;

            return timesheetApproval;
        }

        public override TimesheetApproval MapToModel(TimesheetApproval tsa)
        {
            TimesheetApproval timesheetApproval = new TimesheetApproval();

            timesheetApproval.Guid = tsa.Guid;
            timesheetApproval.TimesheetId = tsa.TimesheetId;
            timesheetApproval.ProjectId = tsa.ProjectId;
            timesheetApproval.Project = tsa.Project;
            timesheetApproval.Status = tsa.Status;
            timesheetApproval.Comment = tsa.Comment;
            timesheetApproval.Timesheet = tsa.Timesheet;

            return timesheetApproval;
        }
    }
}
