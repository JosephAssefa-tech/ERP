using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Services
{
    public class JobRequirmentService : CRUD<JobRequirmentEntity, JobRequirment>, IJobRequirmentService
    {
        private readonly IJobRequirmentRepository _jobRequirmentRepo;
        public JobRequirmentService(IJobRequirmentRepository jobRequirmentRepo) : base(jobRequirmentRepo)
        {
            _jobRequirmentRepo = jobRequirmentRepo;
        }

        public async Task<Guid> AddJobRequirment(JobRequirmentEntity JobRequirmentEntity)
        {
            var model = JobRequirmentEntity.MapToModel();
            var data = await _jobRequirmentRepo.AddAsync(model);
            return data.Guid;
        }

        public async Task<JobRequirmentEntity> GetByJobRequirment(string JobDescriptionName)
        {
            var data = await _jobRequirmentRepo.FindOneAsync(x => x.JobDescriptionName == JobDescriptionName);
            return data != null ? new JobRequirmentEntity(data) : null;
        }

        public async Task<IEnumerable<JobRequirmentEntity>> GetAllJobRequirmentEntity()
        {
            var data = await _jobRequirmentRepo.GetAllAsync();
            return data?.Select(x => new JobRequirmentEntity(x));
        }
    }
}
