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
    public class LUPositionSkillSetController: Controller
    {
        private readonly ILUPositionSkillSetService _positionSkillSetService;
        private readonly IAuthService _authService;
        private readonly string _feature = "PositionSkillSet";
        private IMapper _mapper;
        private IMapper _mapper2;

        public LUPositionSkillSetController(ILUPositionSkillSetService positionSkillSetService, IAuthService authService)
        {
            _positionSkillSetService = positionSkillSetService;
            _authService = authService;
            var config = new MapperConfiguration(configuratuion => configuratuion.CreateMap<LUSkillSetGetDto, LUSkillSetEntity>().ReverseMap());
            _mapper = new Mapper(config);

            var config2 = new MapperConfiguration(configuratuion => configuratuion.CreateMap<LUPositionSkillSetDto, LUPositionSkillSetEntity>().ReverseMap());
            _mapper2 = new Mapper(config2);
        }

      
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var result = await _positionSkillSetService.GetSkillByPosition(guid);
            var mappedDto = result?.Select(x => this._mapper.Map<LUSkillSetGetDto>(x));
            return Ok(new ResponseDTO { Data = mappedDto, Message = "Successfully retrieved Skills Based on Position", ResponseStatus = ResponseStatus.Success });

        }

        [AllowAnonymous]
        [HttpPost("Post")]
        public async Task<IActionResult> Post(LUPositionSkillSetDto luPSS)
        {
            var result = await _positionSkillSetService.Add(_mapper2.Map<LUPositionSkillSetEntity>(luPSS));
            return Ok(new ResponseDTO { Data = result.Data, Message = "Successfully added position to skill association.", ResponseStatus = ResponseStatus.Success });
        }
    }
}
