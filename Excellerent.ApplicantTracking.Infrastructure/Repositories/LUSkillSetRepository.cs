using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Infrastructure.Repositories
{
    public class LUSkillSetRepository : AsyncRepository<LUSkillSet>, ILUSkillSetRepository
    {
        private readonly EPPContext _context;
        public LUSkillSetRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LUSkillSet>> GetByLUSkillSet(string SkillName)
        {
            return await _context.SkillSet.Where(x => x.SkillName == SkillName).ToListAsync();
        }
    }
}
