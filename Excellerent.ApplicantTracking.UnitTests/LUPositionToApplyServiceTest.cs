using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{
    public class LUPositionToApplyServiceTest
    {
        private readonly LUPositionToApplyService _positionToApply;
        private readonly Mock<ILUPositionToApplyRepository> _positionToApplyRepoMock = new Mock<ILUPositionToApplyRepository>();
        public LUPositionToApplyServiceTest()
        {
            _positionToApply = new LUPositionToApplyService(_positionToApplyRepoMock.Object);
        }
        [Fact]
        public async Task GetByPositionNameTest()
        {
            var guid = new Guid();
            var PositionToApply = "CS101";

            var resApp = new LUPositionToApply
            {
                Guid = guid,
                Name = PositionToApply,

            };

            _positionToApplyRepoMock.Setup(x => x.FindOneAsync(x => x.Name == PositionToApply).Result)
                .Returns(resApp);

            var fieldOfStudy = await (_positionToApply.GetByPositionName(PositionToApply));
        }
    }
}
