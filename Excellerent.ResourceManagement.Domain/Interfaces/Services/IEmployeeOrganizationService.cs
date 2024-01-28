using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Service
{
    public interface IEmployeeOrganizationService : ICRUD<EmployeeOrganizationEntity, EmployeeOrganization>
    {
        public Task<EmployeeOrganization> GetEmployeeOrganizationByEmployeeId(Guid employeeId);
    }
}
