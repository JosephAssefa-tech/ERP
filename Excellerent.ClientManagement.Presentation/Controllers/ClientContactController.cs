
using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ClientManagement.Presentation.Models.PostModels;
using Excellerent.ClientManagement.Presentation.Models.UpdateModels;
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
    public class ClientContactController : AuthorizedController
    {
        private readonly IClientContactService _clientContactService;

        public ClientContactController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IClientContactService clientContactService) : base(htttpContextAccessor, configuration, _businessLog, "ClientContact")
        {
            _clientContactService = clientContactService;
        }

        [HttpDelete("{id}")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Client" })]
        public async Task<ResponseDTO> DeleteClientContact(Guid id)
        {
            return await _clientContactService.DeleteClientContact(id);
        }
    }
}