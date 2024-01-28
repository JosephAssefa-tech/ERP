using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class EmergencyContactsService : CRUD<EmergencyContactsEntity, EmergencyContactsModel>, IEmergencyContactsService
    {

        private readonly IEmergencyContactsRepository  _contactRepository;
        public EmergencyContactsService(IEmergencyContactsRepository contactRepository) : base(contactRepository)
        {
            _contactRepository = contactRepository;
        }


        public async Task<Guid> AddAsync(EmergencyContactsModel entity)
        {
            var data = await _contactRepository.AddAsync(entity);
            return data.Guid;
        }

        public async Task<Guid> AddEmergencyContactAsync(EmergencyContactsModel model)
        {
            var data = await _contactRepository.AddAsync(model);
            return data.Guid;
        }

        public async Task<bool> DeleteEmergencyContact(string email)
        {
            var contact = await _contactRepository.FindOneAsyncForDelete(x => x.email == email);
            await _contactRepository.DeleteAsync(contact);
            return true;
        }

        public async Task<IEnumerable<EmergencyContactsModel>> GetAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<EmergencyContactsModel> GetEmergencyContactByEmployee(Guid employeeguid)
        {
            return await _contactRepository.GetByGuidAsync(employeeguid);
        }
    }
}
