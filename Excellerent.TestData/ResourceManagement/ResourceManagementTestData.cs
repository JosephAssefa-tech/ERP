using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.EppConfiguration.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.TestData.ResourceManagement
{
    public static class ResourceManagementTestData
    {
        public static async Task<bool> IsEmptyData(IEmployeeRepository employeeRepository)
        {
            List<Employee> employees = (await employeeRepository.GetAllAsync()).ToList();

            return employees.Count == 0;
        }
        public static async Task Add(IEmployeeRepository employeeRepository, ICountryRepository countryRepository, IDutyBranchRepository dutyBranchRepository,
                                    IDepartmentRepository departmentRepository, IRoleRepository roleRepository)
        {
            await EmployeeTestData.Add(employeeRepository, countryRepository, dutyBranchRepository, departmentRepository, roleRepository);
        }
    }
}
