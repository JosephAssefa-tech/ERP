using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : AuthorizedController
    {
        private readonly IClientService _clientService;
        public ClientController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IClientService clientService) : base(htttpContextAccessor, configuration, _businessLog, "Client")
        {
            _clientService = clientService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Client> Get()
        {
            return _clientService.GetAll().Result;

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseDTO> Add(ClientEntity clientEntity)
        {
            return await _clientService.Add(clientEntity);
        }


        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseDTO> Edit(ClientEntity clientEntity)
        {
            var data = _clientService.GetClientByGuid(clientEntity.Guid);
            var model = new ClientEntity(data.Result);
            return await _clientService.Update(model);
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<ResponseDTO> Remove(ClientEntity clientEntity)
        {

            return await _clientService.Delete(clientEntity);
        }
    }
}
