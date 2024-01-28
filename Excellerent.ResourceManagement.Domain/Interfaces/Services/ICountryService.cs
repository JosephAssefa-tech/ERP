using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System.Threading.Tasks;
using System;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Services
{
    public interface ICountryService : ICRUD<CountryEntity, Country>
    {
        public Task<bool> CheckCountryExistance(string name);

        public Task<Country> GetCountryById(Guid countryId);
    }
}
