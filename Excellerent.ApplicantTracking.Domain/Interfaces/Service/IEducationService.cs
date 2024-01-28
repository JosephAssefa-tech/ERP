using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Service
{
    public interface IEducationService:  ICRUD<EducationEntity, Education>
    {
        Task<IEnumerable<EducationEntity>> GetByApplicantIdAsync(Guid applicantId);
        Task<EducationEntity> GetByIdAsync(Guid educationId);
        Task<IEnumerable<EducationEntity>> GetWithPredicateAsync(Guid? id, Guid? applicantId);
        new Task<ResponseDTO> Add(EducationEntity entity);
        Task<EducationEntity> FindOneAsyncForDelete(Guid guid);
        new Task<ResponseDTO> Update(EducationEntity entity);
        public Task<bool> GetExistenceByProgrammeAsync(Guid applicantId, Guid educationProgrammeId);

    }
}
