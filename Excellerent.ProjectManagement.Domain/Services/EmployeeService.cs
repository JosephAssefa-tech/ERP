using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellerent.ProjectManagement.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        List<Employee> employees = new List<Employee>();
        public EmployeeService()
        {
            employees.AddRange(
            new List<Employee> {

                    new Employee {
                        Guid= new Guid("1fa85f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Yosef Assefa",
                        Email="yosef@excellernet.com",
                        PhoneNumberPrefix="+251",
                        Phone=0945948911,
                        Role ="Developer",
                        HiredDate = new DateTime(2021,5,1)

                    },
                    new Employee {
                        Guid= new Guid("2fa86f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Yaschelew Erkihun",
                        Email="Yascechalew@excellernet.com",
                        PhoneNumberPrefix="+251",
                        Phone=090940902,
                        Role ="Developer",
                        HiredDate = new DateTime(2015,5,1)
                    },
                    new Employee{
                        Guid= new Guid("3fa87f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Abel Hailu",
                        Email="Abel@excellernet.com",
                        PhoneNumberPrefix="+61",
                        Phone=094232394,
                        Role ="Developer",
                        HiredDate = new DateTime(2015,5,1)
                    },
                    new Employee{
                        Guid= new Guid("4fa88f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Abebe Kebede",
                        Email="Abebe@excellernet.com",
                        PhoneNumberPrefix="+880",
                        Phone=0911939393,
                        Role ="QA Tester",
                        HiredDate = new DateTime(2015,5,1)
                    },
                    new Employee{
                        Guid= new Guid("5fa89f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Alemu Abraham",
                        Email="Alemu@excellernet.com",
                        PhoneNumberPrefix="+251",
                        Phone=092392982,
                        Role = "Product Owner",
                        HiredDate = new DateTime(2015,5,1)
                    },
                    new Employee{
                        Guid= new Guid("6fa89f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Yonas Alemu",
                        Email="yonas@excellernet.com",
                        PhoneNumberPrefix="+251",
                        Phone=092392982,
                        Role = "Sales Person",
                        HiredDate = new DateTime(2015,5,1)
                    },
                    new Employee{
                        Guid= new Guid("7fa89f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Yodit Ayalew",
                        Email="yodit@excellernet.com",
                        PhoneNumberPrefix="+251",
                        Phone=092392982,
                        Role = "Sales Person",
                        HiredDate = new DateTime(2015,5,1)
                    },
                    new Employee{
                        Guid= new Guid("8fa89f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Ermiyas Kebede",
                        Email="ermiyas@excellernet.com",
                        PhoneNumberPrefix="+251",
                        Phone=092392982,
                        Role = "TDM",
                        HiredDate = new DateTime(2015,5,1)
                    },
                    new Employee{
                        Guid= new Guid("9fa89f64-5717-4562-b3fc-2c963f66afa6"),
                        Name = "Sara Yakob",
                        Email="sara@excellernet.com",
                        PhoneNumberPrefix="+251",
                        Phone=092392982,
                        Role = "Product Owner",
                        HiredDate = new DateTime(2015,5,1)
                    }



            }


            );

        }
        public IEnumerable<Employee> GetEmployee()
        {

            return employees;

        }

        public List<Employee> GetEmployeeById(Guid id)
        {
            return employees.Where(x => x.Guid == id).ToList();

        }
    }
}
