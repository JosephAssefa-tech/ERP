using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmergencyAddressController : ControllerBase
    {
        private readonly IEmergencyAddressService _emergencyAddressService;

        public EmergencyAddressController(
            IEmergencyAddressService emergencyAddressService
            )
        {
            _emergencyAddressService = emergencyAddressService;
        }

        [Authorize]
        [HttpPost]
        public Task<ResponseDTO> Add(EmergencyAddressEntity emergencyAddress)
        {
            return _emergencyAddressService.Add(emergencyAddress);
        }
        [Authorize]
        [HttpPut]
        public async Task<ResponseDTO> EditEmergencyAddress(EmergencyAddressEntity emergencyAddressEntity)
        {
            return await _emergencyAddressService.Update(emergencyAddressEntity);
        }
    }
}
