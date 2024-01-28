using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
    public class DeviceDetailsRepository : AsyncRepository<DeviceDetails>, IDeviceDetailsRepository
    {
        private readonly EPPContext _context;
        public DeviceDetailsRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DeviceDetails> Get(Guid id)
        {
            return await _context.DeviceDetails.FindAsync(id);
        }

        /*public async Task<IEnumerable<DeviceDetails>> GetWithPredicateAsync(Expression<Func<DeviceDetails, Boolean>> predicate, int pageIndex, int pageSize)
        {
            return predicate == null ? (await _context.DeviceDetails.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync())
                : (await _context.DeviceDetails.Where(predicate).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync());
        }*/
        public async Task<IEnumerable<DeviceDetails>> GetWithPredicateAsync(Expression<Func<DeviceDetails, bool>> predicate, int pageIndex, int pageSize)
        {
            return predicate == null ? (await _context.DeviceDetails.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                // .Include(x => x.Category).Include(y => y.SubCategory)
                .ToListAsync())
         : (await _context.DeviceDetails.Where(predicate: predicate).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize)
         // .Include(x => x.Category).Include(y => y.SubCategory)
         .ToListAsync());

        }
    }
}
