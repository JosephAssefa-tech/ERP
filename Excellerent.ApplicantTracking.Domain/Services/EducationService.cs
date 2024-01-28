using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Services
{
    public class EducationService: CRUD<EducationEntity, Education>, IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        public EducationService(IEducationRepository respository): base(respository)
        {
            _educationRepository = respository;
        }
        public new async Task<ResponseDTO> Add(EducationEntity entity)
        {
            try
            {
                var model = await _educationRepository.AddAsync(entity.MapToModel());
                return new ResponseDTO(ResponseStatus.Success, "Successfully added", model.Guid);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDTO();
            }

        }
        public async Task<IEnumerable<EducationEntity>> GetByApplicantIdAsync(Guid applicantId)
        {
            try
            {
                var educations = await _educationRepository.GetQueryAsync(x => x.ApplicantId == applicantId);
                return educations?.Select(a => new EducationEntity(a));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<EducationEntity> GetByIdAsync(Guid educationId)
        {
            try
            {
                var education = await _educationRepository.GetByGuidAsync(educationId);
                return  new  EducationEntity(education);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<EducationEntity> FindOneAsyncForDelete(Guid guid)
        {
            try
            {
                var education = await _educationRepository.FindOneAsyncForDelete(x => x.Guid == guid);
                return education == null? null: new EducationEntity(education);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<IEnumerable<EducationEntity>> GetWithPredicateAsync(Guid? id, Guid? applicantId)
        {
            var predicate = PredicateBuilder.True<Education>();
            if (id != null)
                predicate = predicate.And(p => p.Guid == id);
            if (applicantId != null)
                predicate = predicate.And(p => p.ApplicantId == applicantId);
            
            try
            {
                return (await _educationRepository.GetWithPredicate(predicate))?.OrderByDescending(x => x.CreatedDate);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public new async Task<ResponseDTO> Update(EducationEntity entity)
        {
            var data = await _educationRepository.FindOneAsync(x => x.Guid == entity.Guid);
            if(data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "Not found", null);
            }
            var model = entity.MapToModel(data);
            await _educationRepository.UpdateAsync(model);
            return new ResponseDTO(ResponseStatus.Success, "Record updated successfully", null);
        }
        public async Task<bool>  GetExistenceByProgrammeAsync(Guid applicantId, Guid educationProgrammeId)
        {
            try
            {
                var record = (await  _educationRepository.GetQueryAsync(x => x.ApplicantId == applicantId
                && x.EducationProgrammeId == educationProgrammeId)).FirstOrDefault();

                if (record == null) return false;
                return record.EducationProgramme.Name.ToLower().Contains("high school");
            }
            catch(Exception  e)
            {
                return false;

            }
        }


    }
}
