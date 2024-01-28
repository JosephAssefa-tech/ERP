using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class JobRequirmentServiceTest
    {
        private readonly JobRequirmentService _jobRequirmentService;
        private readonly Mock<IJobRequirmentRepository> _jobRequirmentRepoMock = new Mock<IJobRequirmentRepository>();
        public JobRequirmentServiceTest()
        {
            _jobRequirmentService = new JobRequirmentService(_jobRequirmentRepoMock.Object);
        }
        [Fact]
        public async Task GetByJobRequirmentTest()
        {
            var guid = new Guid();
            var JobDescriptionName = "CS101";
            var JobBrief = "Test 102";
            var JobResponsiblity = "Test 101";

            var resApp = new JobRequirment
            {
                Guid = guid,
                JobDescriptionName = JobDescriptionName,
                JobBrief = JobBrief,
                JobResponsiblity = JobResponsiblity

            };

            _jobRequirmentRepoMock.Setup(x => x.FindOneAsync(x => x.JobDescriptionName == JobDescriptionName).Result)
                .Returns(resApp);

            var skillSet = await (_jobRequirmentService.GetByJobRequirment(JobDescriptionName));
        }
    }
}