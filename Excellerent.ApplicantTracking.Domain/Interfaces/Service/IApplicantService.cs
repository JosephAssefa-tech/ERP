using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Service
{
    public interface IApplicantService : ICRUD<ApplicantEntity, Applicant>
    {
        Task<ApplicantEntity> Authenticate(string Email, string Password);
        Task<IEnumerable<ApplicantEntity>> GetApplicantsByCountry(string country);
        Task<ApplicantEntity> GetApplicantsByEmails(string email);
        Task<ResponseDTO> MockUserProfile(ApplicantEntity model);
        Task<Guid> ApplicantSignUp(ApplicantEntity model);
        Task<Guid> UpdateApplicant(ApplicantEntity model);

    }
}
