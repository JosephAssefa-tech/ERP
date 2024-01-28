using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Repository.Interfaces
{
    public interface IEmployeeOrganizationRepository : IAsyncRepository<EmployeeOrganization>
    {
        public Task<EmployeeOrganization> GetEmployeeOrganizationByEmployeeId(Guid employeeId);
    }
}
