using AutoMapper;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using Excellerent.UserManagement.Presentation.Models.RequestDtos;
using Excellerent.UserManagement.Presentation.Validations;
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
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        private readonly IMapper _mapper2;
        private readonly PermissionPostRequestValidator _validator;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
            var config = new MapperConfiguration(configuratuion => configuratuion.CreateMap<PermissionPostRequestDto, PermissionEntity>().ReverseMap());
            _mapper = new Mapper(config);
            var config2 = new MapperConfiguration(configuratuion => configuratuion.CreateMap<PermissionGetDto, PermissionEntity>().ReverseMap());
            _mapper2 = new Mapper(config2);
            _validator = new PermissionPostRequestValidator();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> Post(PermissionPostRequestDto request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, validationResult.ToString(), null));
            }
            return Ok(await _permissionService.Add(this._mapper.Map<PermissionEntity>(request)));  
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAll()
        {
            var result = await _permissionService.GetAllPermissions();
            var mappedDto = result?.Select(x => this._mapper2.Map<PermissionGetDto>(x));
            return Ok(new ResponseDTO { Data = mappedDto, Message = "Successfully retrieved all data.", ResponseStatus = ResponseStatus.Success });
        }
        [AllowAnonymous]
        [HttpGet("zeroLevel")]
        public async Task<ActionResult<ResponseDTO>> GetGroupedPermission()
        {
            var result = await _permissionService.GetZeroLevelssions();
            //var mappedDto = result?.Select(x => this._mapper2.Map<PermissionGetDto>(x));
            return result;
        }
        [AllowAnonymous]
        [HttpGet("module")]
        public async Task<ActionResult<ResponseDTO>> GetModulePermission()
        {
            var result = await _permissionService.GetModulePermission();
            //var mappedDto = result?.Select(x => this._mapper2.Map<PermissionGetDto>(x));
            return result;
        }
        [AllowAnonymous]
        [HttpGet("CategoryByGroupId")]
        public async Task<ActionResult<ResponseDTO>> GetGroupedPermissionCategory(Guid guid)
        {
            var result = await _permissionService.GetPermissionCategoryByGroupId(guid);
            return result;
        }
    }
}

