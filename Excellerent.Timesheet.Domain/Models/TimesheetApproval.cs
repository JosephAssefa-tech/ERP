using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excellerent.Timesheet.Domain.Models
{
    public enum ApprovalStatus
    {
        Requested,
        Approved,
        Rejected
    }
    public class TimesheetApproval : BaseAuditModel
    {
        public Guid TimesheetId { get; set; }
        public virtual TimeSheet Timesheet { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public ApprovalStatus Status { get; set; }
        public string Comment { get; set; }
    }
}
