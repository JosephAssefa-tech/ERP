using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Presentation.Models.PostModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;


namespace Excellerent.ProjectManagement.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectModuleController : AuthorizedController
    {
        private readonly IProjectModuleService _projectModuleService;
        public ProjectModuleController(
            IHttpContextAccessor htttpContextAccessor,
            IConfiguration configuration,
            IBusinessLog _businessLog,
            IProjectModuleService projectModuleService
            ) : base(htttpContextAccessor, configuration, _businessLog, "Project")
        {
            _projectModuleService = projectModuleService;
        }

        [HttpGet("GetByEmployeeId")]
        public async Task<ResponseDTO> GetByEmployeeId(Guid employeeId)
        
        {
            return await _projectModuleService.GetClientsAndProjectsByEmployeeId(employeeId);
        }

        [HttpGet("GetProjectByClientAndEmployeeId")]
        public async Task<ResponseDTO> GetByClientAndEmployeeId(Guid clientId, Guid employeeId)
        {
            return await _projectModuleService.GetProjectsByClientAndEmployeeId(clientId, employeeId);
        }

        [HttpGet("GetClientByProjectId")]
        public async Task<ResponseDTO> GetClientByProjectId(Guid projectId)
        {
            return await _projectModuleService.GetClientsByProjectId(projectId);
        }

        [HttpGet("GetBySupervisorId")]
        public async Task<ResponseDTO> GetBySupervisorId(Guid supervisorId)
        {
            return await _projectModuleService.GetClientsAndProjectsBySupervisorId(supervisorId);
        }

        [HttpGet("GetProjectByClientAndSupervisorId")]
        public async Task<ResponseDTO> GetProjectByClientAndSupervisorId(Guid clientId, Guid supervisorId)
        {
            return await _projectModuleService.GetProjectsByClientAndSupervisorId(clientId, supervisorId);
        }
    }
}
