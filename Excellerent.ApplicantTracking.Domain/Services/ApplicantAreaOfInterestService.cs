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
    public  class ApplicantAreaOfInterestService : CRUD<ApplicantAreaOfInterestEntities, ApplicantAreaOfInterest>, IApplicantAreaOfInterestService
    {
        private readonly IApplicantAreaOfInterestRepository _applicantAreaOfInterestRepository;
        public ApplicantAreaOfInterestService(IApplicantAreaOfInterestRepository applicantAreaOfInterestRepository) : base(applicantAreaOfInterestRepository)
        {
            _applicantAreaOfInterestRepository = applicantAreaOfInterestRepository;
        }
        public async Task<Guid> AddApplicantAreaOfInterst(ApplicantAreaOfInterestEntities applicantAreaOfInterestToApply)
        {
            var model = applicantAreaOfInterestToApply.MapToModel();
            var data = await _applicantAreaOfInterestRepository.AddAsync(model);
            return data.Guid;
        } 

        public async Task<Guid> DeleteApplicantAreaOfInterst(Guid guid)
        {
            var data = await _applicantAreaOfInterestRepository.FindOneAsync(x => x.Guid == guid);
            await _applicantAreaOfInterestRepository.DeleteAsync(data);
            return data.Guid;

        }
        private ApplicantAreaOfInterestEntities SkillStringToArrayMapper(ApplicantAreaOfInterestEntities entityToMap)
        {
            entityToMap.SelectedIDArray = entityToMap.PrimarySkillSetID.Split(',').ToArray();
            entityToMap.SelectedIDSecondArray = entityToMap.PrimarySkillSetID.Split(',').ToArray();
            entityToMap.SelectedIDOtherArray = entityToMap.PrimarySkillSetID.Split(',').ToArray();

            return entityToMap;
        }
        public async Task<IEnumerable<ApplicantAreaOfInterestEntities>> GetApplicantAreaOfInterest(Guid guid)
        {
            var data = await _applicantAreaOfInterestRepository.FindAsync(x => x.ApplicantId == guid);
            
            var entityData = data?.OrderByDescending(X =>X.CreatedDate).Select(x => new ApplicantAreaOfInterestEntities(x));

            return entityData;
        }
        public async Task<ApplicantAreaOfInterestEntities> GetApplicantAreaOfInterestByPosition(Guid PositionToApplyID)
        {
            var data = await _applicantAreaOfInterestRepository.FindOneAsync(x => x.Guid == PositionToApplyID);
            return data != null ? new ApplicantAreaOfInterestEntities(data) : null;
        }

        public Task AddApplicantAreaOfInterst(ApplicantAreaOfInterest resAppAOInterst)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicantAreaOfInterestEntities> GetApplicantAreaOfInterestByProficiencyLevel(Guid ProficiencyLevelID)
        {
            var data = await _applicantAreaOfInterestRepository.FindOneAsync(x => x.Guid == ProficiencyLevelID);
            return data != null ? new ApplicantAreaOfInterestEntities(data) : null;
        }

        public async Task<ApplicantAreaOfInterestEntities> GetApplicantAreaOfInterestByYearsOfExpierence(int YearsOfExpierence)
        {
            var data = await _applicantAreaOfInterestRepository.FindOneAsync(x => x.YearsOfExpierence == YearsOfExpierence);
            return data != null ? new ApplicantAreaOfInterestEntities(data) : null;
        }

        public async Task<Guid> UpdateApplicantAreaOfInterest(ApplicantAreaOfInterestEntities areaOfInterestEntity)
        {
            var data = await _applicantAreaOfInterestRepository.FindOneAsync(x => x.Guid == areaOfInterestEntity.Guid);
            var model = areaOfInterestEntity.MapToModel(data);
            await _applicantAreaOfInterestRepository.UpdateAsync(model);
            return data.Guid;
        }
       
    }
}
