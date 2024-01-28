using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ResourceManagement.UnitTests
{
    public class DutyBranchServiceTest
    {
        private readonly DutyBranchService _dutyBranchService;
        private readonly Mock<IDutyBranchRepository> dutyRepository = new Mock<IDutyBranchRepository>();

        public DutyBranchServiceTest()
        {
            _dutyBranchService = new DutyBranchService(dutyRepository.Object); 
        }

        [Fact]
        public async Task GetDutyBranchByCountryId()
        {
            Guid countryId = Guid.NewGuid();
            var countryEntity = new Country
            {
                Guid = countryId,
                Name = "Ethiopia",
                Nationality = "Ethiopian",
                CreatedDate = DateTime.Now
            };

            var dutyBranch = new List<DutyBranch>()
            {
                new DutyBranch() {
                Guid = Guid.NewGuid(),
                CountryId = countryEntity.Guid,
                Name = "Addis Abeba Head Quarter",
                CreatedDate = DateTime.Now
                }
            };
            dutyRepository.Setup(x => x.GetDutyBranchByCountry(countryId)).ReturnsAsync(dutyBranch);
            //Act
            var retrivedDutyBranch = await _dutyBranchService.GetDutyBranchByCountry(countryId);
            //Assert
            Assert.Equal(dutyBranch, retrivedDutyBranch);

        }

        [Fact]
        public async Task GetAllDutyBranches()
        {
            Guid countryId = Guid.NewGuid();
            var countryEntity = new Country
            {
                Guid = countryId,
                Name = "Ethiopia",
                Nationality = "Ethiopian",
                CreatedDate = DateTime.Now
            };

            var dutyBranchList = new List<DutyBranch>()
            {
                new DutyBranch
                {
                    Guid = Guid.NewGuid(),
                    CountryId = countryEntity.Guid,
                    Name = "Addis Abeba Head Quarter",
                    CreatedDate = DateTime.Now
                },

                new DutyBranch
                {
                    Guid = Guid.NewGuid(),
                    CountryId = countryEntity.Guid,
                    Name = "Wollo Sefer Branch",
                    CreatedDate = DateTime.Now
                }
            };
            dutyRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(dutyBranchList);
            //Act
            var retrivedDutyBranches = await _dutyBranchService.GetAll();
            //Assert
            Assert.Equal(dutyBranchList, retrivedDutyBranches);

        }

        [Fact]
        public async Task CheckDutyStationExistence()
        {
            Guid countryId = Guid.NewGuid();
            var countryEntity = new Country
            {
                Guid = countryId,
                Name = "Ethiopia",
                Nationality = "Ethiopian",
                CreatedDate = DateTime.Now
            };
            var dutyBranchName = "Addis Abeba Head Quarter";
            var dutyBranch = new DutyBranch
            {
                Guid = Guid.NewGuid(),
                CountryId = countryEntity.Guid,
                Name = dutyBranchName,
                CreatedDate = DateTime.Now
            };
            dutyRepository.Setup(x => x.CheckDutyBranchExistance(countryId, dutyBranchName)).ReturnsAsync(true);
            //Act
            var retrivedDutyBranch = await _dutyBranchService.CheckDutyBranchExistance(countryId, dutyBranchName);
            //Assert
            Assert.True(retrivedDutyBranch);
        }

        [Fact]
        public async Task GetDutyBranchByCountryName()
        {
            Guid countryId = Guid.NewGuid();
            var countryEntity = new Country
            {
                Guid = countryId,
                Name = "Ethiopia",
                Nationality = "Ethiopian",
                CreatedDate = DateTime.Now
            };

            var dutyBranch = new List<DutyBranch>()
            {
                new DutyBranch() {
                Guid = Guid.NewGuid(),
                CountryId = countryEntity.Guid,
                Name = "Addis Abeba Head Quarter",
                CreatedDate = DateTime.Now
                }
            };
            dutyRepository.Setup(x => x.GetDutyBranchByCountryName("Ethiopia")).ReturnsAsync(dutyBranch);
            //Act
            var retrivedDutyBranch = await _dutyBranchService.GetDutyBranchByCountryName("Ethiopia");
            //Assert
            Assert.Equal(dutyBranch, retrivedDutyBranch);
        }
    }
}
