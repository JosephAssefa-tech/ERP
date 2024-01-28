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
    public class JobRequirmentRepository : AsyncRepository<JobRequirment>, IJobRequirmentRepository
    {
        private readonly EPPContext _context;
        public JobRequirmentRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobRequirment>> GetByJobRequirment(string JobDescriptionName)
        {
            return await _context.JobRequirment.Where(x => x.JobDescriptionName == JobDescriptionName).ToListAsync();
        }
    }
}
