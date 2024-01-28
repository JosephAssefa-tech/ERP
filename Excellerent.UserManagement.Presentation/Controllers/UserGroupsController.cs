using AutoMapper;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using Excellerent.UserManagement.Presentation.Models.PostModel;
using Excellerent.UserManagement.Presentation.Models.RequestDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserGroupsController : Controller
    {
        private readonly IUserGroupsService _userGroupsService;
        private readonly IUserService _userService;
        private readonly IGroupSetPermissionService _groupSetPermissionService;

        private readonly IMapper _mapper;
        private readonly IMapper _mapper2;
        private readonly IMapper _mapper3;

        public UserGroupsController(IUserGroupsService userGroupsService, IUserService userService, IGroupSetPermissionService groupSetPermissionService)
        {
            _userGroupsService = userGroupsService;
            _userService = userService;
            _groupSetPermissionService = groupSetPermissionService;

            var config = new MapperConfiguration(Configuration => Configuration.CreateMap<GroupSetPostRequestDto, UserEntity>().ReverseMap());
            _mapper = new Mapper(config);
            var config2 = new MapperConfiguration(Configuration => Configuration.CreateMap<GroupSetPostRequestDto, GroupSetEntity>().ReverseMap());
            _mapper2 = new Mapper(config2); 

            var config3 = new MapperConfiguration(Configuration => Configuration.CreateMap<PermissionGetDto, PermissionEntity>().ReverseMap());
            _mapper3 = new Mapper(config3);
        }
        [AllowAnonymous]
        [HttpGet("GetUserByGroupId")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var result = await _userGroupsService.GetUserByGroupId(guid);
            var mappedDto = result?.Select(x => this._mapper.Map<GroupSetPostRequestDto>(x));
            return Ok(new ResponseDTO { Data = mappedDto, Message = "Successfully retrieved users based on group", ResponseStatus = ResponseStatus.Success });

        }
        [AllowAnonymous]
        [HttpGet("GetGroupSetByUserId")]
        public async Task<IActionResult> GetGroupSetByUserId(Guid guid)
        {
            var result = await _userGroupsService.GetGroupSetByUserId(guid);
            //var mappedDto = result?.Select(x => this._mapper2.Map<GroupSetPostRequestDto>(x));
            return Ok(new ResponseDTO { Data = result, Message = "Successfully retrieved groups based on user id", ResponseStatus = ResponseStatus.Success });

        }
        [Authorize]
        [HttpGet("GetPermissionsByUserEmail")]
        public async Task<IActionResult> GetPermissionsByUserEmail(String email)
        {
            var loggedInUserGuid = _userService.GetUserGuidByEmail(email);
            var result = await _userGroupsService.GetGroupSetByUserId(loggedInUserGuid.Result);
             List<PermissionGetDto> arrayOfPermissions = new List<PermissionGetDto>();

            foreach (var group in result)
            {
                var result2 = await _groupSetPermissionService.GetPermissionsByGroupId(group.Guid);
                var mappedDto2 = result2?.Select(x => this._mapper3.Map<PermissionGetDto>(x));
                foreach(var li in mappedDto2)
                {
                    if (!arrayOfPermissions.Contains(li))
                    {
                        arrayOfPermissions.Add(li);
                    }
                }
            }
            return Ok(new ResponseDTO { Data = arrayOfPermissions, Message = "Successfully retrieved permissions based on user id", ResponseStatus = ResponseStatus.Success });

        }
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllByUserId(Guid guid)
        {
            var result = await _userGroupsService.GetAllByUserId(guid);
            //var mappedDto = result?.Select(x => this._mapper2.Map<GroupSetPostRequestDto>(x));
            return Ok(new ResponseDTO { Data = result, Message = "Successfully retrieved user-groups based on user id", ResponseStatus = ResponseStatus.Success });

        }
        [AllowAnonymous] 
        [HttpPost]
        public async Task<IActionResult> Post(UserGroupPostDto luPSS)
        {
            foreach (var tes in luPSS.GroupIDArray)
            {
                var newEntity = new UserGroupsEntity()
                {
                    GroupSetGuid = tes,
                    UserGuid = luPSS.UserGuid
                };
                await _userGroupsService.Add(newEntity);
            }
            return Ok(new ResponseDTO { Data = null, Message = "Successfully added permissions to group.", ResponseStatus = ResponseStatus.Success });
        }
        [HttpPut]
        public async Task<IActionResult> Put(Guid userId, Guid[] groupIds)
        {

            var success = await _userGroupsService.Update(userId, groupIds);
            if (success)
                return Ok(new ResponseDTO { Data = null, Message = "Groups are successfully added", ResponseStatus = ResponseStatus.Success });
            return NotFound(new ResponseDTO { Data = null, Message = "", ResponseStatus = ResponseStatus.Error });
        }


        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> DeleteUserGroup(Guid guid)
        {
            //Check wherther there are users or designation assigned to the group
            return Ok(await _userGroupsService.Delete(guid));
        }

        [HttpDelete("RemoveUserFromGroup")]
        public async Task<ActionResult<PredicatedResponseDTO>> RemoveUserFromGroup(Guid userGroupGuid)
        {
            return Ok(await _userGroupsService.RemoveGroupUser(userGroupGuid));
        }

        [AllowAnonymous]
        [HttpPost("RegisterUsersInGroup")]
        public async Task<ActionResult<ResponseDTO>> RegisterUsersInGroup([FromBody] GroupUsersPostDto groupUsers)
        {
            return Ok(await _userGroupsService.AddUsersToGroup(groupUsers.UserGuidCollection, Guid.Parse(groupUsers.GroupGuid)));
        }

        [HttpGet("LoadGroupUsersSet")]
        public async Task<ActionResult<PredicatedResponseDTO>> GetGroupSetUsers(Guid groupId, int pageIndex, int pageSize)
        {
            return Ok(await _userGroupsService.GetAllGroupUsersAsync(groupId, pageIndex, pageSize));
        }
    }
}
