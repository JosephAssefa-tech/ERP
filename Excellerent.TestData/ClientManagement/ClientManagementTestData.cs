using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ClientManagement
{
    public static class ClientManagementTestData
    {
        public static async Task Clear(
            IClientStatusRepository clientStatusRepository, IClientDetailsRepository clientDetailsRepository
            )
        {
            await ClientStatusTestData.Clear(clientStatusRepository);
            await ClientDetailsTestData.Clear(clientDetailsRepository);
        }

        public static async Task Add(IClientDetailsRepository clientDetailsRepository,

            IClientStatusRepository clientStatusRepository
            )
        {
            await ClientStatusTestData.Add(clientStatusRepository);
            await ClientDetailsTestData.Add(clientDetailsRepository, clientStatusRepository);
            
        }
    }
}
