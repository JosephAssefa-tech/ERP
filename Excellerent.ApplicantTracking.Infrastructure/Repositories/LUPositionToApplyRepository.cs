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
    public class LUPositionToApplyRepository : AsyncRepository<LUPositionToApply>, ILUPositionToApplyRepository
    {

        private readonly EPPContext _context;
        public LUPositionToApplyRepository(EPPContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<LUPositionToApply>> GetByPosition(string Name)
        {
            return await _context.PositionToApply.Where(x => x.Name == Name).ToListAsync();
        }
    }
}
