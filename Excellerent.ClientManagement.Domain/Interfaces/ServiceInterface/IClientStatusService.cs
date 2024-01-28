using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface
{
    public interface IClientStatusService : ICRUD<ClientStatusEntity, ClientStatus>
    {
        Task<ClientStatus> GetClientStatusById(Guid id);
    }
}