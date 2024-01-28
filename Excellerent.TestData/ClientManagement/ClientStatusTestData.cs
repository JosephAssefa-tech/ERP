using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.TestData.ClientManagement
{
    public static class ClientStatusTestData
    {
        public static readonly List<ClientStatus> _sampleData = new List<ClientStatus>()
        {
            new ClientStatus()
            {
                Guid = Guid.NewGuid(),
                StatusName = "Active",
                IsDeleted = false
            },
            new ClientStatus()
            {
                Guid = Guid.NewGuid(),
                StatusName = "Inactive",
                IsDeleted = false
            },
            new ClientStatus()
            {
                Guid = Guid.NewGuid(),
                StatusName = "Terminated",
                IsDeleted = false
            }
        };

        public static async Task Clear(IClientStatusRepository repo)
        {
            IEnumerable<ClientStatus> data = await repo.GetAllAsync();
            var reply = data.Select(x => repo.DeleteAsync(x));
        }

        public static async Task Add(IClientStatusRepository repo)
        {
            IEnumerable<ClientStatus> data = await repo.GetAllAsync();
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
