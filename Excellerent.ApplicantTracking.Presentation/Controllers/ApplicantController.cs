using AutoMapper;
using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.ModelValidation;
using Excellerent.ApplicantTracking.Presentation.AccoutService;
using Excellerent.ApplicantTracking.Presentation.Models.PutModels;
using Excellerent.ApplicantTracking.Presentation.Models.ViewModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class ApplicantController : AuthorizedController
    {
        private readonly IApplicantService _applicantService;
        private readonly IAuthService _authService;
        private readonly string _feature = "ApplicantProfile";
        private IMapper _mapper;
        private IMapper _mapper2;
        private ApplicantProfileValidation _profileValidation;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApplicantController(IHttpContextAccessor htttpContextAccessor, IAuthService authService, IConfiguration configuration, IBusinessLog _businessLog, IApplicantService applicantService, IWebHostEnvironment webHostEnvironment) : base(htttpContextAccessor, configuration, _businessLog, "ApplicantProfile")
        {
            _applicantService = applicantService;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantEntity, ApplicantGetResponseDto>());
            _mapper = new Mapper(config);
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantUpdateModelDto, ApplicantEntity>());
            _mapper2 = new Mapper(config2);
            _profileValidation = new ApplicantProfileValidation();
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(string email)
        {
            try
            {

                var result = _mapper.Map<ApplicantGetResponseDto>(await _applicantService.GetApplicantsByEmails(email));
                return Ok(new ResponseDTO { Data = result, Message = "Successfully retrieved user data.", ResponseStatus = ResponseStatus.Success });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
        //[HttpGet("{email}")]
        //[AllowAnonymous]

        //public async Task<ActionResult<ApplicantGetResponseDto>> GetApplicantByEmail(string email)

        //{


        //}

        [AllowAnonymous]
        [HttpPut("ApplicantGeneralInfo")]
        public async Task<ActionResult<ResponseDTO>> GeneralInfoApplicant(ApplicantUpdateModelDto patchDoc)
        {
            var applicantEntity = _mapper2.Map<ApplicantEntity>(patchDoc);
            var validationResult = _profileValidation.Validate(applicantEntity);
            if (!validationResult.IsValid)
                return BadRequest(new ResponseDTO { Data = null, Message = validationResult.ToString(), ResponseStatus = ResponseStatus.Error, Ex = null });

            var result = await _applicantService.UpdateApplicant(applicantEntity);

            return Ok(new ResponseDTO { Data = result, Message = "Applicant Profile has been updated succesfully", ResponseStatus = ResponseStatus.Success, Ex = null });
        }

        [AllowAnonymous]
        [HttpPost("FileUploadHandler")]

        public IActionResult FileUploadHandler(IFormFile file)

        {
            string uniqueFileName = null;

            string urlLink = null;

            string foldername = null;

            string webRootPath = _webHostEnvironment.WebRootPath;

            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            if (extension == "pdf")

            {

                foldername = "resumeimages";

            }

            else

            {

                foldername = "profileimages";

            }

            string newPath = Path.Combine(webRootPath, foldername);

            if (!Directory.Exists(newPath))

            {

                Directory.CreateDirectory(newPath);

            }

            if (file != null)

            {

                //string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath);

                var generatedName = Guid.NewGuid().ToString() + "_" + file.FileName.Replace(" ", "");

                uniqueFileName = foldername + "\\" + generatedName;

                urlLink = foldername + "/" + generatedName;

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))

                {

                    file.CopyTo(fileStream);

                }

            }

            return Ok(new { urlLink });

        }

    }
}
