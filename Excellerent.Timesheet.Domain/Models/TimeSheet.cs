using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellerent.Timesheet.Domain.Models
{
    [Table("TimeSheet")]
    public class TimeSheet : BaseAuditModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Guid { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? TotalHours { get; set; }
        public int Status { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee  Employee { get; set; }
        public List<TimeEntry> TimeEntry { get; set; }
        public List<TmpTimeEntry> TmpTimeEntry { get; set; }

        public TimeSheet()
        {
            TmpTimeEntry = new List<TmpTimeEntry>();
        }
    }
}
