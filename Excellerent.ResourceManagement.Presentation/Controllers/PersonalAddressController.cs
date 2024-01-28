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
    public class PersonalAddressController : ControllerBase
    {

        private readonly IPersonalAddressService _personalAddressService;

        public PersonalAddressController(
            IPersonalAddressService personalAddressService
            )
        {
            _personalAddressService = personalAddressService;
        }

        [Authorize]
        [HttpPost]
        public Task<ResponseDTO> Add(PersonalAddressEntity personalAddress)
        {
            return _personalAddressService.Add(personalAddress);
        }
        [Authorize]
        [HttpPut]
        public async Task<ResponseDTO> EditPersonalAddress(PersonalAddressEntity personalAddressEntity)
        {
            return await _personalAddressService.Update(personalAddressEntity);
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<PersonalAddress>> Get()
        {
            return await _personalAddressService.GetAll();
        }
        [Authorize]
        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
            return await _personalAddressService.DeletePersonalAddress(Guid.Parse(id));
        }



    }

}
