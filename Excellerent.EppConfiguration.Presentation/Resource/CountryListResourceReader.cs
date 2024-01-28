using Excellerent.EppConfiguration.Presentation.Resource.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.Json;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Resource
{    
    public class CountryListResourceReader
    {
        private const string resouceFileName = "Excellerent.EppConfiguration.Presentation.Resource.Countries.resources";

        public static async Task<List<CountryAndCity>> GetCountriesAndCities() 
        {
            string countryAndCitiesJson = string.Empty;
            List<CountryAndCity> countryAndCities = new List<CountryAndCity>();
            try
            {
                countryAndCitiesJson = (await GetResourceData("countriesAndCities")).ToString();

                if (String.IsNullOrEmpty(countryAndCitiesJson))
                {
                    return countryAndCities;
                }

                countryAndCities = JsonSerializer.Deserialize(countryAndCitiesJson,  typeof(List<CountryAndCity>)) as List<CountryAndCity>;
            }
            catch (Exception ex)
            {
                countryAndCities.Clear();
            }

            return countryAndCities;
        }

        public static async Task<List<CountryAndState>> GetCountryAndStates()
        {
            String countryAndStatesJson = String.Empty;
            List<CountryAndState> countryAndStates = new List<CountryAndState>();
            try
            {
                countryAndStatesJson = (await GetResourceData("countriesAndStates")).ToString();

                if (countryAndStatesJson == null)
                {
                    return countryAndStates;
                }
                
                countryAndStates = JsonSerializer.Deserialize(countryAndStatesJson, typeof(List<CountryAndState>)) as List<CountryAndState>;
            }
            catch (Exception)
            {
                countryAndStates.Clear();
            }

            return countryAndStates;
        }

        public static async Task<List<CountryAndCode>> GetCountryAndCodes()
        {
            string countryAndCodesJson = String.Empty;
            List<CountryAndCode> countryAndCodes = new List<CountryAndCode>();
            try
            {
                countryAndCodesJson = (await GetResourceData("countryAndCode")).ToString();

                if (String.IsNullOrEmpty(countryAndCodesJson))
                {
                    return countryAndCodes;
                }

                countryAndCodes = JsonSerializer.Deserialize(countryAndCodesJson, typeof(List<CountryAndCode>)) as List<CountryAndCode>;
            }
            catch (Exception)
            {
                countryAndCodes.Clear();
            }

            return countryAndCodes;
        }

        public static async Task<JsonElement> GetResourceData(string key)
        {
            ResourceResponseDto resourceResponseDto = new ResourceResponseDto();

            ResourceManager resourceManager = new ResourceManager("Excellerent.EppConfiguration.Presentation.Resource.Countries", Assembly.GetExecutingAssembly());

            string resourceValueJson = resourceManager.GetString(key);

            resourceResponseDto = JsonSerializer.Deserialize(resourceValueJson, typeof(ResourceResponseDto)) as ResourceResponseDto;

            return resourceResponseDto.data;
        }
    }
}
