using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Repository
{
    public interface ILUPositionToApplyRepository : IAsyncRepository<LUPositionToApply>
    {
        Task<IEnumerable<LUPositionToApply>> GetByPosition(string name);
    }

}
