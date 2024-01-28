using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Interfaces.Repository
{
    public interface IRoleRepository : IAsyncRepository<Role>
    {
        Task<Role> Get(Guid id);
        Task<bool> CheckIfJobTitleExist(string name, Guid departmentGuid);
        Task<int> Count(Expression<Func<Role, bool>> predicate);
        Task<IEnumerable<Role>> GetWithPredicateAsync(Expression<Func<Role, bool>> predicate, int pageIndex, int pageSize, string? sortBy, SortOrder? sortOrder);
    }
}
