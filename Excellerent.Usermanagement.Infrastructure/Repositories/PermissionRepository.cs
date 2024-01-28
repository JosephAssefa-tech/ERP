
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Excellerent.Usermanagement.Infrastructure.Repositories
{
    public class PermissionRepository : AsyncRepository<Permission>, IPermissionRepository
    {
        private readonly EPPContext _context;

        public PermissionRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Permission>> GetAllPermission()
        {
            return await _context.Permissions.OrderBy(x=>x.PermissionCode).ToListAsync();
        }
    }
}
