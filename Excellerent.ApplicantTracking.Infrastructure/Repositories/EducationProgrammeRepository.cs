using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Infrastructure.Repositories
{
    public class EducationProgrammeRepository : AsyncRepository<LUEducationProgram>, IEducationProgrammeRepository
    {
        private readonly EPPContext _context;
        public EducationProgrammeRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
        public async Task<LUEducationProgram> GetByName(string name)
        {
            try
            {
                return (await base.FindOneAsync(x => x.Name == name));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
