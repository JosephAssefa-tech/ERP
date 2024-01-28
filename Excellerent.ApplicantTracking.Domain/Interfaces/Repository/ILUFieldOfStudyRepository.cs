using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Repository
{
    public interface ILUFieldOfStudyRepository : IAsyncRepository<LUFieldOfStudy>
    {
        Task<IEnumerable<LUFieldOfStudy>> GetByFieldOfStudy(string educationName);
    }

}
