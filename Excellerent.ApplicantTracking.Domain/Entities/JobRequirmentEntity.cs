using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ApplicantTracking.Domain.Entities
{
    public class JobRequirmentEntity : BaseEntity<JobRequirment>
    {
        public string JobDescriptionName { get; set; }
        public string JobBrief { get; set; }
        public string JobResponsiblity { get; set; }

        public JobRequirmentEntity() { }

            public JobRequirmentEntity(JobRequirment jobReq) : base(jobReq)
            {
                Guid = jobReq.Guid;
                CreatedDate = jobReq.CreatedDate;
                JobDescriptionName = jobReq.JobDescriptionName;
                JobBrief = jobReq.JobBrief;
                JobResponsiblity = jobReq.JobResponsiblity;
            
            }
        public override JobRequirment MapToModel()
        {
            JobRequirment jobRequirment = new JobRequirment();
            jobRequirment.Guid = Guid.NewGuid();
            jobRequirment.JobDescriptionName = JobDescriptionName;
            jobRequirment.JobBrief = JobBrief;
            jobRequirment.JobResponsiblity = JobResponsiblity;
            jobRequirment.IsActive = IsActive;
            jobRequirment.IsDeleted = IsDeleted;

            return jobRequirment;
        }

        public override JobRequirment MapToModel(JobRequirment t)
        {
            JobRequirment jobRequirmentToUpdate = t;
            jobRequirmentToUpdate.JobDescriptionName = JobDescriptionName;
            jobRequirmentToUpdate.JobBrief = JobBrief;
            jobRequirmentToUpdate.CreatedDate = CreatedDate;
            jobRequirmentToUpdate.JobResponsiblity = JobResponsiblity;
            jobRequirmentToUpdate.IsActive = IsActive;
            jobRequirmentToUpdate.IsDeleted = IsDeleted;
            return jobRequirmentToUpdate;
        }

    }
}
