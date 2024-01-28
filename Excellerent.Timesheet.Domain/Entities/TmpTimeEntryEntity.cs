using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;

namespace Excellerent.Timesheet.Domain.Entities
{
    public class TmpTimeEntryEntity : BaseEntity<TmpTimeEntry>
    {
        public TmpTimeEntryEntity(TmpTimeEntry model) : base(model)
        {
            Note = model.Note;
            Date = model.Date;
            Index = model.Index;
            Hour = model.Hour;
            ProjectId = model.ProjectId;
            Project = new ProjectEntity(model.Project);
            TimesheetGuid = model.TimesheetGuid;
            TimeSheet = new TimeSheetEntity(model.TimeSheet);
        }
        public TmpTimeEntryEntity()
        {

        }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public int Index { get; set; }
        public int Hour { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public Guid TimesheetGuid { get; set; }
        public TimeSheetEntity TimeSheet { get; set; }
        public override TmpTimeEntry MapToModel()
        {
            TmpTimeEntry timeEntry = new TmpTimeEntry(Note, Date, Index, Hour, ProjectId, TimesheetGuid);
            timeEntry.Project = Project.MapToModel();
            return timeEntry;
        }

        public override TmpTimeEntry MapToModel(TmpTimeEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
