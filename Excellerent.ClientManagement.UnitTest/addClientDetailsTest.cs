using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Services;
using Excellerent.ClientManagement.Presentation.Models.PostModels;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ClientManagement.UnitTest
{
    public class addClientDetailsTest
    {
        private readonly ClientDetailsService _clientDetailService;

        private readonly Mock<IClientDetailsRepository> _clientDetailsRepositoryMock = new Mock<IClientDetailsRepository>();
        private readonly Mock<IEmployeeService> _employeeServiceMock = new Mock<IEmployeeService>();

        public addClientDetailsTest()
        {
           _clientDetailService = new ClientDetailsService(_clientDetailsRepositoryMock.Object, null,null,null,null,null);
        }

        [Fact]
        public async Task Test_AddClient_Details_Service()
        {
            var _clientDetailsServiceMock = new Mock<IClientDetailsService>();

            var clientDetail = new ClientPostModel()
            {
                ClientName = "Client Name",
                ClientStatusGuid = new Guid(),
                Description = "About Client",
                SalesPersonGuid = new Guid(),
                ClientContacts = new List<ClientContactPostModel>()
                {
                    new ClientContactPostModel()
                    {
                        ContactPersonName = "PersonName",
                        Email = "sample@host.com",
                        PhoneNumber = "+0000000000000"
                    }
                },
                CompanyContacts = new List<ComapanyContactPostModel>()
                {
                    new ComapanyContactPostModel()
                    { ContactPersonGuid = new Guid()
                    }
                },
                OperatingAddress = new List<OperatingAddressPostModel>() {
                             new OperatingAddressPostModel() {
                                                  Country="CountryName",
                                                  City="Cityname",
                                                  State="StateName",
                                                  ZipCode="100000",
                                                  Address="Address"
                                                       } },
                BillingAddress = new List<BillingAddressPostModel>() {
                    new BillingAddressPostModel() {
                                     Address="Addresss",
                                       Country="country" ,
                                       Name="bill Name",
                                       ZipCode="10000",
                                       Affliation="Affliation"
                       } }
            };
            _clientDetailsRepositoryMock.Setup(repo => repo.AddAsync(clientDetail.MappToEntity().MapToModel())).
                ReturnsAsync(clientDetail.MappToEntity().MapToModel());

            ResponseDTO ClientDetailResponse = new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "",
                Data = clientDetail.MappToEntity()
            };
            //Act
            var response = await _clientDetailService.AddNewClient(clientDetail.MappToEntity());
            //Assert
            Assert.Equal(response.ResponseStatus, ClientDetailResponse.ResponseStatus);
        }
    }
}