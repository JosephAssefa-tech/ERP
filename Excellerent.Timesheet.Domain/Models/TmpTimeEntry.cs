using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellerent.Timesheet.Domain.Models
{
    [Table("TmpTimeEntry")]
    public class TmpTimeEntry : BaseAuditModel
    {
        public TmpTimeEntry() : base() { }
        public TmpTimeEntry(TmpTimeEntry entry) : base()
        {
            this.Note = entry.Note;
            this.Date = entry.Date;
            this.Index = entry.Index;
            this.Hour = entry.Hour;
            this.ProjectId = entry.ProjectId;
            this.TimesheetGuid = entry.TimesheetGuid;
        }
        public TmpTimeEntry(string note, DateTime date, int index, int hour, Guid projectId, Guid TimesheetGuid) : base()
        {
            this.Note = note;
            this.Date = date;
            this.Index = index;
            this.Hour = hour;
            this.ProjectId = projectId;
            this.TimesheetGuid = TimesheetGuid;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Guid { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public int Index { get; set; }
        public int Hour { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        [ForeignKey("TimeSheet")]
        public Guid TimesheetGuid { get; set; }
        public TimeSheet TimeSheet { get; set; }
    }
}
