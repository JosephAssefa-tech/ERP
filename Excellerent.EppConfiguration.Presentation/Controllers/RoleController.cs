using Excellerent.EppConfiguration.Domain.Entities;
using Excellerent.EppConfiguration.Domain.Interfaces.Service;
using Excellerent.EppConfiguration.Presentation.Dtos;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.UserManagement.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpGet("Get")]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Job_Title" })]
        public async Task<ResponseDTO> Get(Guid id)
        {
            return await this._roleService.Get(id);
        }

        [HttpPost]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Create_Job_Title" })]
        public async Task<ResponseDTO> Add(RolePostDto roleDto)
        {
            roleDto.Name = roleDto.Name.Trim();
            if (await _roleService.CheckIfJobTitleExist(roleDto.Name, roleDto.DepartmentGuid))
            {
                return new ResponseDTO(ResponseStatus.Error, "Job title already exist", roleDto.Name);
            }
            return await _roleService.Add(roleDto.MapToEntity());
        }

        [HttpPut]
       [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Update_Job_Title" })]
        public async Task<ResponseDTO> Update(RoleEntity roleEntity)
        {
            roleEntity.Name = roleEntity.Name.Trim();
            if (await _roleService.CheckIfJobTitleExist(roleEntity.Name, roleEntity.DepartmentGuid))
            {
                return new ResponseDTO(ResponseStatus.Error, "Job title already exist", roleEntity.Name);
            }
            await _roleService.Update(roleEntity);
            return new ResponseDTO(ResponseStatus.Success, "Updated Successfully", roleEntity);
        }

        [HttpGet("GetDepartmentRoles")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetRolesByDepartment(string departmentGuid)
        {
            return await _roleService.GetRolesByDepartment(Guid.Parse(departmentGuid));
        }

        [HttpGet]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Job_Title" })]
        public async Task<PredicatedResponseDTO> GetPaginated(string searchKey, int? pageIndex, int? pageSize, string sortBy, SortOrder? sortOrder)
        {
            if (!String.IsNullOrEmpty(searchKey))
                searchKey = searchKey.Trim();
            return await _roleService.GetWithPredicate(searchKey, pageIndex, pageSize, sortBy, sortOrder);

        }

        [HttpDelete]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Job_Title" })]
        public async Task<ResponseDTO> Delete(Guid id)
        {
            var d = await _roleService.FindOneAsyncForDelete(id);
            return await _roleService.Delete(d == null ? null : new RoleEntity(d));
        }
    }
}
