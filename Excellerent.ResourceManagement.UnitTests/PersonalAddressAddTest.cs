using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.Services;
using Excellerent.ResourceManagement.Presentation.Controllers;
using Excellerent.SharedModules.DTO;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ResourceManagement.UnitTests
{
    
    public class PersonalAddressAddTest
    {

        [Fact]
        public async Task Test_Add_PersonalAddress_Controller()
        {
            // Arrange
            var mockService = new Mock<IPersonalAddressService>();
            PersonalAddressEntity personalAddress = new PersonalAddressEntity();

            personalAddress.Guid = Guid.NewGuid();
            personalAddress.PhoneNumber = "+251912345678";
            personalAddress.Country = "Ethiopia";
            personalAddress.StateRegionProvice = "Addis Ababa";
            personalAddress.City = "Addis Ababa";
            personalAddress.SubCityZone = "Bole";
            personalAddress.Woreda = "09";
            personalAddress.HouseNumber = "New";
            personalAddress.PostalCode = "1000";

            mockService.Setup(service => service.Add(personalAddress))
                .Returns(AsyncServiceGet(new ResponseDTO(ResponseStatus.Success, "Successfully Added!", personalAddress)));

            PersonalAddressController controller = new PersonalAddressController(mockService.Object);

            ResponseDTO response = await controller.Add(personalAddress);

            Assert.Equal(ResponseStatus.Success, response.ResponseStatus);
        }

        //[Fact]
        //public async Task Test_Add_PersonalAddress_Service()
        //{
        //    // Arrange
        //    var mockService = new Mock<IAsyncPersonalAddressRepository>();
        //    PersonalAddressEntity personalAddress = new PersonalAddressEntity();

        //    personalAddress.Guid = Guid.NewGuid();
        //    personalAddress.PhoneNumber = "+251912345678";
        //    personalAddress.Country = "Ethiopia";
        //    personalAddress.StateRegionProvice = "Addis Ababa";
        //    personalAddress.City = "Addis Ababa";
        //    personalAddress.SubCityZone = "Bole";
        //    personalAddress.Woreda = "09";
        //    personalAddress.HouseNumber = "New";
        //    personalAddress.PostalCode = 1000;

        //    mockService.Setup(service => service.AddAsync(personalAddress.MapToModel()))
        //        .Returns(AsyncRepoGet(personalAddress.MapToModel()));
        //    mockService.Setup(service => service.GetResponseDTO(ResponseStatus.Success, personalAddress, ""))
        //        .Returns(new ResponseDTO(ResponseStatus.Success, "Successfully Added!", (object)personalAddress.MapToModel()));

        //    PersonalAddressService service = new PersonalAddressService(mockService.Object);

        //    ResponseDTO response = await service.Add(personalAddress);

        //    Assert.Equal(ResponseStatus.Success, response.ResponseStatus);
        //}

        public async Task<ResponseDTO> AsyncServiceGet(ResponseDTO input)
        {
            return input;
        }

        public async Task<PersonalAddress> AsyncRepoGet(PersonalAddress input)
        {
            return input;
        }


    }
}
