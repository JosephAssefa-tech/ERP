using Excellerent.Timesheet.Domain.Helpers;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Utilities;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Presentation.Utilities
{
    public class TimesheetApprovalPredicate
    {
        public static Expression<Func<TimesheetApproval,bool>> ApprovalPredicate(Guid? id, PaginationParams paginationParams)
        {


            DateTime toDate = new DateTime();
            DateTime fromDate = new DateTime();
            if (paginationParams.Week != null)
            {
                DateTime localDateTime = (DateTime)paginationParams.Week;
                localDateTime = (DateTime)localDateTime.Date;
                DateTime fromDates = DateTimeUtility.GetWeeksFirstDate(localDateTime);
                fromDate = fromDates;
                DateTime toDates = DateTimeUtility.GetWeeksLastDate(localDateTime);
                toDate = toDates;
            }
            var predicateProject = PredicateBuilder.New<TimesheetApproval>();
            if (paginationParams.ProjectName != null)
            {

                foreach (var a in paginationParams.ProjectName)
                {
                    predicateProject = predicateProject.Or(x => x.Project.ProjectName == a);
                }
            }
            var predicateClient = PredicateBuilder.New<TimesheetApproval>();
           
            if (paginationParams.ClientName != null)
            {

                foreach (var a in paginationParams.ClientName)
                {
                    predicateClient = predicateClient.Or(x => x.Project.Client.ClientName == a);
                }
            }


            var predicate = PredicateBuilder.New<TimesheetApproval>();

            if (id != null)
            {
                predicate = predicate.And(x => x.Project.SupervisorGuid == id);
            }
            
        
            
            if (paginationParams.status != null)
            {
                if (paginationParams.Week != null)
                {


                    if ((paginationParams.ClientName != null) && (paginationParams.ProjectName != null))

                    {

                        predicate = predicate.And(predicateProject);
                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate

                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate);
                    }
                    else if (paginationParams.ProjectName != null)
                    {
                        predicate = predicate.And(predicateProject);

                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate);
                    }
                    else if (paginationParams.ClientName != null)
                    {

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate);
                    }
                    else
                    {
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate.And(x => x.Timesheet.FromDate == fromDate)
                            .And(n => n.Status == paginationParams.status)
                            : predicate.And(x => x.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status);

                    }

                }
                else
                {
                    if ((paginationParams.ClientName != null) && (paginationParams.ProjectName != null))

                    {

                        predicate = predicate.And(predicateProject);

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate

                            .And(n => n.Status == paginationParams.status)
                            : predicate
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status);

                    }
                    else if (paginationParams.ProjectName != null)
                    {
                        predicate = predicate.And(predicateProject);

                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(n => n.Status == paginationParams.status)
                            : predicate
                             .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                             .Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status);
                    }
                    else if (paginationParams.ClientName != null)
                    {

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(n => n.Status == paginationParams.status)
                            : predicate
                            .And(n => n.Status == paginationParams.status)
                             .And(a => a.Timesheet.Employee.FullName.ToLower() // .FirstName.ToLower()
                             .Contains(paginationParams.searchKey.ToLower()));
                    }
                    else
                    {
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate.And(n => n.Status == paginationParams.status)
                             : predicate.And(n => n.Status == paginationParams.status)
                             .And(a => a.Timesheet.Employee.FullName.ToLower() //.FirstName.ToLower()
                             .Contains(paginationParams.searchKey.ToLower())); ;

                    }
                }
            }
            else
            {
                if (paginationParams.Week != null)
                {


                    if ((paginationParams.ClientName != null) && (paginationParams.ProjectName != null))
                    {

                        predicate = predicate.And(predicateProject);

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate

                            .And(m => m.Timesheet.FromDate == fromDate)
                            : predicate

                            .And(m => m.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()));
                    }
                    else if (paginationParams.ProjectName != null)
                    {

                        predicate = predicate.And(predicateProject);


                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate.And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(m => m.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()));
                    }
                    else if (paginationParams.ClientName != null)
                    {

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate.And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(m => m.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()));
                    }
                    else
                    {
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(x => x.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(x => x.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()));

                    }

                }
                else
                {
                    if ((paginationParams.ClientName != null) && (paginationParams.ProjectName != null))
                    {

                        predicate = predicate.And(predicateProject);

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate

                            : predicate
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()))
                            ;

                    }
                    else if (paginationParams.ProjectName != null)
                    {

                        predicate = predicate.And(predicateProject);


                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                                  : predicate
                             .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                             .Contains(paginationParams.searchKey.ToLower()));
                    }
                    else if (paginationParams.ClientName != null)
                    {

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            : predicate
                            .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                            .Contains(paginationParams.searchKey.ToLower()));
                    }
                    else
                    {
                        if (id != null)
                        {
                            predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                             : predicate
                             .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                             .Contains(paginationParams.searchKey.ToLower()));
                        }
                        else
                        {
                            predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? null
                                 : predicate
                                 .And(a => a.Timesheet.Employee.FullName.ToLower() // FirstName.ToLower()
                                 .Contains(paginationParams.searchKey.ToLower()));
                        }
                    }
                }

            }

            
            return predicate;
        }
    }
}
