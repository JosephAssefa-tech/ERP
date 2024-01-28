using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ApplicantTracking.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ApplicantTracking.UnitTests
{

    public class EducationProgramTest
    {
            private readonly EducationProgrammeService _educationProgramService;
            private readonly Mock<IEducationProgrammeRepository> _educationProgramRepositoryRepoMock = new Mock<IEducationProgrammeRepository>();
            public EducationProgramTest()
            {
                _educationProgramService = new EducationProgrammeService(_educationProgramRepositoryRepoMock.Object);
            }
            [Fact]
            public async Task GetByEducationProgrammeTest()
            {
                var guid = new Guid();
                var Name = "CS101";

                var resApp = new LUEducationProgram
                {
                    Guid = guid,
                    Name = Name,

                };

                _educationProgramRepositoryRepoMock.Setup(x => x.FindOneAsync(x => x.Name == Name).Result)
                    .Returns(resApp);

            }
        }
    }

