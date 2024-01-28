using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ClientManagement
{
    public static  class ClientDetailsTestData
    {


        public static async Task Clear(IClientDetailsRepository repo)
        {
            IEnumerable<ClientDetails> data = await repo.GetAllAsync();
            var reply = data.Select(x => repo.DeleteAsync(x));
        }

        public static async Task Add(IClientDetailsRepository repo, IClientStatusRepository repStaus)
        {
            var activeStatus = await  repStaus.FindOneAsync(c => c.StatusName == "Active");
            if (activeStatus != null)
            {
                ClientDetails _internalClient = new ClientDetails()
            {
                Guid = Guid.NewGuid(),
                SalesPersonGuid = Guid.NewGuid(),
                ClientName = "Internal",
                ClientStatusGuid = activeStatus.Guid,
                Description = ""

            };
                await repo.AddAsync(_internalClient);

                ClientDetails LeaveClient = new ClientDetails()
                {
                    Guid = Guid.NewGuid(),
                    SalesPersonGuid = Guid.NewGuid(),
                    ClientName = "Leave",
                    ClientStatusGuid = activeStatus.Guid,
                    Description = ""

                };
                await repo.AddAsync(LeaveClient) ;


            }
           
        }
        }
}
