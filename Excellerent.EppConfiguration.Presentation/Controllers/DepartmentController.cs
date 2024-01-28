using Excellerent.APIModularization.Controllers;
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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        [HttpGet("Get")]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Department" })]
        public async Task<ResponseDTO> Get(Guid id)
        {
            return await this._departmentService.Get(id);
        }

        [HttpGet("GetAllDepartments")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetAll()
        {
            return await this._departmentService.All();
        }


        [HttpPost]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Create_Department" })]
        public async Task<ResponseDTO> Add(DepartmentDto departmentDto)
        {
            departmentDto.Name = departmentDto.Name.Trim();
            if (await _departmentService.CheckIfDepartmentExist(departmentDto.Name))
            {
                return new ResponseDTO(ResponseStatus.Error, "Department already exist", departmentDto.Name);
            }
            return await _departmentService.Add(departmentDto.MapToEntity());
        }

        [HttpPut]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Update_Department" })]
        public async Task<ResponseDTO> Update(DepartmentEntity departmentEntity)
        {
            departmentEntity.Name = departmentEntity.Name.Trim();
            if (await _departmentService.CheckIfDepartmentExist(departmentEntity.Name))
            {
                return new ResponseDTO(ResponseStatus.Error, "Department already exist", departmentEntity.Name);
            }

            await _departmentService.Update(departmentEntity);
            return new ResponseDTO(ResponseStatus.Success, "Updated Successfully", departmentEntity);
        }

        [HttpGet]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Department" })]
        public async Task<PredicatedResponseDTO> GetPaginated(string searchKey, int? pageIndex, int? pageSize, string sortBy, SortOrder? sortOrder)
        {
            if (!String.IsNullOrEmpty(searchKey))
                searchKey = searchKey.Trim();
            return await _departmentService.GetWithPredicate(searchKey, pageIndex, pageSize, sortBy, sortOrder);
        }

        [HttpDelete]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Department" })]
        public async Task<ResponseDTO> Delete(Guid id)
        {

            return await _departmentService.DeleteDepartment(id);
        }
    }
}
