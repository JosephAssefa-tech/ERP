using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        public Task<List<UserListView>> GetUserDashBoardList(Expression<Func<User, Boolean>> predicate, int pageIndex, int pageSize, string sortBy, SortOrder? sortOrder);
        Task<IEnumerable<Department>> GetDistinctDepartments();
        Task<IEnumerable<Role>> GetDistinctJobTitles();
        public Task<int> GetUserDashBoardListCount(Expression<Func<User, Boolean>> predicate);
        Task<IEnumerable<Employee>> GetEmployeesNotInAsUser();
        Task<bool> CreatUser(User user, Guid[] GroupIds);
        Task<bool> CreateDeletedUser(User user, Guid[] GroupIds);
        Task<List<UsersNotGrouped>> LoadUsersNotGroupedInGroup(Guid Id);
    }
}
