using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Services
{
    public class LUProficiencyLevelService : CRUD<LUProficiencyLevelEntity, LUProficiencyLevel>, ILUProficiencyLevelService
    {
        private readonly ILUProficiencyLevelRepository _luProficiencyLevelServiceRepo;
        public LUProficiencyLevelService(ILUProficiencyLevelRepository luProficiencyLevelServiceRepo) : base(luProficiencyLevelServiceRepo)
        {
            _luProficiencyLevelServiceRepo = luProficiencyLevelServiceRepo;
        }


        public async Task<LUProficiencyLevelEntity> GetPositionToApply(string Name)
        {
            var data = await _luProficiencyLevelServiceRepo.FindOneAsync(x => x.Name == Name);
            return data != null ? new LUProficiencyLevelEntity(data) : null;

        }
        public async Task<Guid> UpdatePositionToApply(LUProficiencyLevelEntity proficiencyLevelEntity)
        {
            var data = await _luProficiencyLevelServiceRepo.FindOneAsync(x => x.Name == proficiencyLevelEntity.Name);
            var model = proficiencyLevelEntity.MapToModel(data);
            await _luProficiencyLevelServiceRepo.UpdateAsync(model);
            return data.Guid;
        }

        public async Task<Guid> AddProficiency(LUProficiencyLevelEntity proficiencyLevelEntity)
        {
            var model = proficiencyLevelEntity.MapToModel();
            var data = await _luProficiencyLevelServiceRepo.AddAsync(model);
            return data.Guid;
        }

        public async Task<LUProficiencyLevelEntity> GetByProficiencyName(string ProficiencyLevelName)
        {
            var data = await _luProficiencyLevelServiceRepo.FindOneAsync(x => x.Name == ProficiencyLevelName);
            return data != null ? new LUProficiencyLevelEntity(data) : null;
        }

        async public Task<IEnumerable<LUProficiencyLevelEntity>> GetAllProficiencyLevels()
        {
            var data = await _luProficiencyLevelServiceRepo.GetAllAsync();
            return data?.Select(x => new LUProficiencyLevelEntity(x));
        }
    }

}
