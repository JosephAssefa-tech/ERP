using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Services
{
    public interface IEmergencyContactService : ICRUD<EmergencyContactsEntity, EmergencyContactsModel>
    {
        Task<EmergencyContactsModel> GetEmergencyContactByEmployee(Guid employeeguid);
        
    }
}
