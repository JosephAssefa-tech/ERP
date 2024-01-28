using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Presentation.Controllers;
using Excellerent.ResourceManagement.Presentation.Dtos;
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
    public class DeviceDetailTest
    {
      /*  [Fact]
        public async Task Test_Add_DeviceDetail()
        {
            var mockService = new Mock<IDeviceDetailsService>();
            DeviceDetailDto deviceDetailsDto = new DeviceDetailDto()
            {
                CategoryGuid= Guid.NewGuid(),
                SubCategoryGuid = Guid.NewGuid(),
                CompanyDeviceCode = "L-001",
                DeviceName = "Laptop",
                BusinessUnit = "Department",
                IsWorking = "No",
                DeviceClassificationGuid = Guid.NewGuid(),
                PurchaseDate = new DateTime(),
                InvoiceNumber = "INV-001",
                Manufacturer = "HP",
                SerialNumber = "QW0123412291",
                Warranty = "1",
                WarrantyEndDate = new DateTime(),
                Notes = "test note"
            };

            mockService.Setup(service => service.Add(deviceDetailsDto.MapToEntity()))
                .Returns(AsyncGet(new ResponseDTO(ResponseStatus.Success, "Successfully Added", deviceDetailsDto.MapToEntity())));

            DeviceDetailsController controller = new DeviceDetailsController(mockService.Object);

            ResponseDTO response = await controller.Add(deviceDetailsDto);

            Assert.Equal(ResponseStatus.Success, response.ResponseStatus);
        }
      */

        public async Task<ResponseDTO> AsyncGet(ResponseDTO input)
        {
            return input;
        }
    }

}
