using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Interfaces.Repository
{
    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
        Task<Department> Get(Guid id);
        Task<bool> CheckIfDepartmentExist(string name);
        Task<int> Count(Expression<Func<Department, bool>> predicate);
        Task<IEnumerable<Department>> GetWithPredicateAsync(Expression<Func<Department, bool>> predicate, int pageIndex, int pageSize, string? sortBy, SortOrder? sortOrder);
    }
}
