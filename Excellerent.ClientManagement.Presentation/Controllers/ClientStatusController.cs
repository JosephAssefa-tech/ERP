using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.UserManagement.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ClientStatusController : AuthorizedController
    {
        private readonly IClientStatusService _clientStatusService;

        public ClientStatusController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IClientStatusService clientStatusService) : base(htttpContextAccessor, configuration, _businessLog, "ClientStatus")
        {
            _clientStatusService = clientStatusService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ClientStatus> GetAll()
        {
            return _clientStatusService.GetAll().Result;
        }

        [HttpGet("{id}")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Client" })]
        public async Task<ClientStatus> GetClientStatusById(Guid id)
        {
            return await _clientStatusService.GetClientStatusById(id);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Create_Client" })]
        public async Task<ResponseDTO> Add(ClientStatusEntity clientStatusEntity)
        {
            return await _clientStatusService.Add(clientStatusEntity);
        }
    }
}