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
    public class LUFieldOfStudyController : Controller
    {
        private readonly ILUFieldOfStudyService _fieldOfStudyService;
        private readonly IAuthService _authService;
        private readonly string _feature = "FieldOfStudy";
        private IMapper _mapper;
        private IMapper _Imapper;
        public LUFieldOfStudyController(IAuthService authService, ILUFieldOfStudyService fieldOfStudyService)
        {
            _fieldOfStudyService = fieldOfStudyService;
            var config = new MapperConfiguration(configuratuion => configuratuion.CreateMap<LUFieldOfStudyModelDto, LUFieldOfStudyEntity>().ReverseMap());
            var conf = new MapperConfiguration(configuratuion => configuratuion.CreateMap<LUFieldOfStudyGetModelDto, LUFieldOfStudyEntity>().ReverseMap());
            _mapper = new Mapper(config);
            _Imapper = new Mapper(conf);

            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost("FieldOfStudy")]
        public async Task<ActionResult<ResponseDTO>> AddFieldOfStudy(LUFieldOfStudyModelDto model)
        {
            var mappedEntity = _mapper.Map<LUFieldOfStudyEntity>(model);
            var res = await _fieldOfStudyService.AddFieldOfStudy(mappedEntity);
            return Ok(new ResponseDTO
            {
                Data = res,
                Message = "Field Of Study added succesfully",
                ResponseStatus = ResponseStatus.Success,
                Ex = null
            });
        }

        [AllowAnonymous]
        [HttpGet("{educationName}")]
        public async Task<IActionResult> GetByEducationName(string educationName)
        {
            try
            {
                var result = _mapper.Map<LUFieldOfStudyModelDto>(await _fieldOfStudyService.GetByEducationName(educationName));
                return Ok(new ResponseDTO { Data = result, Message = "Successfully retrieved user data.", ResponseStatus = ResponseStatus.Success });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = (await _fieldOfStudyService.GetAll());
            var response = result?.Select(x => _Imapper.Map<LUFieldOfStudyGetModelDto>(x));
            return Ok(new ResponseDTO { Data = response, Message = "", ResponseStatus = ResponseStatus.Success });
        }
    }
}
