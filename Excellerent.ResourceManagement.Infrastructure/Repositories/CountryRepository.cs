
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
    public class CountryRepository : AsyncRepository<Country>, ICountryRepository
    {
        private readonly EPPContext _context;
        public CountryRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckCountryExistence(string name)
        {
            var result = await _context.Countries.Where(x => x.Name == name).CountAsync() > 0 ? true : false;
            return result;
        }

        public async Task<Country> GetCountryById(Guid countryId) {

            var result = await _context.Countries.Where(x => x.Guid == countryId).FirstOrDefaultAsync();
            return result;
        }
    }
}
