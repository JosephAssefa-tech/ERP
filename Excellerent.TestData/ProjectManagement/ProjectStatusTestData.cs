using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ProjectManagement
{
    public static class ProjectStatusTestData
    {
        public static readonly List<ProjectStatus> _sampleData = new List<ProjectStatus>()
        {
            new ProjectStatus()
            {
                Guid = Guid.NewGuid(),
                StatusName = "Active",
                AllowResource = true,
                IsDeleted = false,
            },
            new ProjectStatus()
            {
                Guid = Guid.NewGuid(),
                StatusName = "Inactive",
                AllowResource = false,
                IsDeleted = false,
            },
            new ProjectStatus()
            {
                Guid = Guid.NewGuid(),
                StatusName = "Terminated",
                AllowResource = false,
                IsDeleted = false,
            },
            new ProjectStatus()
            {
                Guid = Guid.NewGuid(),
                StatusName = "Finished",
                AllowResource = false,
                IsDeleted = false,
            },
        };

        public static async Task Clear(IProjectStatusRepository repo)
        {
            IEnumerable<ProjectStatus> data = await repo.GetAllAsync();
            var reply = data.Select(x => repo.DeleteAsync(x));
        }

        public static async Task Add(IProjectStatusRepository repo)
        {
            IEnumerable<ProjectStatus> data = await repo.GetAllAsync();
            for (int i = 0; i < _sampleData.Count; i++)
            {
                Guid guid;
                var dataIn = data.Where(x => x.StatusName.Equals(_sampleData[i].StatusName));
                if (dataIn.Count() == 0)
                {
                    guid = Guid.NewGuid();

                    _sampleData[i].Guid = guid;
                    _sampleData[i] = await repo.AddAsync(_sampleData[i]);
                }
                else
                {
                    guid = dataIn.FirstOrDefault().Guid;
                    _sampleData[i] = dataIn.FirstOrDefault();
                }

            }
        }
    }
}
