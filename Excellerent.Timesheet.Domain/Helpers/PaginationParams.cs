using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Helpers
{
    public class PaginationParams

    {
        private const int MaxPageIndex = 50;
        
        public string? searchKey { get; set; }
        public string? SortBy { get; set; }
        public List<string>? ProjectName { get; set; }
        public List<string>? ClientName { get; set; }
        public DateTime ? Week { get; set; } 
        public ApprovalStatus? status { get; set; } 
        public int? PageIndex { get; set; } 
        private int _PageSize = 10;
        public int? PageSize { 
            get; 
            set ; 
        }
        public SortOrder sort { get; set; } 
    }
}
