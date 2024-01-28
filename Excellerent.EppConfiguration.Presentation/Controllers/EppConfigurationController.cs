using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.EppConfiguration.Domain.Dtos;
using Excellerent.EppConfiguration.Domain.Interfaces.Service;
using Excellerent.EppConfiguration.Domain.Mapping;
using Excellerent.SharedModules.DTO;
using Excellerent.UserManagement.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EppConfigurationController : AuthorizedController
    {
        private readonly IEppConfigurationService _eppConfigurationService;
        private readonly static string _feature = "EppConfig";

        public EppConfigurationController(IEppConfigurationService eppConfigService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog)
            : base(httpContextAccessor, configuration, _businessLog, _feature)
        {
            _eppConfigurationService = eppConfigService;
        }

        [HttpGet]
        public async Task<ResponseDTO> GetTimeSheetConfiguration()
        {
            return await _eppConfigurationService.GetEppConfiguration();
        }

        [HttpPost]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Update_Timesheet_Configuration" })]
        public async Task<ResponseDTO> AddTimeSheetConfiguration(ConfigurationDto timesheetConfig)
        {
            return await _eppConfigurationService.AddEppConfiguration(timesheetConfig.MapToEntity());
        }
    }
}
