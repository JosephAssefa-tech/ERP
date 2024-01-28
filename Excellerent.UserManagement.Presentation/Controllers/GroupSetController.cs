
using AutoMapper;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using Excellerent.UserManagement.Presentation.Filters;
using Excellerent.UserManagement.Presentation.Models.PutModels;
using Excellerent.UserManagement.Presentation.Models.RequestDtos;
using Excellerent.UserManagement.Presentation.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GroupSetController : Controller
    {
        private readonly IGroupSetService _groupSetService;
        private readonly IMapper _mapper;
        private readonly IMapper _putMapper;
        private readonly IMapper _groupSetMapper;
        private readonly GroupSetPostRequestValidator _validator;
        private readonly GroupSetPatchRequestValidator _putValidator;
        public GroupSetController(IGroupSetService groupSetService)
        {
            _groupSetService = groupSetService;
            var config = new MapperConfiguration(configuratuion => configuratuion.CreateMap<GroupSetPostRequestDto, GroupSetEntity>().ReverseMap());
            var putconfig = new MapperConfiguration(configuratuion => configuratuion.CreateMap<GroupSetPatchModel, GroupSetEntity>().ReverseMap());
            _mapper = new Mapper(config);
            _putMapper = new Mapper(putconfig);
            _validator = new GroupSetPostRequestValidator();
            _putValidator = new GroupSetPatchRequestValidator();
        }


        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Create_Group" })]
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> Post(GroupSetPostRequestDto request)
        {
            var validationResult = _validator.Validate(request);
            var existingGroup = _groupSetService.GetGroupByName(request.Name);
            if (!(existingGroup.Result == null))
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, "Group with the same name already exists", null));
            }
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, validationResult.ToString(), null));
            }
            return Ok(await _groupSetService.Add(this._mapper.Map<GroupSetEntity>(request)));
        }
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Group, Update_User" })]
        [HttpGet]
        public async Task<PredicatedResponseDTO> GetAllEmployeeDashboard(string searchKey, int? pageIndex, int? pageSize, string sortBy, SortOrder? sortOrder)
        {
            if (!String.IsNullOrEmpty(searchKey))
                searchKey = searchKey.Trim();
            return await _groupSetService.GetAllUserGroupsDashboardAsync(searchKey, pageIndex, pageSize, sortBy, sortOrder);
        }

        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Update_Group" })]
        [HttpPatch]
        public async Task<ActionResult<ResponseDTO>> UpdateGroupDescription(GroupSetPatchModel request)
        {
            var validationResult = _putValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, validationResult.ToString(), null));
            }

            await _groupSetService.UpdateGroupDescription(request);
            return new ResponseDTO(ResponseStatus.Success, "Successfully updated group's description", request);
        }

        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Group, Update_User, Add_User" })]
        [HttpGet("LoadGroupSet")]
        public async Task<ActionResult<GroupSetDetail>> GetGroupSet(Guid id)
        {
            return Ok(await _groupSetService.GetGroupSetById(id));
        }
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "View_Group, Update_User, Add_User" })]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<GroupSetEntity>> GetAll()
        {
            return await _groupSetService.GetAll();
        }
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Group" })]
        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> DeleteGroup(Guid id)
        {
            //Check wherther there are users or designation assigned to the group
            return Ok(await _groupSetService.Delete(id));
        }

        [HttpGet("CheckGroupNameExistance")]
        public async Task<ActionResult<ResponseDTO>> CheckGroupNameExistance(string  groupName)
        {
            var existingGroup = _groupSetService.GetGroupByName(groupName);
            if (!(existingGroup.Result == null))
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, "Group with the same name already exists", true));
            }
            else
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Success, "", false));
            }
        }

        [Authorize]
        [HttpGet("IsSuperAdmin")]
        public async Task<bool> IsSuperAdmin(string id) 
        {
            return await _groupSetService.IsSuperAdmin(id);
        }
    }
}
