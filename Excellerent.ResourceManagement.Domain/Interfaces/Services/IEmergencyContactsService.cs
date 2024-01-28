using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Services
{
    public interface IEmergencyContactsService : ICRUD<EmergencyContactsEntity, EmergencyContactsModel>
    {
        Task<bool> DeleteEmergencyContact(string email);


    }
}
