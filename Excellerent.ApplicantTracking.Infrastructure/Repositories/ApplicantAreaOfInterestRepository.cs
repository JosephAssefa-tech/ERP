using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Infrastructure.Repositories
{
   public class ApplicantAreaOfInterestRepository : AsyncRepository<ApplicantAreaOfInterest>, IApplicantAreaOfInterestRepository
    {
        private readonly EPPContext _context;
        public ApplicantAreaOfInterestRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicantAreaOfInterest>> GetApplicantAreaOfInterest(Guid guid)
        {
           return await _context.ApplicantAreaOfInterest.Where(x => x.Guid == guid).ToListAsync();
            
        }

        public Task<IEnumerable<ApplicantAreaOfInterest>> GetApplicantAreaOfInterestByPosition(Guid PositionToApplyID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicantAreaOfInterest>> GetApplicantAreaOfInterestByProficiencyLevel(Guid ProficiencyLevelID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicantAreaOfInterest>> GetApplicantAreaOfInterestByYearsOfExpierence(int YearsOfExpierence)
        {
            throw new NotImplementedException();
        }
    }
}
