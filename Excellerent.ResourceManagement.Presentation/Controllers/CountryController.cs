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
    public class CountryController : AuthorizedController
    {
        private readonly ICountryService _countryService;
        public CountryController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, ICountryService countryService) : base(htttpContextAccessor, configuration, _businessLog, "Country")
        {
            _countryService = countryService;
        }
        [Authorize]
        [HttpPost("register")]
        public async Task<ResponseDTO> RegisterCountry([FromBody] CountryEntity country)
        {
            if(!await _countryService.CheckCountryExistance(country.Name))
            {
                var result = await _countryService.Add(country);
                return new ResponseDTO(ResponseStatus.Success, "Country registered successfully", country);
            }
            else
            {
               return new ResponseDTO(ResponseStatus.Error, "There is already registered country with information you provided", country);
            } 
        }
        [Authorize]
        [HttpGet("GetAllCountries")]
        public async Task<ActionResult<IEnumerable<CountryEntity>>> GetAllCountries()
        {
            var result = await _countryService.GetAll();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetCountryById/{countryId?}")]
        public async Task<ActionResult<CountryEntity>> GetCountryById(Guid countryId)
        {
            var result = await _countryService.GetCountryById(countryId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("UpdateCountry")]
        public async Task<ResponseDTO> EditCountry([FromBody] CountryEntity country)
        {
            var result = await _countryService.Update(country);
            return new ResponseDTO(ResponseStatus.Success, "Country updated successfully", country);
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<ResponseDTO> DeleteCountry([FromBody] CountryEntity country)
        {
            var result =  await _countryService.Delete(country);
            if (result.ResponseStatus == ResponseStatus.Success)
            {
                return new ResponseDTO(ResponseStatus.Success, "Country deleted successfully", country);
            }
            else
            {
                return new ResponseDTO(ResponseStatus.Error, "Country can not be deleted", country);
            }
        }
    }
}
