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

    public class LUFieldOfStudyRepository : AsyncRepository<LUFieldOfStudy>, ILUFieldOfStudyRepository
    {

        private readonly EPPContext _context;
        public LUFieldOfStudyRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LUFieldOfStudy>> GetByFieldOfStudy(string educationName)
        {
            return await _context.FieldOfStudie.Where(x => x.EducationName == educationName).ToListAsync();
        }

    }
}