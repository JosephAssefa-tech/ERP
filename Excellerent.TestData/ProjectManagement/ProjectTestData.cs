using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Infrastructure.Repositories;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ProjectManagement
{
    public static class ProjectTestData
    {

        public static async Task Add(IClientDetailsRepository clientDetailsRepository, IProjectRepository projectRepostery , IProjectStatusRepository projectStatusRepository)
        {
            var leaveClient = await clientDetailsRepository.GetClientByName("Leave");
            var activeSatatus = await projectStatusRepository.FindOneAsync(s => s.StatusName == "Active");

              if(leaveClient != null && activeSatatus!=null)
            {
           
                 Project projectCasualLeave = new Project()
                  {
                    Guid = Guid.NewGuid(),
                    ProjectStatusGuid = activeSatatus.Guid,
                    ClientGuid=leaveClient.First().Guid,
                    SupervisorGuid = Guid.NewGuid(),
                    ProjectName = "Casual Leave",
                    StartDate = new DateTime(2000, 1,1)
                   };
                await projectRepostery.AddAsync(projectCasualLeave);

                Project projectMedical = new Project()
                {
                    Guid = Guid.NewGuid(),
                    ProjectStatusGuid = activeSatatus.Guid,
                    ClientGuid = leaveClient.First().Guid,
                    SupervisorGuid = Guid.NewGuid(),
                    ProjectName = "Medical/Maternity",
                    StartDate = new DateTime(2000, 1, 1)
                };

                await projectRepostery.AddAsync(projectMedical);

                Project projectVacation = new Project()
                {
                    Guid = Guid.NewGuid(),
                    ProjectStatusGuid = activeSatatus.Guid,
                    ClientGuid = leaveClient.First().Guid,
                    SupervisorGuid = Guid.NewGuid(),
                    ProjectName = "Vacation",
                    StartDate = new DateTime(2000, 1, 1)
                };
                await projectRepostery.AddAsync(projectVacation);

                Project projectSickLeave = new Project()
                {
                    Guid = Guid.NewGuid(),
                    ProjectStatusGuid = activeSatatus.Guid,
                    ClientGuid = leaveClient.First().Guid,
                    SupervisorGuid = Guid.NewGuid(),
                    ProjectName = "Sick Leave",
                    StartDate = new DateTime(2000, 1, 1)
                };

               await projectRepostery.AddAsync(projectSickLeave);
            }
        }
    
    }
}
