using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DeviceClassificationController : Controller
    {
        private readonly IDeviceClassificationService _deviceClassificationService;
        public DeviceClassificationController(IDeviceClassificationService deviceClassificationService)
        {
            _deviceClassificationService = deviceClassificationService;
        }

        [HttpGet]
        public List<DeviceClassification> List()
        {
            return _deviceClassificationService.GetDeviceClassifications();
        }
    }
}
