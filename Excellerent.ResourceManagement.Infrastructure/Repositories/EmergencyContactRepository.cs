using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Infrastructure.Dtos;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
    public class EmergencyContactRepository : AsyncRepository<EmergencyContactsModel>, IEmergencyContactRepository
    {
        private readonly EPPContext _dbcontext;
        public EmergencyContactRepository(EPPContext context) : base(context)
        {
            _dbcontext = context;
        }



        public async Task<EmergencyContactsModel> AddAsync(EmergencyContactsModel _data)
        {
            var data = await _dbcontext.emergencycontacts.AddAsync(_data);
            _dbcontext.SaveChanges();
            return data.Entity;
        }


        public async Task<IEnumerable<EmergencyContactDto>> GetAllAsync()
        {
            return (IEnumerable<EmergencyContactDto>)await _dbcontext.emergencycontacts.ToListAsync();


           
        }


        public async Task<EmergencyContactsModel> GetByGuidAsync(Guid guid)
        {
            return await _dbcontext.emergencycontacts.FindAsync(guid);



        }



    }
}
