using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Repository
{
    public interface IApplicantRepository : IAsyncRepository<Applicant>
    {
        Task<IEnumerable<Applicant>> GetApplicantByCountry(string country);

        Task<IEnumerable<Applicant>> GetApplicantByEmail(string email);



    }

}
