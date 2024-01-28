using Excellerent.ProjectManagement.Domain.DTOs;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Services
{
    public class AssignResourceService : CRUD<AssignResourceEntity, AssignResourcEntity>, IAssignResourceService
    {
        private readonly IAssignResourceRepository _repository;
        public AssignResourceService(IAssignResourceRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public Task<AssignResourcEntity> GetOneAssignResource(Guid id)
        {
            return _repository.GetByGuidAsync(id);
        }

        public Task<IEnumerable<AssignResourcEntity>> GetProjectIdsByEmployee(Guid empId)
        {

            return _repository.GetProjectIdsByEmployee(empId);
            
        }

      
        public async  Task<ResponseDTO> GetAssignResourceByProject(Guid projectGuid)
        {
            try
            {
                IEnumerable< AssignResourcEntity > projectresource=  await _repository.GetAssignResourceByProject(projectGuid);

                return new ResponseDTO
                {
                    Data = projectresource.Select(p => new ProjectAssignedResourceDto(p)).ToList(),
                    Message = "project's resourses",
                    Ex = null,
                    ResponseStatus = ResponseStatus.Success
                };

            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Data = null,
                    Message = null,
                    Ex = null,
                    ResponseStatus = ResponseStatus.Error
                };

            }

        }
        public async Task<bool> GetAssignmentStatus(Guid id)
        {
            int d= (await _repository.FindAsync(x => x.EmployeeGuid == id)).Count();
            return d > 4 ? true : false;
        }

        public async Task<ResponseDTO> UnassginResourceOfProject(Guid id)
        {

            try
            {
                var assginedResource = await this._repository.CountAsync(a => a.Guid == id);
                if (assginedResource == 0)
                    return new ResponseDTO
                    {
                        Data = null,
                        Message = "Ivalid Input",
                        Ex = null
                    };
                else
                {
                    await this._repository.UnassginResourceOfProject(id);
                    return new ResponseDTO
                    {
                        Data = null,
                        Message = "project's resourses unassigned  successfully",
                        Ex = null,
                        ResponseStatus = ResponseStatus.Success
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Data = null,
                    Message = null,
                    Ex = null,
                    ResponseStatus = ResponseStatus.Error
                };

            }

        }
        public async Task<ResponseDTO> AssginResourceForProject(AssignResourceEntity assignResourceEntity)
        {
            try
            {

                var resource = await _repository.FindOneAsync(r => r.EmployeeGuid == assignResourceEntity.EmployeeGuid &&
                                                 r.ProjectGuid == assignResourceEntity.ProjectGuid);

                if (resource == null)
                    return await this.Add(assignResourceEntity);
                else if (resource.IsDeleted == true)
                {
                    await this._repository.ReAssginResourceOfProject(resource.Guid);
                    return new ResponseDTO
                    {
                        Data = null,
                        Message = "Resource reassigned to project ",
                        Ex = null,
                        ResponseStatus = ResponseStatus.Success
                    };
                }

                else
                {
                    return new ResponseDTO
                    {
                        Data = null,
                        Message = "Resource already assigned to project ",
                        Ex = null,
                        ResponseStatus = ResponseStatus.Error
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Data = null,
                    Message = null,
                    Ex = null,
                    ResponseStatus = ResponseStatus.Error
                };


            }
        }
    }
}
