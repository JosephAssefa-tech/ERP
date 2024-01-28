using Excellerent.ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;


namespace Excellerent.Timesheet.Domain.Dtos
{
    public class DashboardProjectParams
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<Guid>? ClientIds { get; set; }
        public List<Guid>? SupervisorIds { get; set; }
        public string? SearchKey { get; set; }
        public ProjectType Projecttype { get; set; } = ProjectType.External;

    }

}
