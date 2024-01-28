using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Enums;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Infrastructure.Repositories
{
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        private readonly EPPContext _context;

        public UserRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UserListView>> GetUserDashBoardList(Expression<Func<User, bool>> predicate, int pageIndex, int pageSize, string? sortBy, SortOrder? sortOrder)
        {

            SortOrder sort = sortOrder ?? SortOrder.Descending; // sort order descending by default

            Expression<Func<User, object>> sortExpression = o => o.LastActivityDate; // sort by LastActivityDate by default

            IQueryable<User> query = _context.Users;
            if (predicate != null)
            {
                query = query.Where(predicate: predicate);
            }
            query = query.Where(d => d.IsDeleted == false).Include(x => x.Employee).ThenInclude(eo => eo.EmployeeOrganization)
                        .ThenInclude(z => z.Role).ThenInclude(a => a.Department).Where(d => d.Employee.IsDeleted == false);

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("FullName"))
                {
                    sortExpression = x => x.FullName.ToLower(); // x.FirstName.ToLower() + x.MiddleName.ToLower() + x.LastName.ToLower();
                }
                else if (sortBy.Equals("Department"))
                {
                    sortExpression = x => x.Employee.EmployeeOrganization.Department.Name.ToLower();
                }
                else if (sortBy.Equals("JobTitle"))
                {
                    sortExpression = x => x.Employee.EmployeeOrganization.Role.Name.ToLower();
                }
                else if (sortBy.Equals("Status"))
                {
                    sortExpression = x => x.Status;
                }
            }

            query = sort == SortOrder.Descending ? query.OrderByDescending(sortExpression) :
                query.OrderBy(sortExpression);

            var paginatedUserList = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            List<UserListView> userViewModelList = new List<UserListView>();
            if (paginatedUserList.Count() > 0)
            {
                foreach (User user in paginatedUserList)
                {
                    userViewModelList.Add(
                        new UserListView()
                        {
                            UserId = user.Guid,
                            FullName = user.FullName, // user.FirstName + ' ' + user.MiddleName + ' ' + user.LastName,
                            JobTitle = user.Employee.EmployeeOrganization.Role == null ? string.Empty : user.Employee.EmployeeOrganization.Role.Name,
                            Department = user.Employee.EmployeeOrganization.Department == null ? string.Empty : user.Employee.EmployeeOrganization.Department.Name,
                            Status = user.Status == UserStatus.Active ? "Active" : "In-Active",
                            LastActivityDate = user.LastActivityDate != null ? (DateTime)user.LastActivityDate : DateTime.MinValue
                        }
                    );
                }
            }
            else
            {
                userViewModelList = null;
            }
            return userViewModelList;
        }

        public async Task<IEnumerable<Department>> GetDistinctDepartments()
        {
            var queryable = from e in _context.Departments
                            where _context.Users.Any(u => u.Employee.EmployeeOrganization.Department.Guid == e.Guid)
                            select e;
            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetDistinctJobTitles()
        {
            var queryable = from e in _context.Roles
                            where _context.Users.Any(u => u.Employee.EmployeeOrganization.Role.Guid == e.Guid)
                            select e;
            return await queryable.ToListAsync();
        }

        public async Task<int> GetUserDashBoardListCount(Expression<Func<User, bool>> predicate)
        {
            return predicate == null ? await _context.Users.Where(d => d.Employee.IsDeleted == false).Where(d => d.IsDeleted == false).CountAsync<User>() : 
                await _context.Users.Where(d => d.Employee.IsDeleted == false).Where(predicate).Where(d => d.IsDeleted == false).CountAsync<User>();
        }
       
        public async Task<IEnumerable<Employee>> GetEmployeesNotInAsUser()
        {
            var quarable = from e in _context.Employees.Where(x => x.IsDeleted == false).Include(e => e.EmployeeOrganization.Role)
                           where !_context.Users.Any(u => u.EmployeeId == e.Guid && u.IsDeleted == false)
                           select e
             ;
            return await quarable.ToListAsync();
        }
       


        public async Task<List<UsersNotGrouped>> LoadUsersNotGroupedInGroup(Guid Id)
        {
            var quarable = from e in _context.Users.Include(u => u.Employee.EmployeeOrganization.Role).Where(x => x.IsActive == true)
                           where !_context.UserGroups.Any(ug => ug.UserGuid == e.Guid && ug.GroupSetGuid == Id)
                           select e;
            
            List<UsersNotGrouped> notAssignedUsers = new List<UsersNotGrouped>();
            var notAssignedUsersList = await quarable.ToListAsync();
            StringBuilder builder = new StringBuilder();
            foreach (User user in notAssignedUsersList)
            {
                /* builder.Clear();
                builder.Append(user.FirstName);
                builder.Append(" ");
                builder.Append(user.MiddleName);
                builder.Append(" ");
                builder.Append(user.LastName);
                builder.Append(" ");
                builder.Append(user.Employee.EmployeeOrganization.Role.Name); */
                notAssignedUsers.Add(new UsersNotGrouped()
                {
                    Guid = user.Guid,
                    FullName =  user.FullName + " " + user.Employee.EmployeeOrganization.Role.Name // builder.ToString()
                });
            }

            return notAssignedUsers;
        }

        public async Task<bool> CreatUser(User user, Guid [] GroupIds)
        {
            this._context.Users.Add(user);
            if(GroupIds != null)
            foreach (Guid groupId in GroupIds)
            {
                this._context.UserGroups.Add(new UserGroups { UserGuid= user.Guid, GroupSetGuid = groupId });
            }

            try
            {
               await this._context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
                    
            
        }

        public async Task<bool> CreateDeletedUser(User user, Guid[] GroupIds)
        {
            // this._context.Users.Update(user);
            var ugs = await this._context.UserGroups.Where(ug => ug.UserGuid == user.Guid).ToListAsync();
            foreach (UserGroups ug in ugs)
            {
                this._context.UserGroups.Remove(ug);
            }
            if (GroupIds != null)
                foreach (Guid groupId in GroupIds)
                {
                    this._context.UserGroups.Add(new UserGroups { UserGuid = user.Guid, GroupSetGuid = groupId });
                }

            try
            {
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }


        }
    }
}
