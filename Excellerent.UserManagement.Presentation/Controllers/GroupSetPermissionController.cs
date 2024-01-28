using AutoMapper;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using Excellerent.UserManagement.Presentation.Models.PostModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GroupSetPermissionController : Controller
    {
        private readonly IGroupSetPermissionService _groupSetPermissionService;
        private readonly IMapper _mapper;

        public GroupSetPermissionController(IGroupSetPermissionService groupSetPermissionService)
        {
            _groupSetPermissionService = groupSetPermissionService;
            var config = new MapperConfiguration(Configuration => Configuration.CreateMap<PermissionGetDto, PermissionEntity>().ReverseMap());
            _mapper = new Mapper(config);
        }

        [AllowAnonymous]
        [HttpGet("GetByGroupId")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var result = await _groupSetPermissionService.GetPermissionsByGroupId(guid);
            var mappedDto = result?.Select(x => this._mapper.Map<PermissionGetDto>(x));
            return Ok(new ResponseDTO { Data = mappedDto, Message = "Successfully retrieved permissions based on group", ResponseStatus = ResponseStatus.Success });

        }
            
        [AllowAnonymous]
        [HttpPost]
        public  async Task<IActionResult> Post(GroupSetPermissionPostDto luPSS)  
        {

            if ((await _groupSetPermissionService.removePermissions(luPSS.GroupSetId)) == true)
            {

                foreach (var tes in luPSS.PermissionIDArray)
                {
                    var newEntity = new GroupSetPermissionEntity()
                    {
                        PermissionId = tes,
                        GroupSetId = luPSS.GroupSetId
                    };
                   await _groupSetPermissionService.Add(newEntity);
                }
            }
            
            return Ok(new ResponseDTO { Data = null, Message = "Successfully added permissions to group.", ResponseStatus = ResponseStatus.Success });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RemovedPermission(Guid guid)
        {
            var data =  _groupSetPermissionService.removePermissions(guid);

          
            return Ok(new ResponseDTO { Data = data, Message = "Permissions List.", ResponseStatus = ResponseStatus.Success });
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> DeletePermissionGroup(Guid id)  
        {
            //Check wherther there are users or designation assigned to the group
            return Ok(await _groupSetPermissionService.Delete(id));
        }
    }
}
