using AutoMapper;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Presentation.AccoutService;
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using Excellerent.ApplicantTracking.Presentation.Models.ViewModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LUSkillSetController : Controller
    {
        private readonly ILUSkillSetService _skillSetService;
        private readonly IAuthService _authService;
        private readonly string _feature = "SkillSet";
        private IMapper _mapper, _Imapper;
        public LUSkillSetController(IAuthService authService, ILUSkillSetService skillSetService)
        {
            _skillSetService = skillSetService;
            var config = new MapperConfiguration(configuratuion => configuratuion.CreateMap<LUSkillSetPostDto, LUSkillSetEntity>().ReverseMap());
            var confi = new MapperConfiguration(Configuration => Configuration.CreateMap<LUSkillSetGetDto, LUSkillSetEntity>().ReverseMap());
            _mapper = new Mapper(config);
            _Imapper = new Mapper(confi);
            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost("LUSkillSet")]
        public async Task<ActionResult<ResponseDTO>> AddFieldOfStudy(LUSkillSetPostDto model)
        {
            var mappedEntity = _mapper.Map<LUSkillSetEntity>(model);
            var res = await _skillSetService.AddSkill(mappedEntity);
            return Ok(new ResponseDTO
            {
                Data = res,
                Message = "Skill set added succesfully",
                ResponseStatus = ResponseStatus.Success,
                Ex = null
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBySkillLevel(string SkillName)
        {
            try
            {
                var result = _mapper.Map<LUSkillSetEntity>(await _skillSetService.GetBySkillName(SkillName));
                return Ok(new ResponseDTO { Data = result, Message = "Successfully retrieved user data.", ResponseStatus = ResponseStatus.Success });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _skillSetService.GetAllSkillSet();
            var mappedDto = result?.Select(x => this._Imapper.Map<LUSkillSetGetDto>(x));
            return Ok(new ResponseDTO { Data = mappedDto, Message = "Successfully retrieved skill set data.", ResponseStatus = ResponseStatus.Success });

        }

    }
}
