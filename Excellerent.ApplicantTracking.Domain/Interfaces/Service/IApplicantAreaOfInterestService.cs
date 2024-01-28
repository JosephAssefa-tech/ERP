using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Service
{
    public interface IApplicantAreaOfInterestService : ICRUD<ApplicantAreaOfInterestEntities, ApplicantAreaOfInterest>
    {
        Task<Guid> AddApplicantAreaOfInterst(ApplicantAreaOfInterestEntities model);  
        Task<IEnumerable<ApplicantAreaOfInterestEntities>> GetApplicantAreaOfInterest(Guid guid);
        Task<ApplicantAreaOfInterestEntities> GetApplicantAreaOfInterestByPosition(Guid PositionToApplyID);
        Task<ApplicantAreaOfInterestEntities> GetApplicantAreaOfInterestByProficiencyLevel(Guid ProficiencyLevelID);
        Task<ApplicantAreaOfInterestEntities> GetApplicantAreaOfInterestByYearsOfExpierence(int YearsOfExpierence);
        public Task<Guid> UpdateApplicantAreaOfInterest(ApplicantAreaOfInterestEntities model);
        public Task<Guid> DeleteApplicantAreaOfInterst(Guid guid);
    }
}
