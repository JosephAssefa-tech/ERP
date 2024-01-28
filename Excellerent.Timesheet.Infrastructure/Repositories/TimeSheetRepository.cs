using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Infrastructure.Specificationes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Infrastructure.Repositories
{
    public class TimeSheetRepository : AsyncRepository<TimeSheet>, ITimeSheetRepository
    {
        private readonly EPPContext _context;
        public TimeSheetRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TimeSheet> GetLastEmployeeSheet(Guid employeeId)
        {
            return await _context.TimeSheets.Where(x => x.EmployeeId == employeeId).OrderBy(x => x.ToDate).LastOrDefaultAsync();
        }

        public async Task<TimeSheet> GetTimeSheet(Guid timesheetGuid)
        {
            return await _context.TimeSheets.Where(ts => ts.Guid == timesheetGuid).Include(ts => ts.TimeEntry).Include(ts => ts.TmpTimeEntry).FirstAsync();
        }

        public async Task<TimeSheet> GetTimeSheet(Expression<Func<TimeSheet, bool>> predicate)
        {
            return await FindOneAsync(predicate);
        }

        public async Task<IEnumerable<TimeSheet>> GetTimeSheetsForReport(List<Guid> clientIds, List<Guid> projectIds, List<Guid> leaveProjectIds, DateTime fromDate, DateTime toDate)
        {
            var timesheetSpec = new TimeSheetReportSpecification(clientIds, projectIds, leaveProjectIds, fromDate, toDate);

            var result = await FindAsync(timesheetSpec);

            return result;
        }

        public async Task<TimeSheet> AddTimeSheet(TimeSheet timesheet)
        {
            return await AddAsync(timesheet);
        }

        public async Task<bool> Update(TimeSheet ts)
        {
            _context.TimeSheets.Update(ts);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Guid timesheetId, List<TimeEntry> timeEntries)
        {
            int changes = 0;

            foreach (TimeEntry timeEntry in (await _context.TimeEntry.Where(te => te.TimesheetGuid == timesheetId).ToListAsync()))
            {
                if (timeEntries.Where(te => te.Guid == timeEntry.Guid).Count() == 0)
                {
                    _context.TimeEntry.Remove(timeEntry);
                }
            }

            foreach (TimeEntry timeEntry in timeEntries)
            {
                if (_context.TimeEntry.Where(te => te.Guid == timeEntry.Guid).Count() > 0)
                {
                    _context.TimeEntry.Update(timeEntry);
                }
                else
                {
                    _context.TimeEntry.Add(timeEntry);
                }
            }

            changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Guid timesheetId, List<TmpTimeEntry> tmpTimeEntries)
        {
            int changes = 0;

            foreach (TmpTimeEntry tmpTimeEntry in (await _context.TmpTimeEntries.Where(te => te.TimesheetGuid == timesheetId).ToListAsync()))
            {
                if (tmpTimeEntries.Where(te => te.Guid == tmpTimeEntry.Guid).Count() == 0)
                {
                    _context.TmpTimeEntries.Remove(tmpTimeEntry);
                }
            }

            foreach (TmpTimeEntry tmpTimeEntry in tmpTimeEntries)
            {
                if (_context.TmpTimeEntries.Where(te => te.Guid == tmpTimeEntry.Guid).Count() > 0)
                {
                    _context.TmpTimeEntries.Update(tmpTimeEntry);
                }
                else
                {
                    _context.TmpTimeEntries.Add(tmpTimeEntry);
                }
            }

            changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> IsEmployeeWithTimeesheetExists(Guid employeeGuid)
        {
            return await _context.TimeSheets.AnyAsync(t => t.EmployeeId == employeeGuid);

        }
        #region Report
        public async Task<List<TimeEntry>> GetTimeSheetEntryHoursForReport(Guid? clientId, List<Guid> projectGuids, DateTime? fromDate, DateTime? toDate, bool billable)
        {
            var assignedResources = await this._context.AssignResources
                .Where(ar => ar.Billable == billable).Select(ar => ar.ProjectGuid).ToListAsync();

            var timeEntries = await this._context.TimeEntry.Where(te => assignedResources.Contains(te.ProjectId))
                .Include(te => te.TimeSheet)
                 .ThenInclude(t => t.Employee)
                 .ThenInclude(e => e.EmployeeOrganization)
                 .ThenInclude(e => e.Role)
                 .Include(te => te.Project)
                 .ThenInclude(p => p.Client)
                 .ThenInclude(c => c.ClientContacts)
                 .Where(te => (projectGuids == null || projectGuids.Count == 0 || projectGuids.Contains(te.ProjectId))
                 && ((clientId == null || te.Project.ClientGuid == clientId) && te.Project.Client.ClientName != "Leave")
                 && (fromDate == null || te.Date >= fromDate)
                 && (toDate == null || te.Date <= toDate)).ToListAsync();

            return timeEntries;

        }

        #endregion
    }
}
