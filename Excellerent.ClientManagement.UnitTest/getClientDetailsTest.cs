using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ClientManagement.Domain.Services;
using Excellerent.ClientManagement.Presentation.Models.PostModels;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ClientManagement.UnitTest
{
    public class getClientDetailsTest
    {
        private readonly ClientDetailsService _clientDetailService;
        private readonly Mock<IClientDetailsRepository> _clientDetailsRepositoryMock = new Mock<IClientDetailsRepository>();
        private readonly Mock<IEmployeeService> _employeeServiceMock = new Mock<IEmployeeService>();
        public getClientDetailsTest()
        {
            _clientDetailService = new ClientDetailsService(_clientDetailsRepositoryMock.Object, null,null,null,null,null);
        }
        [Fact]
        public async Task Test_GetClientFullData_Service()
        {

            var _clientDetailsServiceMock = new Mock<IClientDetailsService>();

            var clientDetail = new ClientDetails()
            {
                ClientName = "Client Name",
                ClientStatusGuid = new Guid(),
                Description = "About Client",
                SalesPersonGuid = new Guid(),
                ClientContacts = new List<ClientContact>()
                {
                    new ClientContact()
                    {
                        ContactPersonName = "PersonName",
                        Email = "sample@host.com",
                        PhoneNumber = "+0000000000000"
                    }
                },
                CompanyContacts = new List<CompanyContact>()
                {
                    new CompanyContact()
                    {
                        EmployeeGuid = new Guid()
                    }

                },
                OperatingAddress = new List<OperatingAddress>() {
                             new OperatingAddress() {
                                                  Country="CountryName",
                                                  City="Cityname",
                                                  State="StateName",
                                                  ZipCode="100000",
                                                  Address="Address"
                                                       } },
                BillingAddress = new List<BillingAddress>() {
                    new BillingAddress() {
                                     Address="Addresss",
                                       Country="country" ,
                                       Name="bill Name",
                                       ZipCode="10000",
                                       Affliation="Affliation"
                       } }
            };
            _clientDetailsRepositoryMock.Setup(repo => repo.GetClientFullData());

            ResponseDTO ClientDetailResponse = new ResponseDTO
            {
                Data = clientDetail,
                Message = "Client full data",
                Ex = null,
                ResponseStatus = ResponseStatus.Success
            };
            //Act
            var response = await _clientDetailService.GetClientFullData();
            //Assert
            Assert.Equal(response.ResponseStatus, ClientDetailResponse.ResponseStatus);


        }
    }
}
