using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Repository
{
    public interface ILUPositionSkillSetRepository : IAsyncRepository<LUPositionSkillSet>
    {
        Task<IEnumerable<LUSkillSet>> GetSkillsByPosition(Guid guid);
    }
}
