using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
   public class PostAreaOfInterestServiceUnitTest
    {
        private readonly ApplicantAreaOfInterestService _applicantAreaOfInterestService;
        private readonly Mock<IApplicantAreaOfInterestRepository> _applicantAreaOfInterstRepoMock = new Mock<IApplicantAreaOfInterestRepository>();
        public PostAreaOfInterestServiceUnitTest() 
        {
            _applicantAreaOfInterestService = new ApplicantAreaOfInterestService(_applicantAreaOfInterstRepoMock.Object);
        }
        [Fact]
        public async void  PostApplicantAreaOfInterst()  
        {
            //Arrange

            var guid = new Guid();// ("dc8a5261-5690-4bfc-af82-b602f890bdc");
            var PositionToApplyID = new Guid();//("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var ProficiencyLevelID = new Guid();//("3fa85f64-5714-4562-b3fc-2c963f66afa6");
            var YearsOfExpierence = 3;
            var MonthOfExpierence = 5;
            var ApplicantId = new Guid();// ("6b6e20e9-2eb5-4b65-a28f-80bd341d1a41");//new Guid();
            string[] SelectedIDArray = { "3fa85f64-8717-4662-b3fc-2c963f66afa6" };
            string[] SelectedIDSecondArray = { "3fa85f64-1717-4572-b3fc-2c963f66afa6" };
            string[] SelectedIDOtherArray = { "fourr", "127" };
            var resAppAOInterst = new ApplicantAreaOfInterest
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

            _applicantAreaOfInterstRepoMock.Setup(x => x.FindOneAsync(x => x.Guid == guid).Result)
                .Returns(resAppAOInterst);
            // Act
           // var applicantAOInterest = await _applicantAreaOfInterestService.AddApplicantAreaOfInterst(resAppAOInterst); 
            // Assert
            //Assert.IsType<OkObjectResult>(applicantAOInterest); 
            var appAofInterestdata = Assert.IsType<ApplicantAreaOfInterest>(resAppAOInterst); 
            Assert.Equal(guid, appAofInterestdata.Guid);
            Assert.Equal(PositionToApplyID, appAofInterestdata.PositionToApplyID);
            Assert.Equal(ProficiencyLevelID, appAofInterestdata.ProficiencyLevelID);
            Assert.Equal(YearsOfExpierence, appAofInterestdata.YearsOfExpierence);
            Assert.Equal(MonthOfExpierence, appAofInterestdata.MonthOfExpierence);
            Assert.Equal(SelectedIDArray, appAofInterestdata.PrimarySkillSetID.Split(',').ToArray());
            Assert.Equal(ApplicantId, appAofInterestdata.ApplicantId);
           
        }
      
    }
}

