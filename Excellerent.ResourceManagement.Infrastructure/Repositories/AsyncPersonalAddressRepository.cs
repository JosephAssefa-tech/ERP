using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
    public class AsyncPersonalAddressRepository :
        AsyncRepository<PersonalAddress>,
        IAsyncPersonalAddressRepository
    {
        private readonly EPPContext _context;
        public AsyncPersonalAddressRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAddress(Guid id)
        {
            var result = await _context.PersonalAddresses.Where(x => x.Guid == id).CountAsync() > 0 ? true : false;
            return true;
        }

    }
}
