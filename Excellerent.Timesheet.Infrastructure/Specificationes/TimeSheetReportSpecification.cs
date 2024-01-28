using Excellerent.SharedModules.Specification;
using Excellerent.Timesheet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Infrastructure.Specificationes
{
    public class TimeSheetReportSpecification : ISpecification<TimeSheet>
    {
        private readonly List<Guid> _clientIds;
        private readonly List<Guid> _projectIds;
        private readonly List<Guid> _leaveProjectIds;
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;
        public TimeSheetReportSpecification(List<Guid> clientId, List<Guid> projectIds, List<Guid> leaveProjectIds, DateTime fromDate, DateTime toDate)
        {
            this._clientIds = clientId;
            this._projectIds = projectIds;
            this._leaveProjectIds = leaveProjectIds;
            this._fromDate = fromDate;
            this._toDate = toDate;
        }

        public IQueryable<TimeSheet> SatisfyingEntitiesFrom(IQueryable<TimeSheet> query)
        {
            query = query.Include(ts => ts.Employee).ThenInclude(emp => emp.EmployeeOrganization).ThenInclude(eorg => eorg.Role)
                        .Include(ts => ts.TimeEntry.Where(
                            te => (te.Date >= this._fromDate) &&
                                (te.Date <= this._toDate) &&
                                (
                                    (this._projectIds.Count == 0 || this._projectIds.Contains(te.ProjectId)) ||
                                    (this._leaveProjectIds.Count == 0 || this._leaveProjectIds.Contains(te.ProjectId))
                                ) &&
                                (this._clientIds.Count == 0 || this._clientIds.Contains(te.Project.ClientGuid))
                            )).ThenInclude(te => te.Project).ThenInclude(p => p.Client)
                        .Where(ts => ts.TimeEntry.Count() > 0);

            return query;
        }

        public TimeSheet SatisfyingEntityFrom(IQueryable<TimeSheet> query)
        {
            throw new NotImplementedException();
        }
    }
}
