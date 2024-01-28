using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class AreaOfInterestServiceTest
    {
        private readonly ApplicantAreaOfInterestService _applicantAreaOfInterestService; 
        private readonly Mock<IApplicantAreaOfInterestRepository> _applicantAreaOfInterstRepoMock = new Mock<IApplicantAreaOfInterestRepository>();
        public AreaOfInterestServiceTest() 
        {
            _applicantAreaOfInterestService = new ApplicantAreaOfInterestService(_applicantAreaOfInterstRepoMock.Object);
        }
        [Fact]
        public async Task GetAreaOfInterestByApplicantID()
        {
            var guid = new Guid();//"dc8a5261-5690-4bfc-af82-b6025f890bdc";
            var PositionToApplyID = new Guid();// "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            var ProficiencyLevelID = new Guid();// "3fa85f64-5714-4562-b3fc-2c963f66afa6";
            var YearsOfExpierence = 4;
            var MonthOfExpierence = 2;
            var ApplicantId = new Guid(); // "6b6e20e9-2eb5-4b65-a28f-80bd341d1a41";//new Guid();
            string[] SelectedIDArray  = { "3fa85f64-8717-4562-b3fc-2c963f66afa6" };
            string[] SelectedIDSecondArray = { "3fa85f64-1717-4562-b3fc-2c963f66afa6" };
            string[] SelectedIDOtherArray = {"four","12" };

            var resApp = new ApplicantAreaOfInterest
            {
                Guid = guid,
                PositionToApplyID = PositionToApplyID,
                ProficiencyLevelID = ProficiencyLevelID,
                YearsOfExpierence = YearsOfExpierence,
                MonthOfExpierence = MonthOfExpierence,
                PrimarySkillSetID = string.Join(",", SelectedIDArray),
                SecondarySkillSetID = string.Join(",", SelectedIDSecondArray),
                OtherSkillSet = string.Join(",", SelectedIDOtherArray),
                ApplicantId = ApplicantId
            };

            _applicantAreaOfInterstRepoMock.Setup(x => x.FindOneAsync(x => x.YearsOfExpierence == YearsOfExpierence).Result)
                .Returns(resApp);

            var applicantAOInterest = await (_applicantAreaOfInterestService.GetApplicantAreaOfInterest(guid));
            Assert.Equal(4, resApp.YearsOfExpierence);
        }
    }
}
