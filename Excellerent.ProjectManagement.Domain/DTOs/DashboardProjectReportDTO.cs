using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.DTOs
{
    public  class DashboardProjectReportDTO
    {
        public int InternalProject { get; set; }
        public int ExternalProject { get; set; }

        public DashboardProjectReportDTO(int InternalProject, int ExternalProject)
        {
            this.InternalProject = InternalProject;
            this.ExternalProject = ExternalProject;
        }
    }
}
