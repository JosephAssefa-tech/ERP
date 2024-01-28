using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
   public  class EmergencyConactsRepository : AsyncRepository<EmergencyContactsModel>, IEmergencyContactsRepository
    {
        private readonly EPPContext _dbcontext;
        public EmergencyConactsRepository(EPPContext context) : base(context)
        {
            _dbcontext = context;
        }



        public async Task<EmergencyContactsModel> AddAsync(EmergencyContactsModel _data)
        {
            var data = await _dbcontext.emergencycontact.AddAsync(_data);
            _dbcontext.SaveChanges();
            return data.Entity;
        }


        public async Task<IEnumerable<EmergencyContactsModel>> GetAllAsync()
        {
            return await _dbcontext.emergencycontact.ToListAsync();



        }


        public async Task<EmergencyContactsModel> GetByGuidAsync(Guid guid)
        {
            return await _dbcontext.emergencycontact.FindAsync(guid);



        }





    }
    
}
