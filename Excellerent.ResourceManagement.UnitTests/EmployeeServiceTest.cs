using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.Services;
using Excellerent.SharedModules.DTO;
using LinqKit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ResourceManagement.UnitTests
{
    public class EmployeeServiceTest
    {
        private readonly EmployeeService _EmpService;
        private readonly Mock<IEmployeeRepository> EmpRepository = new Mock<IEmployeeRepository>();

        public EmployeeServiceTest()
        {
            _EmpService = new EmployeeService(EmpRepository.Object);
        }

        [Fact]
        public async Task GetAllEmployees()
        {
            Guid EmpId = Guid.NewGuid();
            var EmployeeEntity = new List<Employee>() {
            new Employee()
            {
                Guid = EmpId,
                // FirstName = "abel",
                // FatherName = "abel",
                // GrandFatherName = "abel",
                FullName = "abel abel abel",
                DateofBirth = DateTime.Now,
                Nationality = new List<Nationality>() {

                    new Nationality() {
                        Guid = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Ethiopian",
                        CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    }
                },
                IsActive = true,
                EmployeeOrganization = new EmployeeOrganization() {
                    Guid = Guid.NewGuid(),
                    CountryId = Guid.NewGuid(),
                    IsActive = true,
                    DutyBranchId = Guid.NewGuid(),
                    DepartmentId = Guid.NewGuid(),
                    CompaynEmail = "d@mail.com",
                    CreatedbyUserGuid = Guid.NewGuid(),
                    EmploymentType = "FullTime Permanent",
                    JobTitleId = Guid.NewGuid(),
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    EmployeeId = Guid.NewGuid(),
                    JoiningDate = DateTime.Now,
                    ReportingManager = Guid.NewGuid(),
                    Status = "Active",
                    TerminationDate = DateTime.Now
                },
                CreatedbyUserGuid = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                EmergencyContact = new List<EmergencyContactsModel>() {
                new EmergencyContactsModel() {
                        Guid = Guid.NewGuid(),
                        CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        FatherName = "abel",
                        FirstName = "abel",
                        IsActive = true,
                        IsDeleted = false,
                        Relationship = "bro",
                         houseNumber ="123123",
                          GrandFatherName="abel",
                           postalCode = "12313",
                            woreda = "12",
                              subCityZone = "Bole",
                               city = "AA",
                                Country = "Et",
                                 email = "nwwqaa@gmail.com",
                                  email2 = "nwqa@gmail.com",
                                   email3 ="retree@gmail.com",
                                    PhoneNumber = "+251987654",
                                     phoneNumber2 = "+251467892",
                                      phoneNumber3 ="+251987654",
                                       stateRegionProvice ="AA"

                }},
                Gender = "male",
                IsDeleted = false,
                MobilePhone = "25145126398",
                PersonalEmail = "asdasd@mail.com",
                PersonalEmail2 = "wasdasd@mail.com",
                PersonalEmail3 = "qasdasd@mail.com",
                Phone1 = "251879652",
                Phone2 = "251687541",
                Photo = "",
                EmployeeAddress = new List<PersonalAddress>() {

                            new PersonalAddress(){
                             Guid = Guid.NewGuid(),
                              City = "Addis",
                               Country="Ethio",
                                CreatedbyUserGuid = Guid.NewGuid(),
                                 CreatedDate = DateTime.Now,
                                  HouseNumber = "1452545",
                                   IsActive = true,
                                    IsDeleted = false,
                                     PhoneNumber = "25147856324",
                                      PostalCode = "12545",
                                       StateRegionProvice = "Addis",
                                        SubCityZone = "Bole",
                                         Woreda = "12"
                            }
                        },
                 FamilyDetails = new List<FamilyDetails>()
                 {
                     new FamilyDetails(){
                      Guid = Guid.NewGuid(),
                       CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                          DoB = DateTime.Now,
                           EmployeeId = Guid.NewGuid(),
                            RelationshipId = Guid.NewGuid(),
                              FullName = "abel",
                               Gender = "male",
                                IsActive = true,
                                 IsDeleted = false,

                                   Remark = "ok",
                                     Relationship = new Relationship()


                     }
                 }

                 }
            }; 



            EmpRepository.Setup(x => x.GetEmployeesAsync()).ReturnsAsync(EmployeeEntity);
            //Act
            var retrivedEmp = await _EmpService.GetAllEmployeesAsync();
            //Assert
            Assert.Equal(EmployeeEntity, retrivedEmp);



        }

        [Fact]
        public async Task AddNewEmployeeEntry() 
        {
            Guid EmpId = Guid.NewGuid();
            var EmployeeEntity = new Employee() { 
           
                Guid = EmpId,
                FirstName = "abel",
                FatherName = "abel",
                GrandFatherName = "abel",
                DateofBirth = DateTime.Now,
                Nationality = new List<Nationality>() {

                    new Nationality() {
                        Guid = EmpId,
                        IsActive = true,
                        Name = "Ethiopian",
                        CreatedbyUserGuid = EmpId,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    }
                },
                IsActive = true,
                EmployeeOrganization = new EmployeeOrganization() {
                    Guid = EmpId,
                    CountryId = Guid.NewGuid(),
                    IsActive = true,
                    DutyBranchId = Guid.NewGuid(),
                    DepartmentId = Guid.NewGuid(),
                    CompaynEmail = "d@mail.com",
                    CreatedbyUserGuid = EmpId,
                    EmploymentType = "1",
                    JobTitleId = Guid.NewGuid(),
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    EmployeeId = EmpId,
                    JoiningDate = DateTime.Now,
                    ReportingManager = Guid.NewGuid(),
                    Status = "1",
                    TerminationDate = DateTime.Now
                },
                CreatedbyUserGuid = EmpId,
                CreatedDate = DateTime.Now,
                EmergencyContact = new List<EmergencyContactsModel>() {
                    new EmergencyContactsModel()
                    {
                        Guid = Guid.NewGuid(),
                        CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        FatherName = "abel",
                        FirstName = "abel",
                        IsActive = true,
                        IsDeleted = false,
                        Relationship = "bro",
                        houseNumber = "123123",
                        GrandFatherName = "abel",
                        postalCode = "12313",
                        woreda = "12",
                        subCityZone = "Bole",
                        city = "AA",
                        Country = "Et",
                        email = "nwwqaa@gmail.com",
                        email2 = "nwqa@gmail.com",
                        email3 = "retree@gmail.com",
                        PhoneNumber = "+251987654",
                        phoneNumber2 = "+251467892",
                        phoneNumber3 = "+251987654",
                        stateRegionProvice = "AA"

                    }},
                Gender = "male",
                IsDeleted = false,
                MobilePhone = "25145126398",
                PersonalEmail = "asdasd@mail.com",
                PersonalEmail2 = "wasdasd@mail.com",
                PersonalEmail3 = "qasdasd@mail.com",
                Phone1 = "251879652",
                Phone2 = "251687541",
                Photo = "",
                EmployeeAddress = new List<PersonalAddress>() {

                            new PersonalAddress(){
                             Guid = EmpId,
                              City = "Addis",
                               Country="Ethio",
                                CreatedbyUserGuid = EmpId,
                                 CreatedDate = DateTime.Now,
                                  HouseNumber = "1452545",
                                   IsActive = true,
                                    IsDeleted = false,
                                     PhoneNumber = "25147856324",
                                      PostalCode = "12545",
                                       StateRegionProvice = "Addis",
                                        SubCityZone = "Bole",
                                         Woreda = "12"
                            }
                        },
                 FamilyDetails = new List<FamilyDetails>()
                 {
                     new FamilyDetails(){
                      Guid = EmpId,
                       CreatedbyUserGuid = EmpId,
                        CreatedDate = DateTime.Now,
                          DoB = DateTime.Now,
                           EmployeeId = EmpId,
                            RelationshipId = EmpId,
                              FullName = "abel",
                               Gender = "male",
                                IsActive = true,
                                 IsDeleted = false,
                                   Remark = "ok",
                                     Relationship = new Relationship()


                     }
                 }

                 
            };


             EmpRepository.Setup(repo => repo.AddAsync(EmployeeEntity)).ReturnsAsync((EmployeeEntity));

            var savedEmp = await _EmpService.AddNewEmployeeEntry(EmployeeEntity);
            //Assert
            Assert.Equal(EmployeeEntity, savedEmp);

        }

        [Fact]
        public async Task GetFilterMenu() 
        {
            //arrange 

            //act 

            //assert

        }

        
    }
}
