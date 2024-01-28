using AutoMapper;
using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Presentation.AccoutService;
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using Excellerent.ApplicantTracking.Presentation.Models.PutModels;
using Excellerent.ApplicantTracking.Presentation.Models.ViewModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Excellerent.ApplicantTracking.Presentation.ModelValidation;

namespace Excellerent.ApplicantTracking.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class ApplicantAreaOfInterestController : AuthorizedController
    {
        private readonly IApplicantAreaOfInterestService _applicantAreaOfInterestService;
        private readonly IAuthService _authService;
        private IMapper _mapper;
        private IMapper _mapper2;
        private IApplicantAreaOfInterestService service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AreaOfInterestPostValidation _aoiPostValidator;
        private readonly AreaOfInterestPutValidation _aoiPutValidator;
        public ApplicantAreaOfInterestController(IHttpContextAccessor htttpContextAccessor, IAuthService authService, IConfiguration configuration, IBusinessLog _businessLog, IApplicantAreaOfInterestService applicantAreaOfInterestService, IWebHostEnvironment webHostEnvironment) : base(htttpContextAccessor, configuration, _businessLog, "ApplicantAreaOfInterest")
        {
            _applicantAreaOfInterestService = applicantAreaOfInterestService;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantAreaOfInterestEntities, ApplicantAreaOfInterestPostModelDto>().ReverseMap());
            _mapper = new Mapper(config);
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantAreaOfInterestUpdateModelDto, ApplicantAreaOfInterestEntities>().ReverseMap());
            _mapper2 = new Mapper(config2);
            _webHostEnvironment = webHostEnvironment;
            _aoiPostValidator = new AreaOfInterestPostValidation();
            _aoiPutValidator = new AreaOfInterestPutValidation();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(Guid guid)
        {
            try
            {
                var result = await _applicantAreaOfInterestService.GetApplicantAreaOfInterest(guid);
                var mappedDto = result?.Select(x => this._mapper.Map<ApplicantAreaOfInterestPostModelDto>(x));
                return Ok(new ResponseDTO { Data = result, Message = "Successfully retrieved area of interest.", ResponseStatus = ResponseStatus.Success });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
        [AllowAnonymous]
        [HttpPut("ApplicantAreaOfInterest" )]
        public async Task<ActionResult<ResponseDTO>> UpdateApptAreaOfInterest(ApplicantAreaOfInterestUpdateModelDto patchDoc)
        {
            var vldResult = _aoiPutValidator.Validate(patchDoc);
            if (!vldResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, vldResult.ToString(), null));
            }
            var applicantAreaEntity = _mapper2.Map<ApplicantAreaOfInterestEntities>(patchDoc);
            var result = await _applicantAreaOfInterestService.UpdateApplicantAreaOfInterest(applicantAreaEntity);

            return Ok(new ResponseDTO { Data = result, Message = "Applicant Profile has been updated succesfully", ResponseStatus = ResponseStatus.Success, Ex = null });
        }
        //List<ApplicantAreaOfInterestEntities> aOfIntersts = new List<ApplicantAreaOfInterestEntities>();
        //aOfIntersts = ConvertDataTable<ApplicantAreaOfInterestEntities>(DataTable dt);  
        [AllowAnonymous]
        [HttpPost("applicantAreaOfInterestToApply")]
        public async Task<ActionResult<ResponseDTO>> AddAppAreaOfInterest(ApplicantAreaOfInterestPostModelDto model)
        {
            var vldResult = _aoiPostValidator.Validate(model);
            if (!vldResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, vldResult.ToString(), null));
            }
            var mappedEntity = _mapper.Map<ApplicantAreaOfInterestEntities>(model);
            var res = await _applicantAreaOfInterestService.AddApplicantAreaOfInterst(mappedEntity);
            return Ok(new ResponseDTO
            {
                Data = res,
                Message = "Applicant Area of Interest added succesfully",
                ResponseStatus = ResponseStatus.Success,
                Ex = null
            });
        }
        [AllowAnonymous] 
        [HttpDelete("DeleteApplicantAreaOfInterst")]
        public async Task<ActionResult<Guid>> DeleteApplicantAreaOfInterst(Guid id)
        {
            try
            {
                var aoiToDelete = await _applicantAreaOfInterestService.GetApplicantAreaOfInterest(id);
                if (aoiToDelete == null)
                {
                    return NotFound($"Area of Interest with Id = {id} not found");
                }

                return await _applicantAreaOfInterestService.DeleteApplicantAreaOfInterst(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }

        }
    }
}
