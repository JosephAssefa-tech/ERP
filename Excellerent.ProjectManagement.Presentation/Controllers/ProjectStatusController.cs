using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Presentation.Controllers
{
    //[ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectStatusController : ControllerBase
    {
        private readonly IProjectStatusService _projectStatusService;
        public ProjectStatusController(IProjectStatusService projectStatusService)
        {
            _projectStatusService = projectStatusService;

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseDTO> Add(ProjectStatusEntity projectStatusEntity)
        {
            return await _projectStatusService.Add(projectStatusEntity);
        }
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ProjectStatus> GetAll()
        {
            return _projectStatusService.GetAll().Result;
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ProjectStatus> GetProjectStatusById(Guid id)
        {
            return await _projectStatusService.GetProjectStatusById(id);
        }
        [HttpDelete]
        [AllowAnonymous]
        public ResponseDTO Delete(Guid id)
        {
            ProjectStatusEntity projectStatusEntity = new ProjectStatusEntity();
            projectStatusEntity.Guid = id;
            return _projectStatusService.Delete(projectStatusEntity).Result;

        }



    }
}
