using Excellerent.ResourceManagement.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using System.Collections.Generic;
using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.SharedModules.DTO;

namespace Excellerent.ResourceManagement.UnitTests
{
    public class CountryServiceTest
    {
        private readonly CountryService _countryService;
        private readonly Mock<ICountryRepository> countryRepository = new Mock<ICountryRepository>();
        public CountryServiceTest()
        {
            _countryService = new CountryService(countryRepository.Object);
        }

       
        [Fact]
        public async Task GetCountryById()
        {
            //Arrange
            Guid customerId = Guid.NewGuid();
            Country country = new Country
            {
                Guid = customerId,
                Name = "Ethiopia",
                Nationality = "Ethiopian",
                CreatedDate = DateTime.Now
            };
            countryRepository.Setup(x => x.GetCountryById(country.Guid)).ReturnsAsync(country);
            //Act
            var retrivedCountry = await _countryService.GetCountryById(customerId);
            //Assert
            Assert.Equal(country, retrivedCountry);
        }

        [Fact]
        public async Task CheckCountryExistance()
        {
            //Arrange
            var countryName = "Ethiopia";
            Country country = new Country()
            {
                Guid = Guid.NewGuid(),
                Name = countryName,
                Nationality = "Ethiopian",
                CreatedDate = DateTime.Now
            };

            countryRepository.Setup(x => x.CheckCountryExistence(countryName)).ReturnsAsync(true);
            //Act
            var doesCountryExist = await _countryService.CheckCountryExistance(countryName);
            //Assert
            Assert.True(doesCountryExist);
        }
    }
}
