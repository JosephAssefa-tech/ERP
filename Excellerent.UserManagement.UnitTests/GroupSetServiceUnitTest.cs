using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using Excellerent.Usermanagement.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.UserManagement.UnitTests
{
    public class GroupSetServiceUnitTest
    {
        private readonly IGroupSetService _groupSetService;
        private readonly Mock<IGroupSetRepository> _gSetServiceMock = new Mock<IGroupSetRepository>();
        private readonly Mock<IGroupSetService> _educationServiceRepo = new Mock<IGroupSetService>();
        public GroupSetServiceUnitTest()
        {
            _groupSetService = new GroupSetService(_gSetServiceMock.Object);
        }

        [Fact]
        public async Task CreateNewGroupSetServiceTest_ReturnsData_WhenValidInputProvided()
        {
            GroupSet gSet = new GroupSet
            {
                Guid = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da222"),
                Name = "Project Managers",
                Description = "a group for PMs to access permissions"
            };

            _gSetServiceMock.Setup(repo => repo.AddAsync(It.IsAny<GroupSet>()).Result)
                   .Returns(gSet);
            var returneData = _groupSetService.Add(new GroupSetEntity(gSet));
            Assert.Equal(gSet.Guid, returneData.Result.Data);
        }
    }
}