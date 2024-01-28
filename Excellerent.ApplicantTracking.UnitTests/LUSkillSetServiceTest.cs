using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class LUSkillSetServiceTest
    {
        private readonly LUSkillSetService _skillSetService;
        private readonly Mock<ILUSkillSetRepository> _skillSetRepositoryRepoMock = new Mock<ILUSkillSetRepository>();
        public LUSkillSetServiceTest()
        {
            _skillSetService = new LUSkillSetService(_skillSetRepositoryRepoMock.Object);
        }
        [Fact]
        public async Task GetBySkillSetTest()
        {
            var guid = new Guid();
            var SkillSet = "CS101";

            var resApp = new LUSkillSet
            {
                Guid = guid,
                SkillName = SkillSet,

            };

            _skillSetRepositoryRepoMock.Setup(x => x.FindOneAsync(x => x.SkillName == SkillSet).Result)
                .Returns(resApp);

            var skillSet = await (_skillSetService.GetBySkillName(SkillSet));
        }
    }
}