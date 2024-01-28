using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.Services;
using Excellerent.ResourceManagement.Presentation.Controllers;
using Excellerent.SharedModules.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ResourceManagement.UnitTests
{
    public class EmergencyContactServiceTest
    {
        private readonly EmergencyContactService _econtactService;
        private readonly Mock<IEmergencyContactRepository> econtactServiceRepository = new Mock<IEmergencyContactRepository>();

        public EmergencyContactServiceTest()
        {
            _econtactService = new EmergencyContactService(econtactServiceRepository.Object);
        }


        [Fact]
        public async Task GetAll()
        {
            List<EmergencyContactsModel> eList = new List<EmergencyContactsModel>()
            {
                new EmergencyContactsModel
                {
                Guid = Guid.NewGuid(),
                IsActive = true,
                IsDeleted = true,
                CreatedDate = DateTime.Now,
                CreatedbyUserGuid = Guid.NewGuid(),
                FirstName = "Simbo",
                FatherName = "Temesgen",
                Relationship = "brorther",

                }
            };
            econtactServiceRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(eList);
            var getAll = await _econtactService.GetAll();
            Assert.Equal(eList, getAll);
        }


        [Fact]
        public async Task CreateEmergencyContacts()
        {

            var mockService = new Mock<IEmergencyContactsService>();
            EmergencyContactsEntity econtact = new EmergencyContactsEntity();

            econtact.FirstName = "Simbo";
            econtact.FatherName = "Temesgen";
            econtact.GrandFatherName = "Gonfa";
            econtact.email = "simboo100@gmail.com";
            econtact.email2 = "";
            econtact.email3 = "";
            econtact.phoneNumber2 = "";
            econtact.phoneNumber3 = "";
            econtact.Relationship = "brother";
            econtact.woreda = "13";
            econtact.Guid = Guid.NewGuid();
            econtact.PhoneNumber = "+251912345678";
            econtact.Country = "Ethiopia";
            econtact.stateRegionProvice = "Addis Ababa";
            econtact.city = "Addis Ababa";
            econtact.subCityZone = "Bole";
            econtact.houseNumber = "New";
            econtact.postalCode = "1000";

            mockService.Setup(service => service.Add(econtact))
                .Returns(AsyncServiceGet(new ResponseDTO(ResponseStatus.Success, "Successfully Added!", econtact)));

            EmergencyContactsController controller = new EmergencyContactsController(mockService.Object);

            ResponseDTO response = await controller.AddEmergencyContact(econtact);

            Assert.Equal(ResponseStatus.Success, response.ResponseStatus);
        }



        public async Task<ResponseDTO> AsyncServiceGet(ResponseDTO input)
        {
            return input;
        }

       


    }



}
