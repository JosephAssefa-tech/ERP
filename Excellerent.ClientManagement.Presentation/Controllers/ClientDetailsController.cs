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
    public class ClientDetailsController : AuthorizedController
    {
        private readonly IClientDetailsService _clientDetailsService;

        public ClientDetailsController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IClientDetailsService clientDetailsService) : base(htttpContextAccessor, configuration, _businessLog, "ClientDetails")
        {
            _clientDetailsService = clientDetailsService;
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Client,Create_Project" })]
        public async Task<ResponseDTO> Get()
        {
            return await _clientDetailsService.GetClientFullData();
        }
        [HttpGet("SelectClientById")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Client" })]
       //[AllowAnonymous]
        public async Task<ResponseDTO> GetClientById(Guid clientId)
        {
            var data = await _clientDetailsService.GetClientById(clientId);

            return new ResponseDTO
            {
                Data = data,
                Message = "A single client details.",
                ResponseStatus = data != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };
        }

        [HttpGet("Predicated")]

        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Client" })]
        //[AllowAnonymous]
        public async Task<PredicatedResponseDTO> Get(Guid? id, string searchKey, int? pageindex, int? pageSize)
        {
            return await _clientDetailsService.GetPaginatedClient(id, searchKey, pageindex, pageSize);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Create_Client" })]
        //[AllowAnonymous]
        public async Task<ResponseDTO> Add(ClientPostModel model)
        {
            return await _clientDetailsService.AddNewClient(model.MappToEntity());
        }

        [HttpPut("EditClient")]
        //[AllowAnonymous]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Update_Client" })]
        public async Task<ResponseDTO> EditClient(UpdateClientModel model)
        {
           
            return await _clientDetailsService.UpdateClient(model.MappToEntity());
        }
        [HttpDelete("{id}")]
        // [AllowAnonymous]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Client" })]
        public async Task<ResponseDTO> RemoveClient(Guid id)
        {
            return await _clientDetailsService.DeleteClient(id,false);
        }

        [HttpGet("isEmployeeSalesPerson/")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Employee" })]
        public async Task<bool> IsEmployeeSalesPerson(Guid id)
        {
             return await _clientDetailsService.IsEmployeeSalesPerson(id);
             
        }
        [HttpDelete("check")]
        // [AllowAnonymous]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Client" })]
        public async Task<ResponseDTO> CheckClient(Guid id)
        {
            return await _clientDetailsService.DeleteClient(id, true);
        }
    }
}