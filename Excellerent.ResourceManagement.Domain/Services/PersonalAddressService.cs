using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class PersonalAddressService :
        CRUD<PersonalAddressEntity, PersonalAddress>,
        IPersonalAddressService
    {
        private readonly IAsyncPersonalAddressRepository _repository;
        public PersonalAddressService(IAsyncPersonalAddressRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeletePersonalAddress(Guid id)
        {
            var member = _repository.FindOneAsyncForDelete(x => x.Guid == id);
            await _repository.DeleteAsync(member.Result);
            return true;
        }
    }
}
