using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Infrastructure.Repositories
{
    public class ClientRepository : AsyncRepository<Client>, IClientRepository
    {
        private readonly EPPContext _context;
        public ClientRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Client>> GetClientByClientName(string clientName)
        {
            try
            {
                IEnumerable<Client> client = (await base.GetQueryAsync(x => x.ClientName == clientName)).ToList();
                return client;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
