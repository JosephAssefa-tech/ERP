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
    public class LUSkillSetService : CRUD<LUSkillSetEntity, LUSkillSet>, ILUSkillSetService
    {
        private readonly ILUSkillSetRepository _luSkillSetServiceRepo;
        public LUSkillSetService(ILUSkillSetRepository luSkillSetServiceRepo) : base(luSkillSetServiceRepo)
        {
            _luSkillSetServiceRepo = luSkillSetServiceRepo;
        }
        public async Task<LUSkillSetEntity> GetSkillSet(string SkillName)
        {
            var data = await _luSkillSetServiceRepo.FindOneAsync(x => x.SkillName == SkillName);
            return data != null ? new LUSkillSetEntity(data) : null;

        }
        public async Task<Guid> UpdatePositionToApply(LUSkillSetEntity SkillSet)
        {
            var data = await _luSkillSetServiceRepo.FindOneAsync(x => x.SkillName == SkillSet.Name);
            var model = SkillSet.MapToModel(data);
            await _luSkillSetServiceRepo.UpdateAsync(model);
            return data.Guid;
        }

        public async Task<Guid> AddSkill(LUSkillSetEntity SkillSetEntity)
        {
            var model = SkillSetEntity.MapToModel();
            var data = await _luSkillSetServiceRepo.AddAsync(model);
            return data.Guid;
        }
        public async Task<LUSkillSetEntity> GetBySkillName(string SkillName)
        {
            var data = await _luSkillSetServiceRepo.FindOneAsync(x => x.SkillName == SkillName);
            return data != null ? new LUSkillSetEntity(data) : null;
        }

        async  public Task<IEnumerable<LUSkillSetEntity>> GetAllSkillSet()
        {
            var data = await _luSkillSetServiceRepo.GetAllAsync();
            return data?.Select(x => new LUSkillSetEntity(x));
        }


     
    }
}
