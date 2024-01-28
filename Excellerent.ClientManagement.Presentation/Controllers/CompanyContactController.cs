using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
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
    public class CompanyContactController : AuthorizedController
    {
        private readonly ICompanyContactService _companyContactService;

        public CompanyContactController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, ICompanyContactService companyContactService) : base(htttpContextAccessor, configuration, _businessLog, "CompanyContact")
        {
            _companyContactService = companyContactService;
        }

        [HttpDelete("{id}")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Client" })]
        public async Task<ResponseDTO> DeletecompanyContact(Guid id)
        {
            return await _companyContactService.DeleteCompanyContact(id);
        }
    }
}