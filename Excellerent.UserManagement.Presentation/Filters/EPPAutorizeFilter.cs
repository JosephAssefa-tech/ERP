using AutoMapper;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Filters
{

    public class EPPAutorizeFilter : ActionFilterAttribute
    {
        private readonly IUserGroupsService _userGroupsService;
        private readonly IUserService _userService;
        private readonly IGroupSetPermissionService _groupSetPermissionService;
        private readonly string _permission;
        private readonly IMapper _mapper3;
        public EPPAutorizeFilter(
            IUserGroupsService userGroupsService,
            IUserService userService,
            IGroupSetPermissionService groupSetPermissionService,
            string permission
            )
        {
            this._userService = userService;
            _userGroupsService = userGroupsService;
            _groupSetPermissionService = groupSetPermissionService;
            _permission = permission;
            var config3 = new MapperConfiguration(Configuration => Configuration.CreateMap<PermissionGetDto, PermissionEntity>().ReverseMap());
            _mapper3 = new Mapper(config3);
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            const string key = "Authorization";
            var headers = context.HttpContext.Request.Headers;
            if (!headers.ContainsKey(key))
            {
                context.Result = new UnauthorizedObjectResult(new ResponseDTO(ResponseStatus.Error, "Unauthorized.", null));
            }
            else
            {
                string bearer = headers[key];
                string token = bearer.Substring(7);
                var handler = new JwtSecurityTokenHandler();
                var desiralizedToken = (handler.ReadJwtToken(token)) as JwtSecurityToken;
                var email = desiralizedToken.Claims.First(x => x.Type.ToLower() == JwtRegisteredClaimNames.Email).Value;


                if (email == null)
                {
                    context.Result = new UnauthorizedObjectResult(new ResponseDTO(ResponseStatus.Error, "Unauthorized.", null));
                }
                else
                {
                    var loggedInUserGuid = await _userService.GetUserGuidByEmail(email);
                    var result = await _userGroupsService.GetGroupSetByUserId(loggedInUserGuid);


                    bool auth = false;

                    foreach (var group in result)
                    {
                        var result2 = await _groupSetPermissionService.GetPermissionsByGroupId(group.Guid);
                        var mappedDto2 = result2.Select(x => this._mapper3.Map<PermissionGetDto>(x));
                        foreach (var li in mappedDto2)
                        {

                            string[] permissions = _permission.Split(',');
                            foreach (string arg in permissions)
                            {
                                if (li.KeyValue.Equals(arg.Trim()))
                                {
                                    auth = true;
                                    break;
                                }
                            }
                            if (auth)
                            {
                                break;
                            }
                        }
                        if (auth)
                        {
                            break;
                        }
                    }
                    if (!auth)
                    {
                        context.Result = new UnauthorizedObjectResult(new ResponseDTO(ResponseStatus.Error, "Unauthorized.", null));
                    }
                    else 
                    {
                        await next();
                    }
                }
            }
        }


    }
}
