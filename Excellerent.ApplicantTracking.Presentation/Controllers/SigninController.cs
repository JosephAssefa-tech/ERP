using AutoMapper;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.ModelValidation;
using Excellerent.ApplicantTracking.Presentation.AccoutService;
using Excellerent.ApplicantTracking.Presentation.Models;
using Excellerent.ApplicantTracking.Presentation.Modlels.ViewModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SigninController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IApplicantService _applicantService;
        private readonly IMapper _mapper;
        private readonly IMapper _mapper2;
        private UserCredentialValidation userCredentialValidation;
        public SigninController(IApplicantService applicantService, IAuthService authService)
        {
            _applicantService = applicantService;
            _authService = authService;
            userCredentialValidation = new UserCredentialValidation();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SignInModelDto, ApplicantEntity>());
            _mapper = new Mapper(config);
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantEntity, SigninViewModel>());
            _mapper2 = new Mapper(config2);
        }
        [AllowAnonymous]
        [HttpPost("Sign-IN")]
        public async Task<IActionResult> Signin([FromBody] SignInModelDto user)
        {
            var applicantData = _mapper.Map<ApplicantEntity>(user);
            var validationResult = userCredentialValidation.Validate(applicantData);
            if (!validationResult.IsValid)
                return BadRequest(new ResponseDTO { Data = null, Message = validationResult.ToString(), ResponseStatus = ResponseStatus.Error, Ex = null });

            ApplicantEntity applicant = await _applicantService.Authenticate(user.Email, user.Password);

            if (applicant == null || !_authService.VerifyPassword(user.Password, applicant.Password)) return Unauthorized(new ResponseDTO { Data = null, Message = "Unauthorized", ResponseStatus = ResponseStatus.Error, Ex = null });

            var token = _authService.Authenticate(applicant);

            var userRes = _mapper2.Map<SigninViewModel>(applicant);
            userRes.Token = token;

            return Ok(new ResponseDTO { Data = userRes, Message = "Logged in successfully", ResponseStatus = ResponseStatus.Success });
        }
    }
}
