using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Excellerent.ResourceManagement.Domain.DTOs;
using LinqKit;
using Excellerent.SharedModules.DTO;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : AsyncRepository<Employee>, IEmployeeRepository
    {
        private readonly EPPContext _context;
   

        public EmployeeRepository(EPPContext context ) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckEmailAddressExistence(string email, Guid guid)
        {
            var em = email.ToLower();
            var exists = _context.Employees.Where(e => !e.IsDeleted).Include(e => e.EmployeeOrganization).Where(e =>
                 e.Guid != guid &&
                 (e.PersonalEmail.ToLower().Equals(em)
                 || e.PersonalEmail2.ToLower().Equals(em)
                 || e.PersonalEmail3.ToLower().Equals(em)
                 || e.EmployeeOrganization.CompaynEmail.ToLower().Equals(em))
            );
            return exists.Count() > 0;
        }

        public async Task<bool> CheckPhoneNumberExistence(string phone, Guid guid)
        {
            return _context.Employees.Where(e => !e.IsDeleted).Include(e => e.Organization).Where(e =>
                 e.Guid != guid &&
                 (e.MobilePhone.Equals(phone)
                 || e.Phone1.Equals(phone)
                 || e.Phone2.Equals(phone))
            ).Count() > 0;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee emp)
        {
            await _context.Employees.AddAsync(emp);
            _context.SaveChanges();
            return emp;
        
        }

        public async  Task<List<Employee>> GetEmployeesAsync()
        {

            return await _context.Employees.Where(e => !e.IsDeleted).Where(e=>e.EmployeeNumber != "EDC-000")
                .Include(e => e.Nationality)
                .Include(e => e.EmployeeAddress)
                .Include(e => e.EmergencyContact)
                .Include(e => e.FamilyDetails)
                .Include(e => e.EmployeeOrganization).ThenInclude(o => o.Role)
                 .Include(e => e.EmployeeOrganization).ThenInclude(o => o.Country)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeesByEmailAsync(string email)
        {
            var result = await _context.Employees.Include(employeeorganization => employeeorganization.EmployeeOrganization).ThenInclude(jt => jt.Role).Where(x => x.EmployeeOrganization.CompaynEmail.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            return result;
        }


        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesDashboardAsync(Expression<Func<Employee, Boolean>> predicate, int pageIndex, int pageSize)
        {

            var employees = (predicate == null ? (await _context.Employees.Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false && d.EmployeeNumber != "EDC-000").OrderByDescending(o => o.CreatedDate).ToListAsync())
                            : (await _context.Employees.Where(predicate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).OrderByDescending(o => o.CreatedDate).Where(d => d.IsDeleted == false && d.EmployeeNumber != "EDC-000").ToListAsync()));


            int pageindex = 0;
             pageindex = pageIndex - 1;

            var employeePaginatedList = employees.Skip(pageindex * pageSize).Take(pageSize);
            List<EmployeeViewModel> employeeViewModelList = new List<EmployeeViewModel>();
            if (employeePaginatedList.Count() > 0)
            {
                foreach (Employee employee in employeePaginatedList)
                {
                    employeeViewModelList.Add(new EmployeeViewModel()
                    {
                        EmployeeGUid = employee.Guid,
                        FullName = employee.FullName, // employee.FirstName + " " + employee.GrandFatherName,
                        JobTitle = employee.EmployeeOrganization == null ? string.Empty : employee.EmployeeOrganization.Role.Name,
                        Status = employee.EmployeeOrganization ==  null? string.Empty : employee.EmployeeOrganization.Status,
                        Location = employee.EmployeeOrganization == null ? string.Empty : employee.EmployeeOrganization.Country.Name,
                        JoiningDate = employee.EmployeeOrganization == null ? new DateTime() : employee.EmployeeOrganization.JoiningDate,
                        OrganizationEmail=employee.EmployeeOrganization==null ? string.Empty :employee.EmployeeOrganization.CompaynEmail
                    });
                }
            }
            else
            {
                employeeViewModelList = null;
            }
            return employeeViewModelList;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesDashboardwithSortAsync(Expression<Func<Employee, Boolean>> predicate, EmployeeSpecParams paginationParams, int pageIndex, int pageSize)
        {
            var tmpemp = new List<Employee>();
            paginationParams.pageSize = paginationParams.pageSize ?? 10;
            paginationParams.pageIndex = paginationParams.pageIndex ?? 1;
            if (paginationParams.SortField != null)
            {
                switch (paginationParams.SortField)
                {
                    case "JobTitle":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderByDescending(x => x.EmployeeOrganization.Role.Name.ToLower()).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                            : (await _context.Employees.OrderByDescending(x => x.EmployeeOrganization.Role.Name.ToLower()).Where(predicate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").OrderByDescending(x => x.EmployeeOrganization.Role.Name).ToListAsync()));
                        }
                        else
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderBy(x => x.EmployeeOrganization.Role.Name.ToLower()).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                             : (await _context.Employees.OrderBy(x => x.EmployeeOrganization.Role.Name.ToLower()).Where(predicate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").OrderBy(x => x.EmployeeOrganization.Role.Name).ToListAsync()));
                        }
                        break;

                    case "FullName":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderByDescending(x => x.FullName.ToLower()) // .OrderByDescending(x => x.FirstName.ToLower())
                                .Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                            : (await _context.Employees.Where(predicate).OrderByDescending(x => x.FullName.ToLower()) // .OrderByDescending(x => x.FirstName)
                            .Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        }
                        else
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderBy(x => x.FullName.ToLower()) // .OrderBy(x => x.FirstName.ToLower())
                                .Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                             : (await _context.Employees.Where(predicate).OrderBy(x => x.FullName) // .OrderBy(x => x.FirstName)
                             .Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        }
                        break;

                    case "JoiningDate":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderByDescending(x => x.EmployeeOrganization.JoiningDate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                            : (await _context.Employees.Where(predicate).OrderByDescending(x => x.EmployeeOrganization.JoiningDate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        }
                        else
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderBy(x => x.EmployeeOrganization.JoiningDate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                             : (await _context.Employees.Where(predicate).OrderBy(x => x.EmployeeOrganization.JoiningDate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        }
                        break;

                    case "Location":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderByDescending(x => x.EmployeeOrganization.Country.Name.ToLower()).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                            : (await _context.Employees.Where(predicate).OrderByDescending(x => x.EmployeeOrganization.Country.Name.ToLower()).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        }
                        else
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderBy(x => x.EmployeeOrganization.Country.Name.ToLower()).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                             : (await _context.Employees.Where(predicate).OrderBy(x => x.EmployeeOrganization.Country.Name.ToLower()).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        }
                        break;

                    case "Status":
                        if (paginationParams.sortOrder == SharedModules.Seed.SortOrder.Descending)
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderByDescending(x => x.EmployeeOrganization.Status).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                            : (await _context.Employees.Where(predicate).OrderByDescending(x => x.EmployeeOrganization.Status).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        }
                        else
                        {
                            tmpemp = (predicate == null ? (await _context.Employees.OrderBy(x => x.EmployeeOrganization.Status).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync())
                             : (await _context.Employees.Where(predicate).OrderBy(x => x.EmployeeOrganization.Status).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        }
                        break;

                    default:
                        tmpemp = (predicate == null ? (await _context.Employees.Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").OrderByDescending(o => o.CreatedDate).ToListAsync())
                                 : (await _context.Employees.Where(predicate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).OrderByDescending(o => o.CreatedDate).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
                        throw new ArgumentException(nameof(predicate));

                }
            }
            else 
            {
                tmpemp = (predicate == null ? (await _context.Employees.Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").OrderByDescending(o => o.CreatedDate).ToListAsync())
                            : (await _context.Employees.Where(predicate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).OrderByDescending(o => o.CreatedDate).Where(d => d.IsDeleted == false).Where(d => d.EmployeeNumber != "EDC-000").ToListAsync()));
            }

            var employees = tmpemp; //(predicate == null ? (await _context.Employees.Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).Where(d => d.IsDeleted == false).OrderByDescending(o => o.CreatedDate).ToListAsync())
                                    //: (await _context.Employees.Where(predicate).Include(x => x.EmployeeOrganization).Include(x => x.EmployeeOrganization.Country).Include(y => y.EmployeeOrganization.Role).OrderByDescending(o => o.CreatedDate).ToListAsync()));

            int pageindex = 0;
            pageindex = pageIndex - 1;

            var employeePaginatedList = employees.Skip(pageindex * pageSize).Take(pageSize);

            List<EmployeeViewModel> employeeViewModelList = new List<EmployeeViewModel>();
            if (employeePaginatedList.Count() > 0)
            {
                foreach (Employee employee in employeePaginatedList)
                {
                    employeeViewModelList.Add(new EmployeeViewModel()
                    {
                        EmployeeGUid = employee.Guid,
                        FullName = employee.FullName, // employee.FirstName + " " + employee.GrandFatherName,
                        JobTitle = employee.EmployeeOrganization == null ? string.Empty : employee.EmployeeOrganization.Role.Name,
                        Status = employee.EmployeeOrganization == null ? string.Empty : employee.EmployeeOrganization.Status,
                        Location = employee.EmployeeOrganization == null ? string.Empty : employee.EmployeeOrganization.Country.Name,
                        JoiningDate = employee.EmployeeOrganization == null ? new DateTime() : employee.EmployeeOrganization.JoiningDate,
                        OrganizationEmail = employee.EmployeeOrganization == null ? string.Empty : employee.EmployeeOrganization.CompaynEmail,
                    });
                }
            }
            else
            {
                employeeViewModelList = null;
            }
            return employeeViewModelList;
        }


        public async Task<Employee> UpdateEmployee(Employee employee)
        {

            #region
            var existingEmp = _context.Employees
               .Include(n => n.Nationality)
                .Include(ce => ce.EmergencyContact)
                .Include(f => f.FamilyDetails)
                .Include(o => o.EmployeeOrganization)
                .Include(ea => ea.EmployeeAddress)
               .FirstOrDefault(e => e.Guid.Equals(employee.Guid));

            if (existingEmp == null)
                return null;
            else
            {
                // existingEmp.Photo = employee.Photo;
                existingEmp.EmployeeNumber = existingEmp.EmployeeNumber;
                // existingEmp.FirstName = employee.FirstName;
                // existingEmp.FatherName = employee.FatherName;
                // existingEmp.GrandFatherName = employee.GrandFatherName;
                existingEmp.FullName = employee.FullName;
                existingEmp.MobilePhone = employee.MobilePhone;
                existingEmp.Phone1 = employee.Phone1;
                existingEmp.Phone2 = employee.Phone2;
                existingEmp.DateofBirth = employee.DateofBirth;
                existingEmp.Gender = employee.Gender;
                existingEmp.PersonalEmail = employee.PersonalEmail;
                existingEmp.PersonalEmail = employee.PersonalEmail;
                existingEmp.PersonalEmail2 = employee.PersonalEmail2;
                existingEmp.PersonalEmail3 = employee.PersonalEmail3;

                if (existingEmp.Nationality.Count() > 0 && employee.Nationality.Count() > 0 && employee.Nationality.Count() == existingEmp.Nationality.Count())
                {
                    for (int i = 0; i < employee.Nationality.Count(); i++)
                    {
                        existingEmp.Nationality[i].Name = employee.Nationality[i].Name;

                    }
                }


                
                    //emergency contact
                    if (existingEmp.EmergencyContact.Count() == 0)
                    {
                        for (int i = 0; i < employee.EmergencyContact.Count(); i++)
                        {
                            existingEmp.EmergencyContact.Add(employee.EmergencyContact[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < employee.EmergencyContact.Count(); i++)
                        {
                            bool emailEdit = false;
                            if (employee.EmergencyContact[i].Guid == Guid.Parse("00000000-0000-0000-0000-000000000000")
                                && employee.EmergencyContact.Count() == existingEmp.EmergencyContact.Count()) { emailEdit = true; }

                            if (existingEmp.EmergencyContact.FindIndex(x => x.Guid == employee.EmergencyContact[i].Guid) != -1 || emailEdit == true)
                            {

                                // if (existingEmp.EmergencyContact[j].email == employee.EmergencyContact[i].email)

                                existingEmp.EmergencyContact[i].city = employee.EmergencyContact[i].city;
                                existingEmp.EmergencyContact[i].Country = employee.EmergencyContact[i].Country;

                                existingEmp.EmergencyContact[i].email = employee.EmergencyContact[i].email;

                                existingEmp.EmergencyContact[i].email2 = employee.EmergencyContact[i].email2;

                                existingEmp.EmergencyContact[i].email3 = employee.EmergencyContact[i].email3;
                                existingEmp.EmergencyContact[i].GrandFatherName = employee.EmergencyContact[i].GrandFatherName;

                                existingEmp.EmergencyContact[i].FatherName = employee.EmergencyContact[i].FatherName;
                                existingEmp.EmergencyContact[i].FirstName = employee.EmergencyContact[i].FirstName;
                                existingEmp.EmergencyContact[i].houseNumber = employee.EmergencyContact[i].houseNumber;
                                existingEmp.EmergencyContact[i].PhoneNumber = employee.EmergencyContact[i].PhoneNumber;

                                existingEmp.EmergencyContact[i].phoneNumber2 = employee.EmergencyContact[i].phoneNumber2;

                                existingEmp.EmergencyContact[i].phoneNumber3 = employee.EmergencyContact[i].phoneNumber3;

                                existingEmp.EmergencyContact[i].postalCode = employee.EmergencyContact[i].postalCode;

                                existingEmp.EmergencyContact[i].stateRegionProvice = employee.EmergencyContact[i].stateRegionProvice;
                                existingEmp.EmergencyContact[i].Relationship = employee.EmergencyContact[i].Relationship;
                                existingEmp.EmergencyContact[i].woreda = employee.EmergencyContact[i].woreda;
                                existingEmp.EmergencyContact[i].subCityZone = employee.EmergencyContact[i].subCityZone;

                                // break;
                            }
                            else
                            {
                                //jobtitle.findIndex(x=>x.text.trim() === response.Data.jobtype[i].Name.trim()) === -1 
                                if (employee.EmergencyContact[i].Guid == Guid.Parse("00000000-0000-0000-0000-000000000000") &&
                                    employee.EmergencyContact.Count() > existingEmp.EmergencyContact.Count())
                                {

                                    existingEmp.EmergencyContact.Add(employee.EmergencyContact[i]);

                                    break;
                                }
                                //existingEmp.EmergencyContact.Add(employee.EmergencyContact[i]);
                            }
                        }


                    }
                }

                //personal address

                if (existingEmp.EmployeeAddress.Count() == 0)
            {
                for (int i = 0; i < employee.EmployeeAddress.Count(); i++)
                {
                    existingEmp.EmployeeAddress.Add(employee.EmployeeAddress[i]);
                }
            }
            else
            {

                for (int i = 0; i < employee.EmployeeAddress.Count(); i++)
                {
                    bool emailEdit = false;
                    if (employee.EmployeeAddress[i].Guid == Guid.Parse("00000000-0000-0000-0000-000000000000")
                        && employee.EmployeeAddress.Count() == existingEmp.EmployeeAddress.Count()) { emailEdit = true; }

                    if (existingEmp.EmployeeAddress.FindIndex(x => x.Guid == employee.EmployeeAddress[i].Guid) != -1 || emailEdit == true)
                    {
                        // if (existingEmp.EmployeeAddress[j].Country == employee.EmployeeAddress[i].Country)

                        existingEmp.EmployeeAddress[i].Country = employee.EmployeeAddress[i].Country;
                        existingEmp.EmployeeAddress[i].City = employee.EmployeeAddress[i].City;
                        existingEmp.EmployeeAddress[i].HouseNumber = employee.EmployeeAddress[i].HouseNumber;
                        existingEmp.EmployeeAddress[i].PhoneNumber = employee.EmployeeAddress[i].PhoneNumber;
                        existingEmp.EmployeeAddress[i].PostalCode = employee.EmployeeAddress[i].PostalCode;
                        existingEmp.EmployeeAddress[i].StateRegionProvice = employee.EmployeeAddress[i].StateRegionProvice;
                        existingEmp.EmployeeAddress[i].SubCityZone = employee.EmployeeAddress[i].SubCityZone;
                        existingEmp.EmployeeAddress[i].Woreda = employee.EmployeeAddress[i].Woreda;
                    }

                    else
                    {
                        //jobtitle.findIndex(x=>x.text.trim() === response.Data.jobtype[i].Name.trim()) === -1 
                        if (employee.EmployeeAddress[i].Guid == Guid.Parse("00000000-0000-0000-0000-000000000000") &&
                            employee.EmployeeAddress.Count() > existingEmp.EmployeeAddress.Count())
                        {
                            existingEmp.EmployeeAddress.Add(employee.EmployeeAddress[i]);
                        }
                        //existingEmp.EmergencyContact.Add(employee.EmergencyContact[i]);

                    }
                }

            }



            //familydetail
            if (existingEmp.FamilyDetails.Count() == 0)
            {
                for (int i = 0; i < employee.FamilyDetails.Count(); i++)
                {
                    existingEmp.FamilyDetails.Add(employee.FamilyDetails[i]);
                }
            }
            else
            {

                for (int i = 0; i < employee.FamilyDetails.Count(); i++)
                {
                    bool emailEdit = false;
                    
                    if (employee.FamilyDetails[i].Guid == Guid.Parse("00000000-0000-0000-0000-000000000000")
                        && employee.FamilyDetails.Count() == existingEmp.FamilyDetails.Count()) { emailEdit = true;  }

                    if (existingEmp.FamilyDetails.FindIndex(x => x.Guid == employee.FamilyDetails[i].Guid) != -1 || emailEdit == true)
                    {
                        existingEmp.FamilyDetails[i].FullName = employee.FamilyDetails[i].FullName;
                      //  existingEmp.FamilyDetails[i].RelationshipId = employee.FamilyDetails[i].RelationshipId;
                        existingEmp.FamilyDetails[i].Gender = employee.FamilyDetails[i].Gender;
                        existingEmp.FamilyDetails[i].DoB = employee.FamilyDetails[i].DoB;
                        existingEmp.FamilyDetails[i].Remark = employee.FamilyDetails[i].Remark;
                    }
                    else
                    {
                        if (employee.FamilyDetails[i].Guid == Guid.Parse("00000000-0000-0000-0000-000000000000") &&
                            employee.FamilyDetails.Count() > existingEmp.FamilyDetails.Count())
                        {
                            existingEmp.FamilyDetails.Add(employee.FamilyDetails[i]);
                        }
                        //existingEmp.EmergencyContact.Add(employee.EmergencyContact[i]);
                    }
                }

            }
        
                
                 if (existingEmp.EmployeeOrganization != null)
                 {

                     // existingEmp.EmployeeOrganization.Branch.Country = employee.EmployeeOrganization.Branch.Country;
                     existingEmp.EmployeeOrganization.CompaynEmail = employee.EmployeeOrganization.CompaynEmail;
                     existingEmp.EmployeeOrganization.Country = employee.EmployeeOrganization.Country;
                     existingEmp.EmployeeOrganization.DepartmentId = employee.EmployeeOrganization.DepartmentId;
                     existingEmp.EmployeeOrganization.DutyBranch = employee.EmployeeOrganization.DutyBranch;
                     existingEmp.EmployeeOrganization.JobTitleId = employee.EmployeeOrganization.JobTitleId;
                     existingEmp.EmployeeOrganization.JoiningDate = employee.EmployeeOrganization.JoiningDate;
                     existingEmp.EmployeeOrganization.ReportingManager = employee.EmployeeOrganization.ReportingManager;
                     existingEmp.EmployeeOrganization.Status = employee.EmployeeOrganization.Status;
                     existingEmp.EmployeeOrganization.TerminationDate = employee.EmployeeOrganization.TerminationDate;
                 }
            // update user status to In-active if the employee status is not Active
            if (!employee.EmployeeOrganization.Status.Equals("Active"))
            {
                var userInfo = await _context.Users.Where(emp => emp.EmployeeId == employee.Guid).FirstOrDefaultAsync();
                if (userInfo != null)
                {
                    userInfo.Status = Usermanagement.Domain.Enums.UserStatus.NotActive;
                    _context.Users.Update(userInfo);
                }
            }
            _context.SaveChanges(true);

               return employee;
             }
            #endregion
        
        public async Task<int> AllEmployeesDashboardCountAsync(Expression<Func<Employee, Boolean>> predicate)
        {
            var employeeList = (predicate == null ? (await _context.Employees.Include(x => x.EmployeeOrganization).Where(d => d.IsDeleted == false && d.EmployeeNumber != "EDC-000").OrderByDescending(o => o.CreatedDate).ToListAsync())
                : (await _context.Employees.Include(x => x.EmployeeOrganization).Where(d => d.IsDeleted == false && d.EmployeeNumber != "EDC-000").Where(predicate).OrderByDescending(o => o.CreatedDate).ToListAsync()));
            
            return employeeList.Count;
        }
        public Employee GetEmployeesById(Guid empId)
        {
            var result =  _context.Employees
                .Include(x=>x.EmployeeOrganization)
                .Include(x=>x.EmployeeOrganization.Country)
                .Include(x => x.EmployeeOrganization.DutyBranch)
                .Include(x => x.EmployeeOrganization.Department)
                .Include(x => x.EmployeeOrganization.Role)
                .Include(x=>x.FamilyDetails)
                .ThenInclude(r=>r.Relationship)
                .Include(x=>x.Nationality)
                .Include(x=>x.EmergencyContact)
                .Include(x=>x.EmployeeAddress)
                .Where(x => x.Guid == empId).FirstOrDefault();

            return result;

        }

        public bool UpdatePersonalInfoEmployee(Employee employeeEntity)
        {
            var existingEmp =  _context.Employees.Where(e => e.Guid == employeeEntity.Guid).FirstOrDefault();
            if (existingEmp == null)
                return false;
            else
            {
                // existingEmp.FirstName = employeeEntity.FirstName;
                // existingEmp.FatherName = employeeEntity.FatherName;
                // existingEmp.GrandFatherName = employeeEntity.GrandFatherName;
                existingEmp.FullName = employeeEntity.FullName;

                existingEmp.MobilePhone = employeeEntity.MobilePhone;
                existingEmp.Phone1 = employeeEntity.Phone1;
                existingEmp.Phone2 = employeeEntity.Phone2;

                existingEmp.DateofBirth = employeeEntity.DateofBirth;
                existingEmp.Gender = employeeEntity.Gender;
                existingEmp.Nationality = employeeEntity.Nationality;
                existingEmp.PersonalEmail = employeeEntity.PersonalEmail;

                existingEmp.PersonalEmail = employeeEntity.PersonalEmail;
                existingEmp.PersonalEmail2 = employeeEntity.PersonalEmail2;
                existingEmp.PersonalEmail3 = employeeEntity.PersonalEmail3;

                 _context.SaveChanges(true);

            }
            return true;
        }
        public bool UpdatePersonalAddressEmployee(Employee employeeEntity)
        {
            var existingEmp = _context.Employees.Where(e => e.Guid == employeeEntity.Guid).Include(pa => pa.EmployeeAddress).FirstOrDefault();
            if (existingEmp == null)
                return false;
            else
            {
                existingEmp.EmployeeAddress = employeeEntity.EmployeeAddress;

                 _context.SaveChangesAsync(true);
            }
            return true;
        }

        public bool UpdateOrgDetailEmployee(Employee employeeEntity)
        {
            var existingEmp = _context.Employees.Where(e => e.Guid == employeeEntity.Guid).Include(pa => pa.EmployeeOrganization).FirstOrDefault();
            if (existingEmp == null)
                return false;
            else
            {
                existingEmp.EmployeeOrganization = employeeEntity.EmployeeOrganization;

                 _context.SaveChanges(true);
            }
            return true;
        }

        public bool UpdateFamilyDetailEmployee(Employee employeeEntity)
        {
            var existingEmp = _context.Employees.Where(e => e.Guid == employeeEntity.Guid).Include(pa => pa.FamilyDetails).FirstOrDefault();
            if (existingEmp == null)
                return false ;
            else
            {
                existingEmp.FamilyDetails = employeeEntity.FamilyDetails;

                 _context.SaveChanges(true);
            }
            return true;
        }

        public bool UpdateContactEmployee(Employee employeeEntity)
        {
            var existingEmp = _context.Employees.Where(e => e.Guid == employeeEntity.Guid).Include(pa => pa.EmergencyContact).FirstOrDefault();
            if (existingEmp == null)
                return false;
            else
            {
                existingEmp.EmergencyContact = employeeEntity.EmergencyContact;

                 _context.SaveChanges(true);
            }
            return true;
        }

        public async Task<bool> ChangeIsDeletedStatus(Employee employee)
        {
            employee.IsDeleted = true;
            _context.Employees.Update(employee);
            var userInfo = await _context.Users.Where(emp => emp.EmployeeId == employee.Guid).FirstOrDefaultAsync();
            if (userInfo != null)
            {
                userInfo.IsActive = false;
                userInfo.Status = Usermanagement.Domain.Enums.UserStatus.NotActive;
                userInfo.IsDeleted = true;
                _context.Users.Update(userInfo);
            }
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<ReportingManager>> GetEmployeeListForReportingManager()
        {
           var employeeList = await _context.Employees.Include(jt=>jt.EmployeeOrganization.Role).Where(x=>x.IsDeleted == false).ToListAsync();
           List<ReportingManager> objReportingManager = new List<ReportingManager>();
           foreach(Employee employee in employeeList)
           {
                objReportingManager.Add(new ReportingManager()
                {
                    Id = employee.Guid,
                    EmployeeName = employee.FullName // employee.FirstName + ' ' + (employee.FatherName != null ? employee.FatherName + " " : " ") + employee.GrandFatherName 
                                    + " (" + employee.EmployeeOrganization.Role.Name + ')'
                });
           }
           return objReportingManager;
        }

        public Employee GetEmployeesByEmpNumber(string empId)
        {
            var result = _context.Employees
              .Include(x => x.EmployeeOrganization)
              .Include(x => x.FamilyDetails)
              .ThenInclude(r => r.Relationship)
              .Include(x => x.Nationality)
              .Include(x => x.EmergencyContact)
              .Include(x => x.EmployeeAddress)
              .Where(x => x.EmployeeNumber == empId).FirstOrDefault();

            return result;
        }

        public async Task<bool> CheckIdNumber(string idNumber)
        {
            return (await GetAllAsync()).Count(e =>     e.EmployeeNumber.StartsWith(idNumber)) == 0;
        }

        public async Task<bool> IsEmpForDepartment(Guid id)
        {
            var employeeList = await _context.Employees.Where(x => x.EmployeeOrganization.DepartmentId == id).ToListAsync();
             bool result = false;
             result = (employeeList.Count() > 0 ? (true) :  (false));
            if (result == false) 
            {
                var RolesList = await _context.Roles.Where(x => x.DepartmentGuid == id).ToListAsync();
                result = (RolesList.Count() > 0 ? (true) : (false));
            }

            return result;
        }

        public async Task<bool> IsEmpForRole(Guid id)
        {
            var employeeList = await _context.Employees.Where(x => x.EmployeeOrganization.Role.Guid == id).ToListAsync();
            bool result = false;
            result = (employeeList.Count() > 0 ? (true) : (false));
            

            return result;
        }

        public async Task<bool> IsEmpForCountry(Guid id)
        {
            var employeeList = await _context.Employees.Where(x => x.EmployeeOrganization.Country.Guid == id).ToListAsync();
            bool result = false;
            result = (employeeList.Count() > 0 ? (true) : (false));
            if (result == false)
            {
                var RolesList = await _context.DutyBranches.Where(x => x.Country.Guid == id).ToListAsync();
                result = (RolesList.Count() > 0 ? (true) : (false));
            }

            return result;
        }

        public async Task<bool> IsEmpForDutyStation(Guid id)
        {
            var employeeList = await _context.Employees.Where(x => x.EmployeeOrganization.DutyBranch.Guid == id).ToListAsync();
            bool result = false;
            result = (employeeList.Count() > 0 ? (true) : (false));

            return result;
        }
        public  async  Task AssignEmployeeToTempoLeaveProject(Guid emmployyeeGuid)
        {
            try {
                  await UnitOfWork.BeginTransaction();
                var medcal = _context.Project.Where(p => p.ProjectName.Contains( "Medical/Maternity"));
                var casualLeave = _context.Project.Where(p => p.ProjectName.Contains( "Casual Leave"));
                var sickLeave = _context.Project.Where(p => p.ProjectName.Contains( "Sick Leave"));
                var vacation = _context.Project.Where(p => p.ProjectName.Contains("Vacation"));
                if (medcal != null && vacation != null && sickLeave != null && casualLeave != null)
                {
                    await _context.AssignResources.AddAsync(new ProjectManagement.Domain.Models.AssignResourcEntity() { ProjectGuid = medcal.First().Guid, AssignDate = new DateTime(), EmployeeGuid = emmployyeeGuid });
                    await _context.AssignResources.AddAsync(new ProjectManagement.Domain.Models.AssignResourcEntity() { ProjectGuid = casualLeave.First().Guid, AssignDate = new DateTime(), EmployeeGuid = emmployyeeGuid });
                    await _context.AssignResources.AddAsync(new ProjectManagement.Domain.Models.AssignResourcEntity() { ProjectGuid = sickLeave.First().Guid, AssignDate = new DateTime(), EmployeeGuid = emmployyeeGuid });
                    await _context.AssignResources.AddAsync(new ProjectManagement.Domain.Models.AssignResourcEntity() { ProjectGuid = vacation.First().Guid, AssignDate = new DateTime(), EmployeeGuid = emmployyeeGuid });
                    await UnitOfWork.CommitTransaction();           
                }
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollBackTransaction();
            }
        }

        public async Task<EmployeeCounts> GetEmployeesCounts() 
        {
            var billableEmployees = await _context.AssignResources.Where(x => x.Billable == true && x.Project.ProjectType 
                                    == ProjectManagement.Domain.Models.ProjectType.External && x.IsDeleted==false && x.IsActive==true &&
                                    x.Project.Client.ClientName.ToLower() != "Leave".ToLower() && (x.Project.IsActive == true && x.Project.IsDeleted == false)).CountAsync();

            var NonbillableEmployees = await _context.AssignResources.Where(x => x.Billable == false && x.Project.ProjectType
                                       == ProjectManagement.Domain.Models.ProjectType.External && x.IsDeleted == false && x.IsActive == true &&
                                        x.Project.Client.ClientName.ToLower() != "Leave".ToLower() 
                                        && (x.Project.IsActive == true && x.Project.IsDeleted == false)).CountAsync();

            var internalEmployees = await _context.AssignResources.Where(x => x.Project.ProjectType 
                                        == ProjectManagement.Domain.Models.ProjectType.Internal && x.Billable == false && x.IsDeleted == false &&
                                        x.IsActive == true && x.Project.Client.ClientName.ToLower() != "Leave".ToLower()
                                        && (x.Project.IsActive == true && x.Project.IsDeleted == false)).CountAsync();

            var subselect  = await (from b in _context.Employees where b.IsActive==true && b.IsDeleted==false && b.EmployeeNumber != "EDC-000" select b.Guid).ToListAsync();
            var subselectNotAssigned = await (from b in _context.AssignResources where b.IsActive == true && b.IsDeleted == false select b.EmployeeGuid).ToListAsync();
            var subselectDiff = await (from c in _context.Employees
                                       where !subselectNotAssigned.Contains(c.Guid) && c.EmployeeNumber != "EDC-000" && c.IsActive == true && c.IsDeleted == false
                                       select c).CountAsync();


            var benchEmployees = await (from c in _context.AssignResources where subselect.Contains(c.EmployeeGuid) && c.IsActive==true &&
                          c.IsDeleted==false && (c.Project.IsActive==false && c.Project.IsDeleted==true) select c).CountAsync();

            benchEmployees+=subselectDiff;


            EmployeeCounts totalCounts = new EmployeeCounts
            {
                billable = billableEmployees,
                nonBillable = NonbillableEmployees,
                internalEmp = internalEmployees,
                benchEmp = benchEmployees
            };
            return totalCounts;

        }

        public async Task<PredicatedResponseDTO> GetEmployeeAssignments(EmpAssignmentSpecParams paginationParams)
        {

            List<EmployeeBillabilityDto> EmployeeAssignments = new List<EmployeeBillabilityDto>();
            EmployeeBillabilityDto employeeBillabilityDto; 

            int itemPerPage = paginationParams.pageSize;
            int PageIndex = paginationParams.pageIndex;

            int TotalRowCount = 0;
            var predicate = PredicateBuilder.New<ProjectManagement.Domain.Models.AssignResourcEntity>();
            var predicateRole = PredicateBuilder.New<ProjectManagement.Domain.Models.AssignResourcEntity>();
            var predicateReportingManager = PredicateBuilder.New<ProjectManagement.Domain.Models.AssignResourcEntity>();
            var predicateEmpType = PredicateBuilder.New<ProjectManagement.Domain.Models.AssignResourcEntity>();

            if (paginationParams.Role != null)
            {
                foreach (var role in paginationParams.Role)
                {
                    predicateRole = predicateRole.Or(c => c.Empolyee.EmployeeOrganization.Role.Name == role);
                }
            }

            if (paginationParams.ReportingManager != null)
            {

                foreach (var manager in paginationParams.ReportingManager)
                {
                    predicateReportingManager = predicateReportingManager.Or(c => c.Empolyee.EmployeeOrganization.ReportingManager.ToString() == manager);
                }
            }
            if (paginationParams.EmpType != null)
            {

                if (paginationParams.EmpType == Enum.GetName(typeof(EmpStatusTypes), EmpStatusTypes.Bench))
                    predicateEmpType = predicateEmpType.Or(d => d.IsDeleted == false).And(p => p.Project.IsActive == false);
                else if (paginationParams.EmpType == Enum.GetName(typeof(EmpStatusTypes), EmpStatusTypes.Billable))
                    predicateEmpType = predicateEmpType.Or(d => d.IsDeleted == false).And(p => p.Billable == true && p.Project.ProjectType == ProjectManagement.Domain.Models.ProjectType.External 
                    && p.Project.IsActive == true && p.IsActive == true && p.IsDeleted == false && p.Project.Client.ClientName.ToLower() != "Leave".ToLower());
                else if (paginationParams.EmpType == Enum.GetName(typeof(EmpStatusTypes), EmpStatusTypes.NonBillable))
                    predicateEmpType = predicateEmpType.Or(d => d.IsDeleted == false).And(p => p.Project.ProjectType == ProjectManagement.Domain.Models.ProjectType.External && p.Billable == false 
                    && p.Project.IsActive == true && p.IsActive == true && p.IsDeleted == false && p.Project.Client.ClientName.ToLower() != "Leave".ToLower());
                else if (paginationParams.EmpType == Enum.GetName(typeof(EmpStatusTypes), EmpStatusTypes.Internal))
                    predicateEmpType = predicateEmpType.Or(d => d.IsDeleted == false).And(p => p.Project.ProjectType == ProjectManagement.Domain.Models.ProjectType.Internal
                    && p.Project.IsActive == true && p.Project.IsDeleted == false && p.Billable == false && p.IsActive == true && p.IsDeleted == false && p.Project.Client.ClientName != "Leave".ToLower());
                
            }


            var employees = await (from b in _context.Employees
                                   where (b.IsActive == true && b.IsDeleted == false ) select b).ToListAsync();

            if (paginationParams.Role == null
                    && paginationParams.ReportingManager == null
                    && paginationParams.EmpType == null)
            {
                predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? null
                 : predicate.And(p => p.Empolyee.FullName.ToLower().Contains(paginationParams.searchKey.ToLower())) 
                 .And(d => d.IsDeleted == false).And(d => d.Empolyee.EmployeeNumber != "EDC-000");
            }

            if (paginationParams.Role != null)
            {

                predicate = predicate.And(predicateRole);
                predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                       : predicate.And(d => d.IsDeleted == false).And(p => p.Empolyee.EmployeeOrganization.Role.Name.Contains(paginationParams.searchKey.ToLower()));
            }
            if (paginationParams.ReportingManager != null)
            {
                predicate = predicate.And(predicateReportingManager);
                predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                      : predicate.And(d => d.IsDeleted == false).And(p => p.Empolyee.EmployeeOrganization.ReportingManager.ToString().Contains(paginationParams.searchKey.ToLower()));

            }
            if (paginationParams.EmpType != null)
            {
                predicate = predicate.And(predicateEmpType);
                if(paginationParams.EmpType == Enum.GetName(typeof(EmpStatusTypes), EmpStatusTypes.Bench))
                   predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                      : predicate.And(d => d.IsDeleted == false).And(p => p.Project.IsActive == false).And(p => p.Empolyee.FullName.ToLower().Contains(paginationParams.searchKey.ToLower())); 
                else if(paginationParams.EmpType == Enum.GetName(typeof(EmpStatusTypes), EmpStatusTypes.Billable))
                    predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                      : predicate.And(d => d.IsDeleted == false).And(p => p.Billable == true && p.Project.ProjectType == ProjectManagement.Domain.Models.ProjectType.External 
                      && p.Project.IsActive == true && p.IsActive == true && p.IsDeleted == false && p.Project.Client.ClientName.ToLower() != "Leave".ToLower())
                      .And(p => p.Empolyee.FullName.ToLower().Contains(paginationParams.searchKey.ToLower())); 
                else if(paginationParams.EmpType == Enum.GetName(typeof(EmpStatusTypes), EmpStatusTypes.NonBillable))
                    predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                      : predicate.And(d => d.IsDeleted == false).And(p => p.Project.ProjectType == ProjectManagement.Domain.Models.ProjectType.External && p.Billable == false 
                      && p.Project.IsActive == true && p.IsActive == true && p.IsDeleted == false && p.Project.Client.ClientName.ToLower() != "Leave".ToLower())
                      .And(p => p.Empolyee.FullName.ToLower().Contains(paginationParams.searchKey.ToLower()));
                else if(paginationParams.EmpType == Enum.GetName(typeof(EmpStatusTypes), EmpStatusTypes.Internal))
                    predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                      : predicate.And(d => d.IsDeleted == false).And(p => p.Project.ProjectType == ProjectManagement.Domain.Models.ProjectType.Internal
                      && p.Project.IsActive == true && p.Project.IsDeleted == false && p.Billable == false && p.IsActive == true && p.IsDeleted == false && p.Project.Client.ClientName != "Leave".ToLower())
                      .And(p => p.Empolyee.FullName.ToLower().Contains(paginationParams.searchKey.ToLower()));

            }


            List<ProjectManagement.Domain.Models.AssignResourcEntity> BillabilityData = predicate != null ?
                (await (from b in _context.AssignResources.Where(predicate).Include(a => a.Project).Include(a => a.Empolyee).Include(a => a.Empolyee.EmployeeOrganization).Include(a => a.Empolyee.EmployeeOrganization.Role)
                        where b.IsActive == true && b.IsDeleted == false
                        select b).ToListAsync()) :
                (await (from b in _context.AssignResources.Include(a => a.Project).Include(a => a.Empolyee).Include(a => a.Empolyee.EmployeeOrganization).Include(a => a.Empolyee.EmployeeOrganization.Role)
                        where b.IsActive == true && b.IsDeleted == false
                        select b).ToListAsync());


            int pageindex = 0;
            pageindex = PageIndex - 1;
            TotalRowCount = BillabilityData.Count;

            var employeePaginatedList = BillabilityData.Skip(pageindex * itemPerPage).Take(itemPerPage);


            foreach (ProjectManagement.Domain.Models.AssignResourcEntity employee in employeePaginatedList) {
                
                employeeBillabilityDto = new EmployeeBillabilityDto();
                employeeBillabilityDto.guid = employee.EmployeeGuid;
                employeeBillabilityDto.employeeName = employee.Empolyee.FullName;
                employeeBillabilityDto.jobTitle = employee.Empolyee.EmployeeOrganization.Role.Name;
                employeeBillabilityDto.photo = employee.Empolyee.Photo;
                    if (employee.Empolyee.EmployeeOrganization.ReportingManager.ToString() != "00000000-0000-0000-0000-000000000000")
                      employeeBillabilityDto.reportingManager = employees.Find(x => x.Guid == (employees.Find(x => x.Guid == employee.EmployeeGuid).EmployeeOrganization.ReportingManager)).FullName;
                    else
                        employeeBillabilityDto.reportingManager = "N/A";


                employeeBillabilityDto.EmpType = paginationParams.EmpType;
             

                EmployeeAssignments.Add(employeeBillabilityDto);               
            }
           

            return new PredicatedResponseDTO()
            {
                Data = EmployeeAssignments,
                TotalRecord = TotalRowCount,
                PageIndex = paginationParams.pageIndex,
                PageSize = paginationParams.pageSize,
                TotalPage = (int) Math.Ceiling(Convert.ToDecimal(TotalRowCount)/Convert.ToDecimal(paginationParams.pageSize))

            };
        }

    }
}