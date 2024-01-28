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
    public class LUFieldOfStudyService : CRUD<LUFieldOfStudyEntity, LUFieldOfStudy>, ILUFieldOfStudyService
    {
        private readonly ILUFieldOfStudyRepository _fieldOfStudyServiceRepo;
        public LUFieldOfStudyService(ILUFieldOfStudyRepository fieldOfStudyServiceRepo) : base(fieldOfStudyServiceRepo)
        {
            _fieldOfStudyServiceRepo = fieldOfStudyServiceRepo;
        }

        public async Task<LUFieldOfStudyEntity> GetFieldOfStudy(string EducationName)
        {
            var data = await _fieldOfStudyServiceRepo.FindOneAsync(x => x.EducationName == EducationName);
            return data != null ? new LUFieldOfStudyEntity(data) : null;

        }
        public async Task<Guid> UpdatePositionToApply(LUFieldOfStudyEntity fieldOfStudyEntity)
        {
            var data = await _fieldOfStudyServiceRepo.FindOneAsync(x => x.EducationName == fieldOfStudyEntity.Name);
            var model = fieldOfStudyEntity.MapToModel(data);
            await _fieldOfStudyServiceRepo.UpdateAsync(model);
            return data.Guid;
        }

        public async Task<Guid> AddFieldOfStudy(LUFieldOfStudyEntity fieldOfStudy)
        {
            var model = fieldOfStudy.MapToModel();
            var data = await _fieldOfStudyServiceRepo.AddAsync(model);
            return data.Guid;
        }

        public async Task<LUFieldOfStudyEntity> GetByEducationName(string EducationName)
        {
            var data = await _fieldOfStudyServiceRepo.FindOneAsync(x => x.EducationName == EducationName);
            return data != null ? new LUFieldOfStudyEntity(data) : null;
        }
        public async Task<LUFieldOfStudyEntity> GetByIdAsync(Guid fieldOfStudyId)
        {
            var data = await _fieldOfStudyServiceRepo.FindOneAsync(x => x.Guid == fieldOfStudyId);
            return data != null ? new LUFieldOfStudyEntity(data) : null;
        }
        public new async Task<IEnumerable<LUFieldOfStudyEntity>> GetAll()
        {
            var result = await this._fieldOfStudyServiceRepo.GetAllAsync();
            return result?.Select(x => new LUFieldOfStudyEntity(x));
        }
    }
}
