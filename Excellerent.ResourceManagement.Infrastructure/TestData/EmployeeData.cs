

using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellerent.ResourceManagement.Infrastructure.TestData
{
    public static class EmployeeData
    {
        public static List<Employee> sampleEmployees = new List<Employee>()
        {
            //new Employee()
            //{
            //    FirstName = "Ashnafi",
            //    FatherName = "Feseha",
            //    GrandFatherName = "Tesfaye",
            //    Gender="Female",
            //    DateofBirth=DateTime.Now,
            //    PersonalEmail="afiseha@gmail.com",
            //    MobilePhone="+251983351881",
            //    Nationality=new List<Nationality>()
            //    {
            //        new Nationality()
            //        {
            //            Name="Ethiopian"
            //        }
            //    },
            //    EmployeeOrganization= new EmployeeOrganization()
            //    {
            //        Country="Ethiopia",
            //        DutyBranch="Wengelawit A.A.",
            //        CompaynEmail="afiseha@excellerentsolutions.com",
            //        PhoneNumber="+251983351881",
            //        JobTitle="Developer",
            //        BusinessUnit="Software Development",
            //        Department="Development Team",
            //        ReportingManager=Guid.NewGuid().ToString(),
            //        EmploymentType="Full TIme Permanent",
            //        JoiningDate=DateTime.Now,
            //        Status="Active"

            //    }
            //},
            // new Employee()
            //{
            //    FirstName = "Joseph",
            //    FatherName = "Assefa",
            //    GrandFatherName = "Haile",
            //    Gender="Male",
            //    DateofBirth=DateTime.Now,
            //    PersonalEmail="aman@gmail.com",
            //    MobilePhone="+251945948911",
            //    Nationality=new List<Nationality>()
            //    {
            //        new Nationality()
            //        {
            //            Name="Ethiopian"
            //        }
            //    },
            //    EmployeeOrganization= new EmployeeOrganization()
            //    {
            //        Country="Ethiopia",
            //        DutyBranch="Wengelawit A.A.",
            //        CompaynEmail="yoseph@excellerentsolutions.com",
            //        PhoneNumber="251945948911",
            //        JobTitle="Developer",
            //        BusinessUnit="Software Development",
            //        Department="Development Team",
            //        ReportingManager=Guid.NewGuid().ToString(),
            //        EmploymentType="Full TIme Permanent",
            //        JoiningDate=DateTime.Now,
            //        Status="Active"

            //    }
            //},
            //new Employee()
            //{
            //    FirstName = "Amanuel",
            //    FatherName = "Zewedu",
            //    GrandFatherName = "Haile",
            //    Gender="Male",
            //    DateofBirth=DateTime.Now,
            //    PersonalEmail="amadgn@gmail.com",
            //    MobilePhone="+251965044554",
            //    Nationality=new List<Nationality>()
            //    {
            //        new Nationality()
            //        {
            //            Name="Ethiopian"
            //        }
            //    },
            //    EmployeeOrganization= new EmployeeOrganization()
            //    {
            //        Country="Ethiopia",
            //        DutyBranch="Wengelawit A.A.",
            //        CompaynEmail="amanenie@excellerentsolutions.com",
            //        PhoneNumber="251965044554",
            //        JobTitle="Developer",
            //        BusinessUnit="Software Development",
            //        Department="Development Team",
            //        ReportingManager=Guid.NewGuid().ToString(),
            //        EmploymentType="Full TIme Permanent",
            //        JoiningDate=DateTime.Now,
            //        Status="Active"

            //    }
            //}
            
            new Employee()
            {
                // FirstName = "Asechalew",
                // FatherName = "Erkun",
                // GrandFatherName = "Haile",
                FullName = "Asechalew Erkun Haile",
                Gender="Male",
                DateofBirth=DateTime.Now,
                PersonalEmail="asecsfsha@gmail.com",
                MobilePhone="+2519123444554",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    CountryId=Guid.NewGuid(),
                    DutyBranchId=Guid.NewGuid(),
                    CompaynEmail="asechuhuha@excellerentsolutions.com",
                    JobTitleId=Guid.NewGuid(),
                    DepartmentId=Guid.NewGuid(),
                    ReportingManager=Guid.NewGuid(),
                    EmploymentType="Full TIme Permanent",
                    JoiningDate=DateTime.Now,
                    Status="Active"

                }
            }
        };

        public static async void AddEmployee(EmployeeRepository repo)
        {
            if ((await repo.GetAllAsync()).Count() > 4) return;
            foreach(Employee employee in sampleEmployees)
            {
                var reply = repo.AddAsync(employee);
            }
        }
    }
}
