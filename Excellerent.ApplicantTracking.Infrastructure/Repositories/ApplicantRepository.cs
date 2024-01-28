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
    public class ApplicantRepository : AsyncRepository<Applicant>, IApplicantRepository
    {
        private readonly EPPContext _context;
        public ApplicantRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Applicant>> GetApplicantByCountry(string country)
        {
            try
            {
                IEnumerable<Applicant> applicant = (await base.GetQueryAsync(x => x.Country == country)).ToList();
                return applicant;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Applicant>> Authenticate(string email, string password)
        {
            try
            {
                IEnumerable<Applicant> applicant = (await base.GetQueryAsync(x => x.Email == email && x.Password == password)).ToList();
                return applicant;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Applicant>> GetApplicantByEmail(string Email)
        {
            return await _context.Applicants.Where(x => x.Email == Email).ToListAsync();
        }
    }
}
