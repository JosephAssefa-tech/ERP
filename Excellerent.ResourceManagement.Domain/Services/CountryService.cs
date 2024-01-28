using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class CountryService : CRUD<CountryEntity, Country>, ICountryService
    {

        private readonly ICountryRepository _repository;
        public CountryService(ICountryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckCountryExistance(string name)
        {
          bool result =  await _repository.CheckCountryExistence(name);
          return result;
        }

        public async Task<Country> GetCountryById(Guid countryId)
        {
            var result = await _repository.GetCountryById(countryId);
            return result;
        }
    }
}
