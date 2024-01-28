using AutoMapper;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Excellerent.ApplicantTracking.Presentation.Controllers;
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using Excellerent.ApplicantTracking.Presentation.Models.PutModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class Education_UnitTest
    {
        private readonly IEducationService  _educationService;
        private readonly EducationController _educationController;
        private readonly Mock<IEducationRepository> _eduMock = new Mock<IEducationRepository>();
        private readonly Mock<IEducationService> _educationServiceRepo = new Mock<IEducationService>();
        private readonly Mock<ILUFieldOfStudyService> _fieldStuduService = new Mock<ILUFieldOfStudyService>();
        private readonly Mock<IEducationProgrammeService> _eduProgrammeService = new Mock<IEducationProgrammeService>();
        private readonly Mock<IMapper> _imapper = new Mock<IMapper>();
        EducationEntity Education = new EducationEntity()
        {
            Guid = new Guid("056e19bd-8659-44c3-91bf-979c98c578e6"),
            ApplicantId = new Guid("3f2fd146-f6a4-419e-9e30-2db4c1ee0cad"),
            Institution = "AAU",
            Country = "Ethiopia",
            DateFrom = DateTime.Now.AddDays(-1000),
            DateTo = DateTime.Now.AddDays(20)
        };
        public Education_UnitTest()
        {
            // _eduMock.Setup(repo => repo.).Returns(Multiple());
            _educationService = new EducationService(_eduMock.Object);
            _educationController = new EducationController(_educationService, _fieldStuduService.Object, _eduProgrammeService.Object, _imapper.Object);

        }

        [Fact]
        public void Test_Entry_POST_InvalidModelState()
        {
            // Arrange
            var controller = new EducationController(_educationService, null, null, null);

            // Act
            var result = controller.Delete(Guid.NewGuid());

            // Assert
            // var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            // Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task CreateNewEducationServiceTest_ReturnsData_WhenValidInputProvided()
        {
            Education edu = new Education
            {
                Guid = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da743"),
                ApplicantId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da744"),
                EducationProgrammeId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da745"),
                Institution = "Mekelle University",
                FieldOfStudyId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da746"),
                DateFrom = DateTime.Now.AddDays(-3000),
                DateTo = DateTime.Now.AddDays(1000),
                IsCompleted = false,
                OtherFieldOfStudy = "BS,MS",
                Country = "Ethiopia"
            };

            _eduMock.Setup(repo => repo.AddAsync(It.IsAny<Education>()).Result)
                   .Returns(edu);
            var returneData = _educationService.Add(new EducationEntity(edu));
            Assert.Equal(edu.Guid, returneData.Result.Data);
        }

        //[Fact]
        //public async Task CreateNewEducationControllerTest__ReturnsData_WhenValidInputProvided()
        //{
        //    Guid newT = Guid.NewGuid();
        //    EducationPostModelDto eduDto = new EducationPostModelDto
        //    {
        //        ApplicantId = newT,
        //        EducationProgrammeId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da745"),
        //        Institution = "Mekelle University",
        //        FieldOfStudyId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da746"),
        //        DateFrom = DateTime.Now.AddDays(-3000),
        //        DateTo = DateTime.Now.AddDays(1000),
        //        IsCompleted = false,
        //        OtherFieldOfStudy = "BS,MS",
        //        Country = "Ethiopia"
        //    };

        //    var responseDTO = new ResponseDTO(ResponseStatus.Success, "Successfully added", newT);
        //    _educationServiceRepo.Setup(repo => repo.Add(It.IsAny<EducationEntity>()).Result).Returns(responseDTO);

        //    var resultt = _educationController.Post(eduDto);

        //    Assert.IsType<ActionResult<ResponseDTO>>(resultt.Result);

        //}

        [Fact]
        public async Task GetWithPredicateTest_ReturnsData_WhenValidInputProvided()
        {
            Guid applicantId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da724");
            var retEducation = new List<Education>();
            retEducation.Add(new Education
            {
                Guid = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da723"),
                ApplicantId = applicantId,
                EducationProgrammeId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da725"),
                Institution = "Mekelle University",
                FieldOfStudyId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da726"),
                DateFrom = DateTime.Now.AddDays(-3000),
                DateTo = DateTime.Now.AddDays(1000),
                IsCompleted = false,
                OtherFieldOfStudy = "BS,MS",
                Country = "Ethiopia"
            }
            );
            retEducation.Add(new Education()
            {
                Guid = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da733"),
                ApplicantId = applicantId,
                EducationProgrammeId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da735"),
                Institution = "Mekelle University",
                FieldOfStudyId = new Guid("2bd60bc3-f51c-413b-bd6d-ae62c87da736"),
                DateFrom = DateTime.Now.AddDays(-3000),
                DateTo = DateTime.Now.AddDays(1000),
                IsCompleted = false,
                OtherFieldOfStudy = "BS,MS",
                Country = "Ethiopia"
            });
            _eduMock.Setup(x => x.GetWithPredicate(x => x.Guid == applicantId).Result)
                    .Returns(retEducation?.Select(a => new EducationEntity(a)));

            var applicant = await (_educationService.GetWithPredicateAsync(null, applicantId));
           // Assert.IsType<EducationEntity[]>(applicant);
        }

       }
}
