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
    public class BillingAddressController : AuthorizedController
    {
        private readonly IBillingAddressService _billingAddress;

        public BillingAddressController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IBillingAddressService billingAddress) : base(htttpContextAccessor, configuration, _businessLog, "BillingAddress")
        {
            _billingAddress = billingAddress;
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Create_Client" })]
        public async Task<ResponseDTO> AddBillingAddress(BillingAddressPostModel model)
        {
            return await _billingAddress.Add(model.MappToEntity());
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Client" })]
        public async Task<ResponseDTO> GetBillAddress()
        {
            return new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Data = await _billingAddress.GetAll(),
                Message = "Billing address list",
                Ex = null
            };
        }
        [HttpDelete("{id}")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Client" })]
        public async Task<ResponseDTO> DeleteBillingAddress(Guid id)
        {
            return await _billingAddress.DeleteBillingAddress(id);
        }
    }
}