using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Presentation.Dtos;
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
    public class DeviceDetailsController : Controller
    {
        private readonly IDeviceDetailsService _deviceDetailsService;
        public DeviceDetailsController(IDeviceDetailsService deviceDetailsService)
        {
            _deviceDetailsService = deviceDetailsService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseDTO> Add(DeviceDetailDto deviceDetailDto)
        {
            return await _deviceDetailsService.Add(deviceDetailDto.MapToEntity());
        }

        [HttpPut]
        public async Task<ResponseDTO> Update(DeviceDetailsEntity deviceDetailEntity)
        {
            return await _deviceDetailsService.Update(deviceDetailEntity);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PredicatedResponseDTO> GetPaginated(Guid? id, string searchKey, int? pageindex, int? pageSize)
        {
            return await _deviceDetailsService.GetWithPredicate(id, searchKey, pageindex, pageSize);

        }
        [HttpGet("Get")]
        [AllowAnonymous]
        public async Task<ResponseDTO> Get(Guid id)
        {
            return await _deviceDetailsService.Get(id);
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<ResponseDTO> Delete(Guid id)
        {
            var d = await _deviceDetailsService.FindOneAsyncForDelete(id);
            return await _deviceDetailsService.Delete(d == null ? null : new DeviceDetailsEntity(d));
            // ResponseDTO e = await _deviceDetailsService.Get(id);
            // DeviceDetailsEntity d = (DeviceDetailsEntity) e.Data;
            // return await _deviceDetailsService.Delete(d);
        }

        /*[HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<DeviceDetails>> Get()
        {
            return await _deviceDetailsService.GetAll();
        }*/


    }
}
