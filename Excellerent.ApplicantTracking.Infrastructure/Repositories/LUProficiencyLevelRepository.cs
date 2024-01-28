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

    public class LUProficiencyLevelRepository : AsyncRepository<LUProficiencyLevel>, ILUProficiencyLevelRepository
    {

        private readonly EPPContext _context;
        public LUProficiencyLevelRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LUProficiencyLevel>> GetByProficiencyLevel(string Name)
        {
            return await _context.ProficiencyLevel.Where(x => x.Name == Name).ToListAsync();
        }
    }
}
