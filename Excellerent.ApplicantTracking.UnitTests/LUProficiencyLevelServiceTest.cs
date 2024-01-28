using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class LUProficiencyLevelServiceTest
    {
        private readonly LUProficiencyLevelService _proficencylevel;
        private readonly Mock<ILUProficiencyLevelRepository> _proficencylevelRepoMock = new Mock<ILUProficiencyLevelRepository>();
        public LUProficiencyLevelServiceTest()
        {
            _proficencylevel = new LUProficiencyLevelService(_proficencylevelRepoMock.Object);
        }
        [Fact]
        public async Task GetbYProficiencyLevelTest()
        {
            var guid = new Guid();
            var ProficiencyLevel = "CS101";

            var resApp = new LUProficiencyLevel
            {
                Guid = guid,
                Name = ProficiencyLevel,

            };

            _proficencylevelRepoMock.Setup(x => x.FindOneAsync(x => x.Name == ProficiencyLevel).Result)
                .Returns(resApp);

            var fieldOfStudy = await (_proficencylevel.GetByProficiencyName(ProficiencyLevel));
        }
    }
}
