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

    public class LUPositionToApplyService : CRUD<LUPositionToApplyEntity, LUPositionToApply>, ILuPositionService
    {
        private readonly ILUPositionToApplyRepository _positionToApplyRepository;
        public LUPositionToApplyService(ILUPositionToApplyRepository positionToApplyRepo) : base(positionToApplyRepo)
        {
            _positionToApplyRepository = positionToApplyRepo;
        }


        public async Task<LUPositionToApplyEntity> GetPositionToApply(string Name)
        {
            var data = await _positionToApplyRepository.FindOneAsync(x => x.Name == Name);
            return data != null ? new LUPositionToApplyEntity(data) : null;

        }
        public async Task<Guid> UpdatePositionToApply(LUPositionToApplyEntity positionToApplyEntitie)
        {
            var data = await _positionToApplyRepository.FindOneAsync(x => x.Name == positionToApplyEntitie.Name);
            var model = positionToApplyEntitie.MapToModel(data);
            await _positionToApplyRepository.UpdateAsync(model);
            return data.Guid;
        }
        public async Task<Guid> AddPosition(LUPositionToApplyEntity positionToApply)
        {
            var model = positionToApply.MapToModel();
            var data = await _positionToApplyRepository.AddAsync(model);
            return data.Guid;
        }

        public async Task<LUPositionToApplyEntity> GetByPositionName(string PositionName)
        {
            var data = await _positionToApplyRepository.FindOneAsync(x => x.Name == PositionName);
            return data != null ? new LUPositionToApplyEntity(data) : null;
        }

        async public Task<IEnumerable<LUPositionToApplyEntity>> GetAllPositions()
        {
            var data = await _positionToApplyRepository.GetAllAsync();
            return data?.Select(x => new LUPositionToApplyEntity(x));
        }
    }

}

