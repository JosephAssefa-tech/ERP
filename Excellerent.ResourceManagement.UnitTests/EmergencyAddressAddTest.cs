using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Presentation.Controllers;
using Excellerent.SharedModules.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ResourceManagement.UnitTests
{
    public class EmergencyAddressAddTest
    {

        [Fact]
        public async Task Test_EmergencyAddress_Add_Controller()
        {
            // Arrange
            var mockService = new Mock<IEmergencyAddressService>();
            EmergencyAddressEntity emergencyAddress = new EmergencyAddressEntity();

            emergencyAddress.Guid = Guid.NewGuid();
            emergencyAddress.PhoneNumber = "+251912345678";
            emergencyAddress.Country = "Ethiopia";
            emergencyAddress.StateRegionProvice = "Addis Ababa";
            emergencyAddress.City = "Addis Ababa";
            emergencyAddress.SubCityZone = "Bole";
            emergencyAddress.Woreda = "09";
            emergencyAddress.HouseNumber = "New";
            emergencyAddress.PostalCode = "1000";

            mockService.Setup(service => service.Add(emergencyAddress))
                .Returns(AsyncGet(new ResponseDTO(ResponseStatus.Success, "Successfully Added!", emergencyAddress)));

            EmergencyAddressController controller = new EmergencyAddressController(mockService.Object);

            ResponseDTO response = await controller.Add(emergencyAddress);

            Assert.Equal(ResponseStatus.Success, response.ResponseStatus);
        }

        public async Task<ResponseDTO> AsyncGet(ResponseDTO input)
        {
            return input;
        }
    }
}
