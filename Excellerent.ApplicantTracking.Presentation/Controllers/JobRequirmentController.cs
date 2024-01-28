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
    public class JobRequirmentController : Controller
    {
        private readonly IJobRequirmentService _jobRequirmentService;
        private readonly IAuthService _authService;
        private readonly string _feature = "Job Requirment";
        private IMapper _mapper, _Imapper;
        public JobRequirmentController(IAuthService authService, IJobRequirmentService jobRequirmentService)
        {
            _jobRequirmentService = jobRequirmentService;
            var configurePost = new MapperConfiguration(configuratuion => configuratuion.CreateMap<JobRequirmentPostModel, JobRequirmentEntity>().ReverseMap());
            var configureGet = new MapperConfiguration(Configuration => Configuration.CreateMap<JobRequirmentGetModel, JobRequirmentEntity>().ReverseMap());
            _mapper = new Mapper(configurePost);
            _Imapper = new Mapper(configureGet);
            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost("JobRequirment")]
        public async Task<ActionResult<ResponseDTO>> AddFieldOfStudy(JobRequirmentPostModel model)
        {
            var mappedEntity = _mapper.Map<JobRequirmentEntity>(model);
            var res = await _jobRequirmentService.AddJobRequirment(mappedEntity);
            return Ok(new ResponseDTO
            {
                Data = res,
                Message = "Job Requirment set added succesfully",
                ResponseStatus = ResponseStatus.Success,
                Ex = null
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetByJobDescriptionName(string JobDescriptionName)
        {
            try
            {
                var result = _mapper.Map<JobRequirmentEntity>(await _jobRequirmentService.GetByJobRequirment(JobDescriptionName));
                return Ok(new ResponseDTO { Data = result, Message = "Successfully retrieved user data.", ResponseStatus = ResponseStatus.Success });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _jobRequirmentService.GetAllJobRequirmentEntity();
            var mappedDto = result?.Select(x => this._Imapper.Map<JobRequirmentGetModel>(x));
            return Ok(new ResponseDTO { Data = mappedDto, Message = "Successfully retrieved Job Requirment set data.", ResponseStatus = ResponseStatus.Success });

        }

    }
}
