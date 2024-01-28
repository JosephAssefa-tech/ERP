using AutoMapper;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using Excellerent.UserManagement.Presentation.Controllers;
using LinqKit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.UserManagement.UnitTests
{
    public class UserServiceTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> userService = new Mock<IUserService>();
        private readonly IMapper mapper;
        public UserServiceTest()
        {
            //_userController = new UserController(userService, mapper);
        }

        //[Fact]
//        public async Task GetUsersForDashboard_Without_Searching_Criteria()
//        {
//            Guid EmpId = Guid.NewGuid();
//            var user =  new User()
//            {
//                Guid = Guid.NewGuid(),
//                UserName = "bassefa@excellerentsoulution.com",
//                EmployeeId = EmpId,
//                FirstName = "Biruk",
//                MiddleName = "Assefa",
//                LastName = "Wolde",
//                Password = string.Empty,
//                Status = Usermanagement.Domain.Enums.UserStatus.Active,
//                Tel = "+251921739313",
//                Email = "bassefa@excellerentsoulution.com",
//                LastActivityDate = DateTime.Now,
//                CreatedDate = DateTime.Today,
//                CreatedbyUserGuid = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                Employee = new Employee()
//                {
//                    Guid = EmpId,
//                    FirstName = "abel",
//                    FatherName = "abel",
//                    GrandFatherName = "abel",
//                    DateofBirth = DateTime.Now,
//                    Nationality = null,
//                    IsActive = true,
//                    EmployeeOrganization = new EmployeeOrganization()
//                    {
//                        Guid = Guid.NewGuid(),
//                        Country = Guid.NewGuid().ToString(),
//                        IsActive = true,
//                        DutyBranch = Guid.NewGuid().ToString(),
//                        Department = "HR",
//                        CompaynEmail = "d@mail.com",
//                        CreatedbyUserGuid = Guid.NewGuid(),
//                        EmploymentType = "FullTime Permanent",
//                        JobTitle = "manager",
//                        IsDeleted = false,
//                        CreatedDate = DateTime.Now,
//                        EmployeeId = Guid.NewGuid(),
//                        JoiningDate = DateTime.Now,
//                        PhoneNumber = "251784596",
//                        ReportingManager = "",
//                        Status = "Active",
//                        TerminationDate = DateTime.Now
//                    },
//                    CreatedbyUserGuid = Guid.NewGuid(),
//                    CreatedDate = DateTime.Now,
//                    EmergencyContact = null,
//                    Gender = "male",
//                    IsDeleted = false,
//                    MobilePhone = "25145126398",
//                    PersonalEmail = "asdasd@mail.com",
//                    PersonalEmail2 = "wasdasd@mail.com",
//                    PersonalEmail3 = "qasdasd@mail.com",
//                    Phone1 = "251879652",
//                    Phone2 = "251687541",
//                    Photo = "",
//                    EmployeeAddress = null,
//                    FamilyDetails = null
//                }
//            };
     
//            List<UserListView> userViewList = new List<UserListView>()
//            {
//                new UserListView()
//                {
//                    UserId = user.Guid,
//                    FullName = user.FirstName + ' ' + user.LastName,
//                    JobTitle = user.Employee.EmployeeOrganization.JobTitle,
//                    Department = user.Employee.EmployeeOrganization.Department,
//                    Status = Usermanagement.Domain.Enums.UserStatus.Active
//                }
//            };
            
//            string searchKey = "";
//            var predicate = PredicateBuilder.True<User>();
//            if (searchKey != "")
//            {
//                predicate = predicate.And(p => p.UserName.Contains(searchKey));
//            }
//            else
//            {
//                predicate = null;
//            }
//            PredicatedResponseDTO searchResult = new PredicatedResponseDTO()
//            {
//                Data = user,
//                PageIndex = 0,
//                PageSize = 5,
//                TotalPage = 1,
//                TotalRecord = 1
//            };
//            userService.Setup(x => x.GetUserDashBoardList(searchKey, 1, 5)).ReturnsAsync(searchResult);            
//            //Act
//            var retrivedUserList = await _userController.GetAllUsersDashboard(searchKey, 1, 5);
//            //Assert
//            Assert.Equal(searchResult, retrivedUserList);
//        }

//        [Fact]
//        public async Task GetUsersForDashboard_With_Searching_Criteria()
//        {
//            Guid EmpId = Guid.NewGuid();
//            var user = new User()
//            {
//                Guid = Guid.NewGuid(),
//                UserName = "bassefa@excellerentsoulution.com",
//                EmployeeId = EmpId,
//                FirstName = "Biruk",
//                MiddleName = "Assefa",
//                LastName = "Wolde",
//                Password = string.Empty,
//                Status = Usermanagement.Domain.Enums.UserStatus.Active,
//                Tel = "+251921739313",
//                Email = "bassefa@excellerentsoulution.com",
//                LastActivityDate = DateTime.Now,
//                CreatedDate = DateTime.Today,
//                CreatedbyUserGuid = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                Employee = new Employee()
//                {
//                    Guid = EmpId,
//                    FirstName = "abel",
//                    FatherName = "abel",
//                    GrandFatherName = "abel",
//                    DateofBirth = DateTime.Now,
//                    Nationality = null,
//                    IsActive = true,
//                    EmployeeOrganization = new EmployeeOrganization()
//                    {
//                        Guid = Guid.NewGuid(),
//                        Country = Guid.NewGuid().ToString(),
//                        IsActive = true,
//                        DutyBranch = Guid.NewGuid().ToString(),
//                        Department = "HR",
//                        CompaynEmail = "d@mail.com",
//                        CreatedbyUserGuid = Guid.NewGuid(),
//                        EmploymentType = "FullTime Permanent",
//                        JobTitle = "manager",
//                        IsDeleted = false,
//                        CreatedDate = DateTime.Now,
//                        EmployeeId = Guid.NewGuid(),
//                        JoiningDate = DateTime.Now,
//                        PhoneNumber = "251784596",
//                        ReportingManager = "",
//                        Status = "Active",
//                        TerminationDate = DateTime.Now
//                    },
//                    CreatedbyUserGuid = Guid.NewGuid(),
//                    CreatedDate = DateTime.Now,
//                    EmergencyContact = null,
//                    Gender = "male",
//                    IsDeleted = false,
//                    MobilePhone = "25145126398",
//                    PersonalEmail = "asdasd@mail.com",
//                    PersonalEmail2 = "wasdasd@mail.com",
//                    PersonalEmail3 = "qasdasd@mail.com",
//                    Phone1 = "251879652",
//                    Phone2 = "251687541",
//                    Photo = "",
//                    EmployeeAddress = null,
//                    FamilyDetails = null
//                }
//            };

//            List<UserListView> userViewList = new List<UserListView>()
//            {
//                new UserListView()
//                {
//                    UserId = user.Guid,
//                    FullName = user.FirstName + ' ' + user.LastName,
//                    JobTitle = user.Employee.EmployeeOrganization.JobTitle,
//                    Department = user.Employee.EmployeeOrganization.Department,
//                    Status = Usermanagement.Domain.Enums.UserStatus.Active
//                }
//            };

//            string searchKey = "bassefa@excellerentsoulution.com";
//            var predicate = PredicateBuilder.True<User>();
//            if (searchKey != "")
//            {
//                predicate = predicate.And(p => p.UserName.Contains(searchKey));
//            }
//            else
//            {
//                predicate = null;
//            }
//            PredicatedResponseDTO searchResult = new PredicatedResponseDTO()
//            {
//                Data = user,
//                PageIndex = 0,
//                PageSize = 5,
//                TotalPage = 1,
//                TotalRecord = 1
//            };
//            userService.Setup(x => x.GetUserDashBoardList(searchKey, 1, 5)).ReturnsAsync(searchResult);
//            //Act
//            var retrivedUserList = await _userController.GetAllUsersDashboard(searchKey, 1, 5);
//            //Assert
//            Assert.Equal(searchResult, retrivedUserList);
//=======
//        private  UserService _userService;
//        private  Mock<IUserRepository> _userRepo = new Mock<IUserRepository>();

//        public UserServiceTest()
//        {
//        }
//        [Fact]
//        public async void GetUser_ExistingUserIdPassed_ReturnsSingleUser()
//        {
//            //Arrang
//            var id = new Guid();
//            var User = new User()
//            {
//                Guid = id
//            };
//            _userRepo.Setup(x => x.FindOneAsync(x => x.Guid == id)).ReturnsAsync(User);
//            _userService = new UserService(_userRepo.Object);

//            //act
//            var user = await _userService.GetUser(id);
//            //assert
//            Assert.IsType<UserEntity>(user);
//            Assert.Equal(user.Guid, id);
//        }
//        [Fact]
//        public async void GetUsers_ReturnsAllUsers()
//        {
//            //Arrang
//            IEnumerable<User> Users = new List<User>()
//            {
//                new User(), new User()
//            };
            
//            _userRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Users);
//            _userService = new UserService(_userRepo.Object);

//            //act
//            var result = await _userService.GetUsers();
//            //assert
//           var users =  Assert.IsAssignableFrom<IEnumerable<UserEntity>>(result);
//            Assert.Equal(2, users.Count());
//        }
//        [Fact]
//        public async void GetUser_NONExistingUserIdPassed_ShouldReturnNull()
//        {
//            //Arrang
//            var Id = new Guid();
//            var NonExisitingUserId = Guid.NewGuid();
//            var User = new User()
//            {
//                Guid = Id
//            };
//            _userRepo.Setup(x => x.FindOneAsync(x => x.Guid == NonExisitingUserId)).ReturnsAsync(User);
//            _userService = new UserService(_userRepo.Object);

//            //act
//            var user = await _userService.GetUser(Id);
//            //assert
//            Assert.Null(user);
//        }
//        [Fact]
//        public async void UpdateUser_ExistedUserPassed_UpdatesUserMembers()
//        {
//            //Arrang
//            var ExisitingUserId = new Guid("b85edede-64a8-42e5-bb2f-bdd436426525");
//            var UserEnity = new UserEntity()
//            {
//                Guid = ExisitingUserId,
//                Email = "testemail@gmail.com",
//                Status = Usermanagement.Domain.Enums.UserStatus.Active
//            };
//            var User = new User()
//            {
//                Guid = ExisitingUserId,
//                Email = "testemail@gmail.com",
//                Status = Usermanagement.Domain.Enums.UserStatus.Active
//            };

//            _userRepo.Setup(x => x.FindOneAsync(x => x.Guid == ExisitingUserId)).ReturnsAsync(User);
//            _userRepo.Setup(x => x.UpdateAsync(User));
//            _userService = new UserService(_userRepo.Object);
            
//            //act
//            var respone =  await _userService.Update(UserEnity);
//            //assert
//           var result = Assert.IsType<ResponseDTO>(respone);
//        }

//        [Fact]
//        public async void UpdateUser_NONExistedUserPassed_ShouldReturn()
//        {
//            //Arrang
//            var ExisitingUserId = new Guid("b85edede-64a8-42e5-bb2f-bdd436426525");
//            var NotExistingUserId = new Guid();
//            var UserEnity = new UserEntity()
//            {
//                Guid = ExisitingUserId,
//                Email = "testemail@gmail.com",
//                Status = Usermanagement.Domain.Enums.UserStatus.Active
//            };
//            var User = new User()
//            {
//                Guid = ExisitingUserId,
//                Email = "testemail@gmail.com",
//                Status = Usermanagement.Domain.Enums.UserStatus.Active
//            };

//            _userRepo.Setup(x => x.FindOneAsync(x => x.Guid == NotExistingUserId)).ReturnsAsync(User);
//            _userRepo.Setup(x => x.UpdateAsync(User));
//            _userService = new UserService(_userRepo.Object);

//            //act
//            var respone = await _userService.Update(UserEnity);
//            //assert
//            var result = Assert.IsType<ResponseDTO>(respone);
//            Assert.Equal(null, result.Data);
//>>>>>>> user-management
//        }s
    }
}
