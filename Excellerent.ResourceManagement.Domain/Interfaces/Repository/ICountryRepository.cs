using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Repository
{
    public interface ICountryRepository : IAsyncRepository<Country>
    {
        public Task<bool> CheckCountryExistence(string name);

        public Task<Country> GetCountryById(Guid countryId);
    }
}
