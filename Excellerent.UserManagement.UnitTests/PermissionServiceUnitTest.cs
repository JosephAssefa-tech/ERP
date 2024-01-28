
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using Excellerent.Usermanagement.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.UserManagement.UnitTests
{
    public class PermissionServiceUnitTest
    {
        private readonly IPermissionService _permissionService;
        private readonly Mock<IPermissionRepository> _permissionServiceMock = new Mock<IPermissionRepository>();
        private readonly Mock<IPermissionService> _permissionServiceRepo = new Mock<IPermissionService>();
        private readonly Mock<IGroupSetPermissionService> _groupPermissionServiceMock = new Mock<IGroupSetPermissionService>();
        private readonly Mock<IUserGroupsService> _userGroupsServiceMock = new Mock<IUserGroupsService>();
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();
        private readonly Mock<IGroupSetPermissionService> _groupSetPermissionMock = new Mock<IGroupSetPermissionService>();
      
       
        public PermissionServiceUnitTest()
        {
            _permissionService = new PermissionService(_groupPermissionServiceMock.Object,_permissionServiceMock.Object
                 , _userGroupsServiceMock.Object, _userServiceMock.Object, _groupSetPermissionMock.Object);
        }
       

       [Fact]
        public async Task CreateNewPermissionServiceTest_ReturnsData_WhenValidInputProvided()
        {
            Permission perm = new Permission
            {
                Guid = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da222"),
                Name = "User_Create",
                KeyValue = "User Creation",
                PermissionCode = "0011",
                ParentCode = "001",
                Level = "0"
            };

            _permissionServiceMock.Setup(repo => repo.AddAsync(It.IsAny<Permission>()).Result)
                   .Returns(perm);
            var returneData = _permissionService.Add(new PermissionEntity(perm));
            Assert.Equal(perm.Guid, returneData.Result.Data);
        }

        [Fact]
        public async Task GetAllPermissionServiceTest_ReturnsData_WhenValidInputProvided()
        {
            var perm = new List<Permission>()
            {
                new Permission(){
                Guid = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da222"),
                Name = "User_Create",
                KeyValue = "User Creation",
                PermissionCode = "0011",
                ParentCode = "001",        
                Level = "0"
            } 
            };

            _permissionServiceMock.Setup(repo => repo.GetAllAsync().Result)
                   .Returns(perm);
            var returneData = await _permissionService.GetAllPermissions();
            Assert.NotNull(returneData);
        }
    }
}
