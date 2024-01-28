using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmergencyContactsController : Controller
    {
        private readonly IEmergencyContactsService _emservice;

        public EmergencyContactsController(IEmergencyContactsService emservice)
        {
            _emservice = emservice;
        }



        [Authorize]
        [HttpPost]
        public async Task<ResponseDTO> AddEmergencyContact(EmergencyContactsEntity entity)
        {
            return await _emservice.Add(entity);
            //return (new ResponseDTO
            //{
            //    Data = data,
            //    Message = "Emergency Contact added Sussessfully!",
            //    ResponseStatus = ResponseStatus.Success,
            //    Ex = null
            //});
        }

        [Authorize]
        [HttpGet]
        public Task<IEnumerable<EmergencyContactsModel>> GetAll()
        {
            return _emservice.GetAll();

        }

        [Authorize]
        [HttpDelete]
        public async Task<bool> Delete(string email)
        {
            return await _emservice.DeleteEmergencyContact(email);
        }

    }
}
