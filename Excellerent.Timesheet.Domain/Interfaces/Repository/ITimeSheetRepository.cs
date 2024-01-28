using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Repository
{
    public interface ITimeSheetRepository : IAsyncRepository<TimeSheet>
    {
        Task<TimeSheet> GetLastEmployeeSheet(Guid employeeId);
        Task<TimeSheet> GetTimeSheet(Guid timesheetGuid);
        Task<TimeSheet> GetTimeSheet(Expression<Func<TimeSheet, bool>> predicate);

        Task<IEnumerable<TimeSheet>> GetTimeSheetsForReport(List<Guid> clientId, List<Guid> projectIds, List<Guid> leaveProjectIds, DateTime fromDate, DateTime toDate);

        Task<TimeSheet> AddTimeSheet(TimeSheet timesheet);
        Task<bool> Update(TimeSheet timesheet);
        Task<bool> Update(Guid timesheetId, List<TimeEntry> timeEntries);
        Task<bool> Update(Guid timesheetId, List<TmpTimeEntry> tmpTimeEntries);

        Task<bool> IsEmployeeWithTimeesheetExists(Guid employeeGuid);
        Task<List<TimeEntry>> GetTimeSheetEntryHoursForReport(Guid? clientId, List<Guid> projectGuids, DateTime? fromDate, DateTime? toDate, bool billable);
    }
}
