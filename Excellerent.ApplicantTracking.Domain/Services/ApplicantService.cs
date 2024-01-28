using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Excellerent.ApplicantTracking.Domain.Services
{
    public class ApplicantService : CRUD<ApplicantEntity, Applicant>, IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        public ApplicantService(IApplicantRepository applicantRepository) : base(applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public async Task<Guid> AddAsync(ApplicantEntity applicantEntity)
        {
            var model = applicantEntity.MapToModel();
            var data = await _applicantRepository.AddAsync(model);
            return data.Guid;
        }

        public async Task<Guid> ApplicantSignUp(ApplicantEntity applicantEntity)
        {
            var model = applicantEntity.MapToModel();
            var existingEmail = await GetApplicantsByEmails(model.Email);
            if (existingEmail != null)
                return Guid.Empty;
            var data = await _applicantRepository.AddAsync(model);
            return data.Guid;
        }

        public async Task<ApplicantEntity> Authenticate(string Email, string Password)
        {
            var data = await _applicantRepository.FindOneAsync(x => x.Email == Email);
            return data != null ? new ApplicantEntity(data) : null;

        }

        public async Task<Guid> UpdateApplicant(ApplicantEntity applicantEntity)
        {
            var data = await _applicantRepository.FindOneAsync(x => x.Email == applicantEntity.Email);
            var model = applicantEntity.MapToModel(data);
            await _applicantRepository.UpdateAsync(model);
            return data.Guid;
        }
        public Task<IEnumerable<ApplicantEntity>> GetApplicantsByCountry(string country)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicantEntity> GetApplicantsByEmails(string email)
        {
            var data = await _applicantRepository.GetApplicantByEmail(email);
            if (data == null || data.Count() == 0)
                return null;
            return new ApplicantEntity(data.FirstOrDefault());
        }

        public Task<ResponseDTO> MockUserProfile(ApplicantEntity model)
        {
            throw new NotImplementedException();
        }

        //public string GetConnectionString()
        //{

        //    try
        //    {
        //        return _applicantRepository.GetConnectionString();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    }
}
