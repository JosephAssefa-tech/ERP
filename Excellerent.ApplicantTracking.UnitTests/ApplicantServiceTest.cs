using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class ApplicantServiceTest
    {
        private readonly ApplicantService _ast;
        private readonly Mock<IApplicantRepository> _applicantRepoMock = new Mock<IApplicantRepository>();
        public ApplicantServiceTest()
        {
            _ast = new ApplicantService(_applicantRepoMock.Object);
        }
        [Fact]
        public async Task GetByEmailTest()
        {
            var guid = new Guid();
            var email = "ayifter@test.com";
            var applicantFirstName = "Abrham";
            var applicantLastName = "Yifter";
            var applicantCountry = "Et";
            var applicantContactNumber = "251914277222";
            var applicantProfileImage = "profilemoq";
            var applicantResumeFile = "resumefilemoq";
            var applicantStatus = "";
            var applicantIsActive = false;

            var resApp = new Applicant
            {
                Guid = guid,
                FirstName = applicantFirstName,
                LastName = applicantLastName,
                Country = applicantCountry,
                ContactNumber = applicantContactNumber,
                Status = applicantStatus,
                IsActive = applicantIsActive,
                ProfilePictureUpload = applicantProfileImage,
                ResumeUpload = applicantResumeFile
            };


            _applicantRepoMock.Setup(x => x.GetApplicantByEmail(email).Result)
                .Returns(new List<Applicant>() { resApp });

            var applicant = await (_ast.GetApplicantsByEmails(email));
            Assert.Equal(resApp.Email, applicant.Email);
            
        }
    }
}
