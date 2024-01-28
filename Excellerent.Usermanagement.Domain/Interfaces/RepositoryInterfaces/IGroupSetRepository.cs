
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces
{
    public interface IGroupSetRepository : IAsyncRepository<GroupSet>
    {

        Task<IEnumerable<GroupSet>> GetAllUserGroupsDashboardAsync(Expression<Func<GroupSet, bool>> predicate, int pageIndex, int pageSize, string sortBy, SortOrder? sortOrder);

        Task<int> AllUserGroupsDashboardCountAsync(Expression<Func<GroupSet, bool>> predicate);

        Task<GroupSetDetail> GetGroupSetById(Guid groupId);

        Task UpdateGroupDescription(GroupSetPatchModel newGroupDescription);
    }
}
