using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Repository
{
    public interface IAsyncPersonalAddressRepository : IAsyncRepository<PersonalAddress>
    {
        Task<bool> DeleteAddress(Guid id);
    }
}
