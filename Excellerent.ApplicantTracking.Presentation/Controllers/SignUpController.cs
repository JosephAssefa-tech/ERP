using AutoMapper;
using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Presentation.AccoutService;
using Excellerent.ApplicantTracking.Presentation.Models;
using Excellerent.ApplicantTracking.Presentation.ModelValidation;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;


namespace Excellerent.Timesheet.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class SignUpController : AuthorizedController
    {
        private readonly IApplicantService _applicantService;
        private readonly IAuthService _authService;
        private readonly string _feature = "SignUp";
        private IMapper _mapper;
        private SignupValidation signupValidation;
        public SignUpController(IHttpContextAccessor htttpContextAccessor, IAuthService authService, IConfiguration configuration, IBusinessLog _businessLog, IApplicantService applicantService) : base(htttpContextAccessor, configuration, _businessLog, "SignUp")
        {
            _applicantService = applicantService;
            var config = new MapperConfiguration(configuratuion => configuratuion.CreateMap<SignUpModelDto, ApplicantEntity>());
            _mapper = new Mapper(config);
            signupValidation = new SignupValidation();
            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost("Sign-Up")]
        public async Task<ActionResult<ResponseDTO>> ApplicantSignUp(SignUpModelDto model)
        {

            var mappedEntity = _mapper.Map<ApplicantEntity>(model);

            var validationResult = signupValidation.Validate(mappedEntity);
            if (!validationResult.IsValid)
                return BadRequest(new ResponseDTO { Data = null, Message = validationResult.ToString(), ResponseStatus = ResponseStatus.Error, Ex = null });
            mappedEntity.Password = _authService.HashPassword(mappedEntity.Password);
            var res = await _applicantService.ApplicantSignUp(mappedEntity);
            if (res == Guid.Empty)
            {

                return StatusCode(403, new ResponseDTO { Data = null, Message = "User Is already registered with this email address.", ResponseStatus = ResponseStatus.Error, Ex = null });
            }

            return Ok(new ResponseDTO { Data = res, Message = "User has been added succesfully", ResponseStatus = ResponseStatus.Success, Ex = null });
        }
    }
}
