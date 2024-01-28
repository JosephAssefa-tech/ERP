using Excellerent.ClientManagement.Domain.DTOs;
using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.DTOs;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace Excellerent.ProjectManagement.Domain.Services
{
    //public class ClientDTO : BaseAuditModel
    //{
    //    public string ClientName { get; set; }
    //    public IEnumerable<ProjectDTO>? Projects { get; set; }
    //    public ClientDTO(ClientDetailsEntity entity)
    //    {
    //        Guid = entity.Guid;
    //        IsActive = entity.IsActive;
    //        IsDeleted = entity.IsDeleted;
    //        CreatedDate = entity.CreatedDate;
    //        CreatedbyUserGuid = entity.CreatedbyUserGuid;
    //        ClientName = entity.ClientName;
    //    }
    //}
    public class ProjectModuleService :
        CRUD<ProjectEntity, Project>,
        IProjectModuleService
    {
        protected readonly IClientDetailsRepository _clientRepository;
        protected readonly IAssignResourceRepository _assignResourceRepository;
        protected readonly IProjectRepository _projectRepository;

        public ProjectModuleService(
            IClientDetailsRepository clientRepository,
            IAssignResourceRepository assignResourceRepository,
            IProjectRepository projectRepository
            ) : base(projectRepository)
        {
            _clientRepository = clientRepository;
            _assignResourceRepository = assignResourceRepository;
            _projectRepository = projectRepository;
        }

        ////////////////////////////////////////////////////////////////////////////

        private async Task<IEnumerable<Guid>> GetProjectsGuidByEmployeeId(Guid employeeId)
        {
            return (await _assignResourceRepository.GetAllAsync()).Where(x => x.EmployeeGuid == employeeId && x.IsDeleted==false).Select(x => x.ProjectGuid);
        }

        private async Task<IEnumerable<ProjectEntity>> _GetProjectsBySupervisorId(Guid employeeId)
        {
            return (await _projectRepository.GetAllAsync()).Where(x => x.SupervisorGuid == employeeId).Select(x => new ProjectEntity(x));
        }

        private async Task<IEnumerable<ProjectEntity>> GetProjectsByIds(IEnumerable<Guid> ids,Guid? empId)
        {
            List<ProjectEntity> projects = new List<ProjectEntity>();
            foreach (Guid id in ids)
            {
                projects.Add(await _projectRepository.GetByProjectGuidAsync(id, empId));
            }
            return projects;
        }

        private async Task<IEnumerable<ClientDetailsEntity>> GetClientsByIds(IEnumerable<Guid> ids)
        {
            List<ClientDetailsEntity> clients = new List<ClientDetailsEntity>();
            foreach (Guid id in ids)
            {
                clients.Add(new ClientDetailsEntity(await _clientRepository.GetByGuidAsync(id)));
            }
            return clients;
        }

        ////////////////////////////////////////////////////////////////////////////

        private IEnumerable<ProjectDTO> MapProjectsToDTO(IEnumerable<ProjectEntity> projects)
        {
            return projects.Select(x => new ProjectDTO(x));
        }

        private IEnumerable<ClientDTO> MapClientsToDTO(IEnumerable<ClientDetailsEntity> clients, IEnumerable<ProjectEntity>? projects)
        {
            List<ClientDTO> clientDTOs = new List<ClientDTO>();
            foreach(ClientDetailsEntity client in clients)
            {
                IEnumerable<ProjectDTO> projectDTOs = new List<ProjectDTO>();
                if(projects != null)
                {
                    projectDTOs = MapProjectsToDTO(projects.Where(x => x.ClientGuid == client.Guid));
                }
                clientDTOs.Add(new ClientDTO(client, projectDTOs));
            }
            return clientDTOs;
        }

        ////////////////////////////////////////////////////////////////////////////

        private IEnumerable<Guid> FilterUniqueGuids(IEnumerable<Guid> ids)
        {
            List<Guid> uniqueGuids = new List<Guid>();
            foreach(Guid id in ids)
            {
                if(uniqueGuids.Where(x => x.Equals(id)).Count<Guid>() == 0)
                {
                    uniqueGuids.Add(id);
                }
            }
            return uniqueGuids;
        }

        private IEnumerable<ProjectEntity> FilterProjectsByClientId(IEnumerable<ProjectEntity> projects, Guid clientId)
        {
            return projects.Where(x => x.ClientGuid == clientId);
        }

        ////////////////////////////////////////////////////////////////////////////
        
        private async Task<IEnumerable<ProjectEntity>> _GetProjectsByEmployeeId(Guid employeeId)
        {
            IEnumerable<Guid> projectIds = await GetProjectsGuidByEmployeeId(employeeId);

            projectIds = FilterUniqueGuids(projectIds);
            IEnumerable<ProjectEntity> empProjects = await GetProjectsByIds(projectIds, employeeId);

            return empProjects;
        }

        private async Task<IEnumerable<ProjectEntity>> _GetProjectsByClientAndEmployeeId(Guid clientId, Guid employeeId)
        {
            return FilterProjectsByClientId(await _GetProjectsByEmployeeId(employeeId), clientId);
        }

        private async Task<IEnumerable<ProjectEntity>> _GetProjectsByClientAndSupervisorId(Guid clientId, Guid employeeId)
        {
            return FilterProjectsByClientId(await _GetProjectsBySupervisorId(employeeId), clientId);
        }

        private async Task<ClientDTO> _GetClientByProjectId(Guid projectId)
        {
            IEnumerable<ProjectEntity> projects = await GetProjectsByIds(new List<Guid>() { projectId },null);
            IEnumerable<Guid> clientsId = projects.Select(x => x.ClientGuid);
            clientsId = FilterUniqueGuids(clientsId);
            IEnumerable<ClientDetailsEntity> clients = await GetClientsByIds(clientsId);
            return MapClientsToDTO(clients, null).FirstOrDefault();
        }

        private async Task<IEnumerable<ClientDTO>> _GetClientsAndProjectsByEmployeeId(Guid employeeId)
        {
            IEnumerable<ProjectEntity> projects = await _GetProjectsByEmployeeId(employeeId);
            IEnumerable<Guid> clientsId = projects.Select(x => x.ClientGuid);
            clientsId = FilterUniqueGuids(clientsId);
            IEnumerable<ClientDetailsEntity> clients = await GetClientsByIds(clientsId);
            return MapClientsToDTO(clients, projects);
        }

        private async Task<IEnumerable<ClientDTO>> _GetClientsAndProjectsBySupervisorId(Guid employeeId)
        {
            IEnumerable<ProjectEntity> projects = await _GetProjectsBySupervisorId(employeeId);
            IEnumerable<Guid> clientsId = projects.Select(x => x.ClientGuid);
            clientsId = FilterUniqueGuids(clientsId);
            IEnumerable<ClientDetailsEntity> clients = await GetClientsByIds(clientsId);
            return MapClientsToDTO(clients, projects);
        }


        ///////////////////////////////////////////////////////////////////////////

        public async Task<ResponseDTO> GetProjectsByEmployeeId(Guid employeeId)
        {
            return new ResponseDTO(ResponseStatus.Success, "",
                MapProjectsToDTO(await _GetProjectsByEmployeeId(employeeId))
                );
        }

        public async Task<ResponseDTO> GetProjectsByClientAndEmployeeId(Guid clientId, Guid employeeId)
        {
            return new ResponseDTO(ResponseStatus.Success, "",
                MapProjectsToDTO(await _GetProjectsByClientAndEmployeeId(clientId, employeeId))
                );
        }
        public async Task<ResponseDTO> GetClientsAndProjectsByEmployeeId(Guid employeeId)
        {
            return new ResponseDTO(
                ResponseStatus.Success,
                "",
                (await _GetClientsAndProjectsByEmployeeId(employeeId))
                );
        }


        public async Task<ResponseDTO> GetProjectsBySupervisorId(Guid supervisorId)
        {
            return new ResponseDTO(ResponseStatus.Success, "",
                MapProjectsToDTO(await _GetProjectsBySupervisorId(supervisorId))
                );
        }
        public async Task<ResponseDTO> GetClientsAndProjectsBySupervisorId(Guid supervisorId)
        {
            return new ResponseDTO(ResponseStatus.Success, "",
                await _GetClientsAndProjectsBySupervisorId(supervisorId)
                );
        }
        public async Task<ResponseDTO> GetProjectsByClientAndSupervisorId(Guid clientId, Guid employeeId)
        {
            return new ResponseDTO(ResponseStatus.Success, "",
                MapProjectsToDTO(await _GetProjectsByClientAndSupervisorId(clientId, employeeId))
                );
        }


        public async Task<ResponseDTO> GetClientsByProjectId(Guid projectId)
        {
            return new ResponseDTO(ResponseStatus.Success, "",
                await _GetClientByProjectId(projectId)
                );
        }
    }
}
