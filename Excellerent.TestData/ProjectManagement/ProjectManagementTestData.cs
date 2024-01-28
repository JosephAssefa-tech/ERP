using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ProjectManagement
{
    public static class ProjectManagementTestData
    {
        public static async Task Clear(
               IProjectStatusRepository projectStatusRepository
               )
        {
            await ProjectStatusTestData.Clear(projectStatusRepository);
        }

        public static async Task Add(
            IProjectStatusRepository projectStatusRepository , IClientDetailsRepository clientRepository , IProjectRepository projectRepository

            )
        {
            await ProjectStatusTestData.Add(projectStatusRepository);
            await ProjectTestData.Add(clientRepository, projectRepository, projectStatusRepository);
        }
    }
}
