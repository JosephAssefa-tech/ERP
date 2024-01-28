
using Excellerent.SharedModules.Specification;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Excellerent.Timesheet.Infrastructure.Specificationes
{
    public class UserTimesheetApprovalsSpec : ISpecification<TimesheetApproval>
    {
        private readonly UserTimesheetApprovalParamDto _params;
        private readonly Guid _employeeGuId;

        public UserTimesheetApprovalsSpec(UserTimesheetApprovalParamDto param, Guid employeeGuId)
        {
            this._params = param;
            this._employeeGuId = employeeGuId;
        }
        public IQueryable<TimesheetApproval> SatisfyingEntitiesFrom(IQueryable<TimesheetApproval> query)
        {

            var que = query.Include(x => x.Timesheet).Include(x => x.Timesheet.TimeEntry).Include(x => x.Project)
                               .Include(x => x.Project.Client).Where(x => x.Timesheet.EmployeeId == this._employeeGuId)
                               .Where(x => x.Timesheet.ToDate.Date > DateTime.Now.AddYears(-1)).AsQueryable();

            if(this._params.ProjectFilters!=null)
                    que= que.Where(p => _params.ProjectFilters.Contains(p.ProjectId));
                          
            if (this._params.ClientFilters != null)
                    que = que.Where(p => _params.ClientFilters.Contains(p.Project.ClientGuid));
      
            if (this._params.StatusFilter != null)
                for (int i = 0; i < this._params.StatusFilter.Length; i++)
            {
                    switch(this._params.StatusFilter[i])
                    {
                        case ApprovalStatus.Approved:
                                que = que.Where(x => x.Status == ApprovalStatus.Approved);
                                break;
                        case ApprovalStatus.Requested:
                                que = que.Where(x => x.Status == ApprovalStatus.Requested);
                                break;
 
                        case ApprovalStatus.Rejected:
                                que = que.Where(x => x.Status == ApprovalStatus.Rejected);
                                break;
                       
                    }
            }
            if (this._params.DateWeek != null)
            {
                DateTime localDateTime = (DateTime)this._params.DateWeek;
                DateTime fromDate = DateTimeUtility.GetWeeksFirstDate(localDateTime);
                DateTime toDate = DateTimeUtility.GetWeeksLastDate(localDateTime);
                que = que.Where(x => x.Timesheet.ToDate == toDate && x.Timesheet.FromDate == fromDate);
            }
            return que;
               
        }

        public TimesheetApproval SatisfyingEntityFrom(IQueryable<TimesheetApproval> query)
        {
           
            throw new NotImplementedException();
        }

      
    }
}
