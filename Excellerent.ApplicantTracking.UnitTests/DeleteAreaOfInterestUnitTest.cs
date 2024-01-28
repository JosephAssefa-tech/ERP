using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
   public class DeleteAreaOfInterestUnitTest
    {
        [Fact]
        public void  DeleteApplicantAreaOfInterst() 
        {
            //Arrange

            var mockRepository = new Mock<IApplicantAreaOfInterestRepository>(); 
            var areaOfInterstService = new ApplicantAreaOfInterestService(mockRepository.Object);
            
            var guid = new Guid();//"dc8a5261-5690-4bfc-af82-b6025f890bdc";

            var resAppAOInterst = new ApplicantAreaOfInterest
            {
                Guid = guid
            };

            mockRepository.Setup(x => x.FindOneAsync(x => x.Guid == guid).Result)
                .Returns(resAppAOInterst);

            var applicantAOInterest = areaOfInterstService.DeleteApplicantAreaOfInterst(guid);
            Assert.Equal(applicantAOInterest.Result, guid);
        }
    }
}
