using AutoMapper;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Presentation.AccoutService;
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
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
    public class LUPositionToApplyController : Controller
    {
        private readonly ILuPositionService _positionToApplyService;
        private readonly IAuthService _authService;
        private readonly string _feature = "Positions";
        private IMapper _mapper, _Imapper;

        public LUPositionToApplyController(IAuthService authService, ILuPositionService positionService)
        {
            _positionToApplyService = positionService;
            var config = new MapperConfiguration(configuratuion => configuratuion.CreateMap<LUPositionToApplyPostModelDto, LUPositionToApplyEntity>().ReverseMap());
            var confi = new MapperConfiguration(configuratuion => configuratuion.CreateMap<LUPositionToApplyGetModelDto, LUPositionToApplyEntity>().ReverseMap());
            _mapper = new Mapper(config);
            _Imapper = new Mapper(confi);
            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost("LUPositionToApply")]
        public async Task<ActionResult<ResponseDTO>> AddPositions(LUPositionToApplyPostModelDto model)
        {
            var mappedEntity = _mapper.Map<LUPositionToApplyEntity>(model);
            var res = await _positionToApplyService.AddPosition(mappedEntity);

            return Ok(new ResponseDTO
            {
                Data = res,
                Message = "Position added succesfully",
                ResponseStatus = ResponseStatus.Success,
                Ex = null
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPositionByName(string PositionName)
        {
            try
            {
                var result = _mapper.Map<LUPositionToApplyPostModelDto>(await _positionToApplyService.GetByPositionName(PositionName));
                return Ok(new ResponseDTO { Data = result, Message = "Successfully retrieved user data.", ResponseStatus = ResponseStatus.Success });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [AllowAnonymous]
        [HttpGet("LUPositionToApplyGetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _positionToApplyService.GetAllPositions();
            var mappedDto = result?.Select(x => this._Imapper.Map<LUPositionToApplyGetModelDto>(x));
            return Ok(new ResponseDTO { Data = mappedDto, Message = "Successfully retrieved Positions to apply data.", ResponseStatus = ResponseStatus.Success });

        }
    }
}
