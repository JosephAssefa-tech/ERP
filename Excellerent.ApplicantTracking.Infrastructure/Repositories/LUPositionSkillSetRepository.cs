using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Infrastructure.Repositories
{
    public class LUPositionSkillSetRepository : AsyncRepository<LUPositionSkillSet>, ILUPositionSkillSetRepository
    {
        private readonly EPPContext _context;
        public LUPositionSkillSetRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        async public Task<IEnumerable<LUSkillSet>> GetSkillsByPosition(Guid guid)
        {
            return await _context.LUPositionSkillSet.Include(x => x.LUSkillSet)
                                .Where(entry => entry.LUPositionToApplyId == guid)
                                .Select(entry => entry.LUSkillSet).ToListAsync();
               
        }
    }
}
