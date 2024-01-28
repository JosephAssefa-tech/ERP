using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Presentation.Models.PostModels;
using Excellerent.SharedModules.DTO;
using Excellerent.UserManagement.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OperatingAddressController : AuthorizedController
    {
        private readonly IOperatingAddressService _operatingAddress;

        public OperatingAddressController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IOperatingAddressService operatingAddress) : base(htttpContextAccessor, configuration, _businessLog, "OperatingAddress")
        {
            _operatingAddress = operatingAddress;
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Create_Client" })]
        public async Task<ResponseDTO> AddOperatingAddress(OperatingAddressPostModel model)
        {
            return await _operatingAddress.Add(model.MappToEntity());
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Client" })]
        public async Task<ResponseDTO> GetOperatingAddress()
        {
            return new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Data = await _operatingAddress.GetAll(),
                Message = "Operating address list",
                Ex = null
            };
        }
        [HttpDelete("{id}")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Client" })]
        public async Task<ResponseDTO> DeleteOperatingAddress(Guid id)
        {
            return await _operatingAddress.DeleteOperatingAddress(id);
        }
    }
}