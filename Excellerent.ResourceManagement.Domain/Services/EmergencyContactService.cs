using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class EmergencyContactService : CRUD<EmergencyContactsEntity, EmergencyContactsModel>, IEmergencyContactService
    {
        private readonly IEmergencyContactRepository _contactRepository;
        public EmergencyContactService(IEmergencyContactRepository contactRepository) : base(contactRepository)
        {
            _contactRepository = contactRepository;
        }


        public async Task<Guid> AddAsync(EmergencyContactsModel entity)
        {
            var data = await _contactRepository.AddAsync(entity);
            return data.Guid;
        }

        public async  Task<Guid> AddEmergencyContactAsync(EmergencyContactsModel model)
        {
            var data = await _contactRepository.AddAsync(model);
            return data.Guid;
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
