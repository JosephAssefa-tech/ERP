using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface
{
    public interface IClientService : ICRUD<ClientEntity, Client>
    {
        Task<IEnumerable<ClientEntity>> GetClientByClientName(string clientName);
        Task<Client> GetClientByGuid(Guid guid);
    }
}
