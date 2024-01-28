using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface
{
    public interface IProjectModuleService : ICRUD<ProjectEntity, Project>
    {
        Task<ResponseDTO> GetProjectsByEmployeeId(Guid employeeId);
        Task<ResponseDTO> GetProjectsByClientAndEmployeeId(Guid clientId, Guid employeeId);
        Task<ResponseDTO> GetClientsAndProjectsByEmployeeId(Guid employeeId);


        Task<ResponseDTO> GetProjectsBySupervisorId(Guid supervisorId);
        Task<ResponseDTO> GetClientsAndProjectsBySupervisorId(Guid employeeId);
        Task<ResponseDTO> GetProjectsByClientAndSupervisorId(Guid clientId, Guid employeeId);


        Task<ResponseDTO> GetClientsByProjectId(Guid projectId);
    }
}
