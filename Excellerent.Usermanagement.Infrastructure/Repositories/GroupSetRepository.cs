using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.Seed;
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
    public class GroupSetRepository : AsyncRepository<GroupSet>, IGroupSetRepository
    {
        private readonly EPPContext _context;

        public GroupSetRepository(EPPContext context) : base(context)
        {
            _context = context;
        }


        public async Task<int> AllUserGroupsDashboardCountAsync(Expression<Func<GroupSet, bool>> predicate)
        {
            return predicate == null ? await _context.GroupSets.CountAsync<GroupSet>() :
                  await _context.GroupSets.Where(predicate).CountAsync<GroupSet>();
        }

        public async Task<IEnumerable<GroupSet>> GetAllUserGroupsDashboardAsync(Expression<Func<GroupSet, bool>> predicate, int pageIndex, int pageSize, string sortBy, SortOrder? sortOrder)
        {
            SortOrder sort = sortOrder ?? SortOrder.Descending; // sort order descending by default

            Expression<Func<GroupSet, object>> sortExpression = x => x.CreatedDate; // sort by createdDate by default

            IQueryable<GroupSet> query = _context.GroupSets;

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

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
        }

        public async Task<GroupSetDetail> GetGroupSetById(Guid groupId)
        {
            var result = await _context.GroupSets.Where(x => x.Guid == groupId).FirstOrDefaultAsync();
            var groupSetDetail = new GroupSetDetail()
            {
                Guid = result.Guid,
                Name = result.Name,
                Description = result.Description
            };
            return groupSetDetail;
        }

        public async Task UpdateGroupDescription(GroupSetPatchModel newGroupDescription)
        {
            var result = await _context.GroupSets.Where(x => x.Guid == newGroupDescription.Guid).FirstOrDefaultAsync();
            result.Description = newGroupDescription.Description;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }  
    }
}
