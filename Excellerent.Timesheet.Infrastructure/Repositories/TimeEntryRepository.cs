using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Infrastructure.Repositories
{
    public class TimeEntryRepository : AsyncRepository<TmpTimeEntry>, ITimeEntryRepository
    {
        private readonly EPPContext _context;
        public TimeEntryRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TmpTimeEntry> GetTimeEntry(Expression<Func<TmpTimeEntry, bool>> predicate)
        {
            return await FindOneAsync(predicate);
        }

        public async Task<TmpTimeEntry> GetTimeEntryForUpdateOrDelete(Expression<Func<TmpTimeEntry, bool>> predicate)
        {
            return await FindOneAsyncForDelete(predicate);
        }

        public async Task<IEnumerable<TmpTimeEntry>> GetTimeEntries(Expression<Func<TmpTimeEntry, bool>> predicate)
        {
            return await FindAsync(predicate);
        }

        public async Task<TmpTimeEntry> AddTimeEntry(TmpTimeEntry timeEntry)
        {
            return await AddAsync(timeEntry);
        }

        public async Task UpdateTimeEntry(TmpTimeEntry timeEntry)
        {
            await UpdateAsync(timeEntry);
        }
    }
}
