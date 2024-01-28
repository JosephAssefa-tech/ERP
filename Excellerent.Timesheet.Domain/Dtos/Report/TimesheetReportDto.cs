using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Dtos.Report
{
    public class TimesheetReportDto
    {
        public string Client { get; set; }
        public string ClientManager { get; set; }
        public string Month { get; set; }
        public DateTime ReportDate { get; set; }
        public string Legend { get; set; } = "L - Casual Leave, M - Medical/Maternity Leave, S - Sick Leave, V - Vacation, U - Unassigned, H - Holiday";
        public string ProjectName { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool Billable { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
    }
}
