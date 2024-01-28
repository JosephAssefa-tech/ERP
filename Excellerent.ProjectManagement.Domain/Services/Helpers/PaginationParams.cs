using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Services.Helpers
{
    public class PaginationParams
    {
        public Guid? id { get; set; }
        public List<string> client { get; set; }
        public List<string> status { get; set; }
        public List<Guid> supervisorId { get; set; }
        public string searchKey { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string SortField { get; set; }
        public SortOrder sortOrder { get; set; }




    }
}
