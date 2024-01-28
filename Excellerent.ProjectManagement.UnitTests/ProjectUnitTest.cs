using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ProjectManagement.Presentation.Controllers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Excellerent.ProjectManagement.UnitTests
{
    public class ProjectUnitTest
    {
        private readonly Mock<IProjectService> status = new Mock<IProjectService>();
        private readonly List<Project> projectStatus = new List<Project>();
        private readonly ProjectController _projectController;

        public ProjectUnitTest()
        {

        }
    }
}
