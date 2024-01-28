using AutoMapper;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Presentation.Controllers;
using Excellerent.ApplicantTracking.Presentation.Models.PutModels;
using Excellerent.ApplicantTracking.Presentation.Models.ViewModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class EducationControlUnitTest
    {
        Mock<IEducationService> mockService;
        Mock<IMapper> mockMapper;
        Guid Id;
        public EducationControlUnitTest()
        {
            mockService = new Mock<IEducationService>();
            mockMapper = new Mock<IMapper>();
            Id = new Guid("3f2fd146-f6a4-419e-9e30-2db4c1ee0cad");
            
        }

        [Fact]
        public async void Get_ApplicantIdPassed_ReturnsSingleEducation()
        {
            // Arrange
            Guid applicantId = new Guid("3f2fd146-f6a4-419e-9e30-2db4c1ee0cad");
            
             var x = mockService
                .Setup(repo => repo.GetWithPredicateAsync(null, applicantId))
                .Returns(Single());
            EducationController controller = new EducationController(mockService.Object, null, null,mockMapper.Object);

            // Act
            var result = await controller.Get(null, applicantId);

            // Assert`
            var model =  Assert.IsAssignableFrom<ResponseDTO>(result);
            var data =  Assert.IsAssignableFrom<IEnumerable<EducationGetModelDto>>(model.Data);
            Assert.Single(data);
        }
        [Fact]
        public void Get_ApplicantIdPassed_ReturnsMultipleEducation()
        {
            // Arrange
            Guid applicantId = new Guid("3f2fd146-f6a4-419e-9e30-2db4c1ee0cad");
            var x = mockService.Setup(repo => repo.GetWithPredicateAsync(null, applicantId)).Returns(Multiple());
            EducationController controller = new EducationController(mockService.Object, null, null,mockMapper.Object);

            // Act
            var result = controller.Get(null, applicantId);

            // Assert
            var model = Assert.IsAssignableFrom<Task<ResponseDTO>>(result);
            var data = model.Result.Data as IEnumerable<EducationGetModelDto>;
            int count = data.Count();
            Assert.Equal(3, data.Count());
        }
       [Fact]
        public async void Delete_ValidDataPassed_ReturnsOK()
        {
            // Arrange
            EducationEntity education = new EducationEntity() { Guid = this.Id };
            await mockService.Object.Add(education);
            
            mockService
               .Setup(repo => repo.FindOneAsyncForDelete(education.Guid))
               .Returns(Task.FromResult(education));
            mockService.Setup(repo => repo.Delete(It.IsAny<EducationEntity>())).Verifiable();
            EducationController controller = new EducationController(mockService.Object, null, null, mockMapper.Object);

            // Act
            var result = await controller.Delete(education.Guid);

            // Assert
            mockService.Verify();
        }
        [Fact]
        public async void Delete_InvalidaDataPassed_ReturnsBadRequestObject()
        {
            // Arrange
            EducationEntity education = new EducationEntity() { Guid = this.Id };

            await mockService.Object.Add(education);
            
            mockService.Setup(repo => repo.Delete(It.IsAny<EducationEntity>())).Verifiable();
            EducationController controller = new EducationController(mockService.Object, null, null, mockMapper.Object);

            // Act
            var result = await controller.Delete(education.Guid);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async void Put_ValidDataPassed_UpdatesValidEducation()
        {
            // Arrange
            EducationPutModel education = new EducationPutModel()
            {
                Guid = this.Id,
                EducationProgrammeId = Guid.NewGuid(),
                FieldOfStudyId = Guid.NewGuid(),
                DateFrom = DateTime.Now.AddDays(-300),
                DateTo = DateTime.Now.AddDays(-10),
                Institution = "Some Instution"
            };
            EducationEntity educationEntity = mockMapper.Object.Map<EducationEntity>(education);
            mockService.Setup(repo => repo.Update(educationEntity));
            EducationController controller = new EducationController(mockService.Object, null, null, mockMapper.Object);

            // Act
            var result = await controller.Put(education);

            // Assert`
            var model = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public async void Put_InvalidDataPassed_ReturnsBadRequest()
        {
            // Arrange 
            // Ivalida data
            EducationPutModel education = new EducationPutModel()
            {
                Guid = this.Id,
            };
            EducationEntity educationEntity = mockMapper.Object.Map<EducationEntity>(education);
            mockService.Setup(repo => repo.Update(educationEntity));
            EducationController controller = new EducationController(mockService.Object, null, null, mockMapper.Object);

            // Act
            var result = await controller.Put(education);

            // Assert`
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        public async Task<IEnumerable<EducationEntity>> Single()
        {
            var educations = new List<EducationEntity>()
            {
               new EducationEntity()
                {
                    Guid = new Guid("3f2fd146-f6a4-419e-9e30-2db4c1ee0cad"),
                    ApplicantId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                }
            };
            return await Task.FromResult(educations);
        }
        public async Task<IEnumerable<EducationEntity>> Multiple()
        {
            var educations = new List<EducationEntity>()
            {
               new EducationEntity()
                {
                    Guid = new Guid("3f2fd146-f6a4-419e-9e30-2db4c1ee0cad"),
                    ApplicantId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                },
               new EducationEntity()
                {
                    Guid = new Guid("765a5198-e7ab-4d6f-b538-140be71ede0c"),
                    ApplicantId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                },
                new EducationEntity()
                {
                    Guid = new Guid("c042d81f-8feb-4144-b179-0c38f4fecf01"),
                    ApplicantId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                }
            };
            return await Task.FromResult(educations);
        }
    }
}
