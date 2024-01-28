using Excellerent.ClientManagement.Domain.DTOs;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.DTOs;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.Services.Helpers;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface
{
    public interface IProjectService : ICRUD<ProjectEntity, Project>
    {
        Task<IEnumerable<ProjectEntity>> GetProjectByName(string projectName);
        Task<IEnumerable<ProjectDTO>> GetAllSupervisorProjects(Guid supervisorGuid);
        Task<IEnumerable<ProjectDTO>> GetClientProjects(Guid clientGuid);
        Task<IEnumerable<ProjectEntity>> GetProjectFullData();
        Task<ResponseDTO> GetFilterMenu();
        Task<PredicatedResponseDTO> GetPaginatedProject(PaginationParams paginationParam);       
        Task<IEnumerable<ProjectEntity>> GetEmployeeProjects(Guid empId);
        Task<IEnumerable<ClientDTO>> GetEmployeeClient(Guid empId);
        Task<IEnumerable<ClientDTO>> GetProjectClient(Guid projectID);
        Task<Project> GetProjectById(Guid id);
        Task<ResponseDTO> DeleteByState(Guid id, Boolean forCheck);
        Task<ResponseDTO> GetUserPaginationOfProjects(PaginationParams paginationParams,string email);
        Task<ResponseDTO> AddProjectWithResources(ProjectEntity project,List<AssignResourceEntity> assignResourceEntities,string email);
        Task<ResponseDTO> UpdateProject(ProjectEntity project,string email);
        Task<bool> IsEmployeeSupervisor(Guid id);
        Task<ResponseDTO> GetDashbaordProjectReport();

    }
}
