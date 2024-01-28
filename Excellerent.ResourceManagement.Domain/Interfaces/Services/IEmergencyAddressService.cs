using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Services
{
    public interface IEmergencyAddressService : ICRUD<EmergencyAddressEntity, EmergencyAddress>
    {
    }
}
