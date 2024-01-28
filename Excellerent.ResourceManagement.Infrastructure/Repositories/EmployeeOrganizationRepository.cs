using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.Repository.Interfaces;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
    public class EmployeeOrganizationRepository : AsyncRepository<EmployeeOrganization>, IEmployeeOrganizationRepository
    {
        private readonly EPPContext _context;
        public EmployeeOrganizationRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
        public async Task<EmployeeOrganization> GetEmployeeOrganizationByEmployeeId(Guid employeeId)
        {
            var result = await _context.EmployeeOrganizations.Where(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
            return result;
        }
    }
}
