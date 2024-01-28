using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Service;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.Repository.Interfaces;
using Excellerent.SharedModules.Services;
using System;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class EmployeeOrganizationService : CRUD<EmployeeOrganizationEntity, EmployeeOrganization>, IEmployeeOrganizationService
    {
        private readonly IEmployeeOrganizationRepository _repository;
        public EmployeeOrganizationService(IEmployeeOrganizationRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeOrganization> GetEmployeeOrganizationByEmployeeId(Guid employeeId)
        {
            var result = await _repository.GetEmployeeOrganizationByEmployeeId(employeeId);
            return result;
        }
    }
}
