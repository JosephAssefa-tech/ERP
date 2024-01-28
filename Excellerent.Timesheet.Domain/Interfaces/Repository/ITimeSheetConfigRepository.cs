using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Repository
{
    public interface ITimeSheetConfigRepository : IAsyncRepository<Configuration>
    {
        Task<IEnumerable<Configuration>> GetTimeSheetConfiguration();

        Task<bool> AddTimeSheetConfiguration(IEnumerable<Configuration> configurations);
    }
}
