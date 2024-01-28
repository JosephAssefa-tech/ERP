using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.DTOs
{
    public class EmployeeSpecParams
    {

        public Guid? id { get; set; }
        public List<string> jobtype { get; set; }
        public List<string> status { get; set; }
        public List<string> location { get; set; }
        public string? searchKey { get; set; }
        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public string? SortField { get; set; }
        public SortOrder? sortOrder { get; set; }
    }
}
