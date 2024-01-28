using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.EppConfiguration.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.TestData.ResourceManagement
{
    public static class EmployeeTestData
    {
        private static readonly DateTime _joiningDate = new DateTime(2021, 8, 2);
        private static readonly DateTime _dateofBirth = new DateTime(2000, 1, 1);

        private static Country country = new Country
        {
            Guid = Guid.NewGuid(),
            Name = "Ethiopia",
            Nationality = "Ethiopian",
            CreatedDate = DateTime.Now
        };
        private static DutyBranch dutyBranch = new DutyBranch
        {
            Guid = Guid.NewGuid(),
            CountryId = country.Guid,
            Name = "Addis Abeba Head Quarter",
            CreatedDate = DateTime.Now
            
        };

        private static Department department = new Department
        {
            Guid = Guid.NewGuid(),
            Name = "Software Development",
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };
        private static Role role = new Role
        {
            Guid = Guid.NewGuid(),
            DepartmentGuid = department.Guid,
            Name = "Senior Developer II",
            IsDeleted = false,
            CreatedDate = DateTime.Now

        };

        public static readonly List<Employee> _sampleEmployees = new List<Employee>()
        {
            new Employee()
            {
                Guid = Guid.NewGuid(),
                EmployeeNumber = "EDC-000",
                FirstName = "Admin",
                FatherName = "Admin",
                GrandFatherName = "Admin",
                FullName = "Admin Admin",
                Gender="Male",
                DateofBirth=_dateofBirth,
                PersonalEmail="aexcellerent@outlook.com",
                MobilePhone="+251910101010",
                IsDeleted = false,
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    CountryId=country.Guid,
                    DutyBranchId=dutyBranch.Guid,
                    CompaynEmail="aexcellerent@outlook.com",
                    JobTitleId=role.Guid,
                    DepartmentId=department.Guid,
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    IsDeleted = false,
                    Status="Active"

                }
            }
        };

        public static async Task Add(IEmployeeRepository repo, ICountryRepository countryRepository, IDutyBranchRepository dutyBranchRepository,
                                    IDepartmentRepository departmentRepository,IRoleRepository roleRepository)
        {
            IEnumerable<Employee> employees = await repo.GetAllAsync();

            //Insert Country
            await countryRepository.AddAsync(country);

            //Insert DutyBranch
            await dutyBranchRepository.AddAsync(dutyBranch);

            await departmentRepository.AddAsync(department);

            await roleRepository.AddAsync(role);

            // Insert employees' test data
            for (int i = 0; i < _sampleEmployees.Count; i++)
            {
                var employeeIn = employees.Where(x => x.FullName.Equals(_sampleEmployees[i].FullName)); // Where(x => x.FirstName.Equals(_sampleEmployees[i].FirstName));
                if (employeeIn.Count() == 0)
                {
                    _sampleEmployees[i].EmployeeOrganization.ReportingManager = _sampleEmployees[i].Guid;
                    _sampleEmployees[i] = await repo.AddAsync(_sampleEmployees[i]);
                }
                else
                {
                    _sampleEmployees[i] = employeeIn.FirstOrDefault();
                }
            }
        }
    }
}
