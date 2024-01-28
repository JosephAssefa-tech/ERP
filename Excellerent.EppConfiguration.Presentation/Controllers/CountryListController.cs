
using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.EppConfiguration.Presentation.Resource;
using Excellerent.EppConfiguration.Presentation.Resource.Dtos;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CountryListController : AuthorizedController
    {
        private const string resxFilename = @".\Resource\Countries.resx";
        public CountryListController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog)
            : base(httpContextAccessor, configuration, _businessLog, "")
        {

        }

        [AllowAnonymous]
        [HttpGet("iso")]
        public async Task<ResourceResponseDto> GetCountryList(String? country)
        {
            ExpressionStarter<Country> predicateBuilder = PredicateBuilder.New<Country>(true);

            if (!String.IsNullOrEmpty(country))
            {
                predicateBuilder = predicateBuilder.And(c => c.country.ToUpper().Equals(country.ToUpper()));
            }

            List<Country> countries = (await CountryListResourceReader.GetCountriesAndCities())
                .Select(cac => new Country() { iso2 = cac.iso2, iso3 = cac.iso3, country = cac.country })
                .Where(predicateBuilder)
                .ToList();

            return new ResourceResponseDto()
            {
                error = false,
                msg = "Country List",
                data = countries
            };
        }

        [AllowAnonymous]
        [HttpGet("states")]
        public async Task<ResourceResponseDto> GetCountryAndStatList(String? country)
        {
            ExpressionStarter<CountryAndState> predicateBuilder = PredicateBuilder.New<CountryAndState>(true);

            if (!String.IsNullOrEmpty(country))
            {
                predicateBuilder = predicateBuilder.And(cas => cas.name.ToUpper().Equals(country.ToUpper()));
            }

            List<CountryAndState> countryAndStates = (await CountryListResourceReader.GetCountryAndStates()).Where(predicateBuilder).ToList();

            return new ResourceResponseDto()
            {
                error = false,
                msg = "Country and state list",
                data = countryAndStates
            };
        }

        [AllowAnonymous]
        [HttpGet("codes")]
        public async Task<ResourceResponseDto> GetCountryAndCodeList(String? country)
        {
            ExpressionStarter<CountryAndCode> predicateBuilder = PredicateBuilder.New<CountryAndCode>(true);

            if (!String.IsNullOrEmpty(country))
            {
                predicateBuilder = predicateBuilder.And(cac => cac.name.ToUpper().Equals(country.ToUpper()));
            }

            List<CountryAndCode> countryAndCodes = (await CountryListResourceReader.GetCountryAndCodes()).Where(predicateBuilder).ToList();

            return new ResourceResponseDto()
            {
                error = false,
                msg = "Country And Code",
                data = countryAndCodes
            };
        }
    }
}
