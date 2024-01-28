using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class LUFieldOfStudyServiceTest
    {
        private readonly LUFieldOfStudyService _fieldOfStudy;
        private readonly Mock<ILUFieldOfStudyRepository> _fieldOfStudyRepoMock = new Mock<ILUFieldOfStudyRepository>();
        public LUFieldOfStudyServiceTest()
        {
            _fieldOfStudy = new LUFieldOfStudyService(_fieldOfStudyRepoMock.Object);
        }
        [Fact]
        public async Task GetByEducationNameTest()
        {
            var guid = new Guid();
            var EducationName = "CS101";

            var resApp = new LUFieldOfStudy
            {
                Guid = guid,
                EducationName = EducationName,

            };

            _fieldOfStudyRepoMock.Setup(x => x.FindOneAsync(x => x.EducationName == EducationName).Result)
                .Returns(resApp);

            var fieldOfStudy = await (_fieldOfStudy.GetByEducationName(EducationName));
        }
    }
}
