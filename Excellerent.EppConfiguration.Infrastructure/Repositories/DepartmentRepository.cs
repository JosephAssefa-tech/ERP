using Excellerent.EppConfiguration.Domain.Interfaces.Repository;
using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Infrastructure.Repositories
{
    public class DepartmentRepository : AsyncRepository<Department>, IDepartmentRepository
    {
        private readonly EPPContext _context;
        public DepartmentRepository(EPPContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Department> Get(Guid id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<bool> CheckIfDepartmentExist(string name)
        {
            name = name.Trim();
            return await _context.Departments.Where(x => x.Name.Trim().ToLower().Equals(name.ToLower())).CountAsync<Department>() > 0;
        }

        public async Task<int> Count(Expression<Func<Department, bool>> predicate)
        {
            return predicate == null ? await _context.Departments.CountAsync<Department>() :
                await _context.Departments.Where(predicate:predicate).CountAsync<Department>();
        }

        public async Task<IEnumerable<Department>> GetWithPredicateAsync(Expression<Func<Department, bool>> predicate, int pageIndex, int pageSize, string? sortBy, SortOrder? sortOrder)
        {
            SortOrder sort = sortOrder ?? SortOrder.Descending; // sort order descending by default

            Expression<Func<Department, object>> sortExpression = x => x.CreatedDate; // sort by createdDate by default

            IQueryable<Department> query = _context.Departments;
            if (predicate != null)
            {
                query = query.Where(predicate: predicate);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("Name"))
                {
                    sortExpression = x => x.Name.ToLower();
                }
            }

            query = sort == SortOrder.Descending ? query.OrderByDescending(sortExpression) :
                query.OrderBy(sortExpression);

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
