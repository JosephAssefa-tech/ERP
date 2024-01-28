using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using Excellerent.APIModularization.Controllers;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.APIModularization.Logging;
using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.SharedModules.DTO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DutyBranchController : AuthorizedController
    {
        private readonly IDutyBranchService _dutyBranchService;
        public DutyBranchController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IDutyBranchService dutyBranchService) : base(htttpContextAccessor, configuration, _businessLog, "DutyBranch")
        {
            _dutyBranchService = dutyBranchService;
        }

        [Authorize]
        [HttpPost("RegisterDutyBranch")]
        public async Task<ActionResult<ResponseDTO>> RegisterDutyBranch([FromBody] DutyBranchEntity dutyBranch)
        {
            if (!await _dutyBranchService.CheckDutyBranchExistance(dutyBranch.CountryId, dutyBranch.Name))
            {
                var result = await _dutyBranchService.Add(dutyBranch);
                return new ResponseDTO(ResponseStatus.Success, "Duty station registered successfully", dutyBranch);
            }
            else
            {
                return new ResponseDTO(ResponseStatus.Error, "There is already registered duty branch under the selected country!", dutyBranch);
            }
        }

        [Authorize]
        [HttpGet("GetDutyBranchByCountryId/{countryId?}")]
        public async Task<ActionResult<IEnumerable<DutyBranchEntity>>> GetDutyBranchByCountryId(Guid countryId)
        {
            var result = await _dutyBranchService.GetDutyBranchByCountry(countryId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetDutyBranchByCountryName/{name?}")]
        public async Task<ActionResult<IEnumerable<DutyBranchEntity>>> GetDutyBranchByCountryName(string name)
        {
            var result = await _dutyBranchService.GetDutyBranchByCountryName(name);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("UpdateDutyBranch")]
        public async Task<ResponseDTO> EditDutyBranch([FromBody] DutyBranchEntity dutyBranch)
        {
            var result = await _dutyBranchService.Update(dutyBranch);
            return new ResponseDTO(ResponseStatus.Success, "Duty branch updated successfully", dutyBranch);
        }

        [Authorize]
        [HttpPut("DeleteDutyBranch/")]
        public async Task<ResponseDTO> DeleteDutyBranch([FromBody] DutyBranchEntity dutyBranch)
        {
            var result = await _dutyBranchService.Delete(dutyBranch);
            if(result.ResponseStatus == ResponseStatus.Success)
            {
                return new ResponseDTO(ResponseStatus.Success, "Duty branch deleted successfully", dutyBranch);
            }
            else
            {
                return new ResponseDTO(ResponseStatus.Error, "Duty branch  can not be deleted", dutyBranch);
            }
        }
    }
}
