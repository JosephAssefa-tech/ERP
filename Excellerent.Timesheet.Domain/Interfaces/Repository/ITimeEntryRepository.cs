using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Repository
{
    public interface ITimeEntryRepository : IAsyncRepository<TmpTimeEntry>
    {
        // Get one/single timesheet
        Task<TmpTimeEntry> GetTimeEntry(Expression<Func<TmpTimeEntry, bool>> predicate);

        Task<TmpTimeEntry> GetTimeEntryForUpdateOrDelete(Expression<Func<TmpTimeEntry, bool>> predicate);

        // Get multiple timesheets
        Task<IEnumerable<TmpTimeEntry>> GetTimeEntries(Expression<Func<TmpTimeEntry, bool>> predicate);

        Task<TmpTimeEntry> AddTimeEntry(TmpTimeEntry timeEntry);

        Task UpdateTimeEntry(TmpTimeEntry timeEntry);
    }
}
