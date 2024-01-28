using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Repository
{
    public interface IApplicantAreaOfInterestRepository : IAsyncRepository<ApplicantAreaOfInterest>
    {
        Task<IEnumerable<ApplicantAreaOfInterest>> GetApplicantAreaOfInterest(Guid guid);
        Task<IEnumerable<ApplicantAreaOfInterest>> GetApplicantAreaOfInterestByPosition(Guid PositionToApplyID);
        Task<IEnumerable<ApplicantAreaOfInterest>> GetApplicantAreaOfInterestByProficiencyLevel(Guid ProficiencyLevelID);
        Task<IEnumerable<ApplicantAreaOfInterest>> GetApplicantAreaOfInterestByYearsOfExpierence(int YearsOfExpierence);
    }
}
