
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ProjectManagement.Presentation.Controllers;
using Excellerent.SharedModules.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Excellerent.ProjectManagement.UnitTests
{
    public class ProjectStatusUnitTest
    {
       
        private static IEnumerable<ProjectStatus> StaticData()
        {
            var projectStatus = new List<ProjectStatus>();
            projectStatus.Add(new ProjectStatus()
            {
                Guid = new Guid("4fa85f64-5717-4562-b3fc-2c963f66afa6"),
                StatusName = "Active",
                AllowResource = true
            });
            projectStatus.Add(new ProjectStatus()
            {
                Guid = new Guid("4fa85f64-5718-4562-b3fc-2c963f66afa6"),
                StatusName = "OnHold",
                AllowResource = true
            });
            projectStatus.Add(new ProjectStatus()
            {
                Guid = new Guid("4fa85f64-5717-4562-b3fc-2c963f66afa6"),
                StatusName = "Closed",
                AllowResource = false
            });

            return projectStatus;
        }



        [Fact]
        public  void GetAllProjectStatus_ShouldReturnAllProjectStatus()
        {
            
            var status = new Mock<IProjectStatusService>();
            status.Setup(repo => repo.GetAll().Result).Returns(StaticData());
            var controller = new ProjectStatusController(status.Object);
            

            var result = controller.GetAll();


            var model = Assert.IsAssignableFrom<IEnumerable<ProjectStatus>>(result);
            Assert.Equal(3, model.Count());

        }

        
        [Fact]
        public  void Add_ShouldPost()
        {

            ProjectStatus projectStatus = new ProjectStatus()
            {
                Guid = new Guid("4fa85f64-5717-4562-b3fc-2c963f66afa6"),
                StatusName = "Active",
                AllowResource = true
            };
            ProjectStatusEntity entity = new ProjectStatusEntity()
            {
                Guid = new Guid("4fa85f64-5717-4562-b3fc-2c963f66afa6"),
                StatusName = "Active",
                AllowResource = true
            };
            ResponseDTO responseDTO = new ResponseDTO
            {
               
                Data = projectStatus,
                Message = "Added Successfully",
                ResponseStatus = ResponseStatus.Success,
                Ex = null

            };
            var status = new Mock<IProjectStatusService>();
            status.Setup(repo => repo.Add(It.IsAny<ProjectStatusEntity>()).Result).Returns(responseDTO);
            var controller = new ProjectStatusController(status.Object);

            
            var result = controller.Add(entity);
            
            
            var model = Assert.IsType<ProjectStatus>(projectStatus);
            Assert.Equal("Active", model.StatusName);
            Assert.True(model.AllowResource);

        }




    }
}
