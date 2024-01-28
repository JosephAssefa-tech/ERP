using AutoMapper;
using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Presentation.Models.PostModels;
using Excellerent.ApplicantTracking.Presentation.Models.PutModels;
using Excellerent.ApplicantTracking.Presentation.Models.ViewModels;
using Excellerent.ApplicantTracking.Presentation.ModelValidation;
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
    [Authorize]
    public class EducationController : Controller
    {
        private readonly IEducationService _educationService;
        private readonly ILUFieldOfStudyService _fieldStuduService;
        private readonly IEducationProgrammeService _eduProgrammeService;
        private readonly EducationPostModelValidator _postValidator;
        private readonly EducationPutModelValidator _putValidator;
        private readonly IMapper _imapper;
        public EducationController(IEducationService educationService,
            ILUFieldOfStudyService fieldStuduService,
            IEducationProgrammeService eduProgrammeService,
            IMapper mapper)
        {
            _educationService = educationService;
            _fieldStuduService = fieldStuduService;
            _eduProgrammeService = eduProgrammeService;
            _imapper = mapper;
            _postValidator = new EducationPostModelValidator(_fieldStuduService, _eduProgrammeService, educationService);
            _putValidator = new EducationPutModelValidator(_fieldStuduService, _eduProgrammeService);
        }

        [HttpGet]
        public async Task<ResponseDTO> Get(Guid? id, Guid? applicantId)
        {
            var entities = await _educationService.GetWithPredicateAsync(id, applicantId);
            var response = entities?.Select(x => this._imapper.Map<EducationGetModelDto>(x));
            return new ResponseDTO(ResponseStatus.Success, "", response);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> Post([FromBody] EducationPostModelDto education)
        {
            var vldResult = _postValidator.Validate(education);
            if (!vldResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, vldResult.ToString(), null));
            }
            return Ok(await _educationService.Add(this._imapper.Map<EducationEntity>(education)));
        }
        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> Put( [FromBody] EducationPutModel education)
        {
            var vldResult =  _putValidator.Validate(education);
            if (!vldResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, vldResult.ToString(), "High School Exists! Please edit/remove the existing one!"));
            }
            return Ok(await _educationService.Update(this._imapper.Map<EducationEntity>(education))); 
        }
        [HttpDelete("{guid}")]
        public async Task<ActionResult<ResponseDTO>> Delete(Guid guid)
        {
            var request = await _educationService.FindOneAsyncForDelete(guid);
            if (request == null)
            {   
                return BadRequest(new ResponseDTO(ResponseStatus.Error, $"Not found", null));
            }
            return Ok(await _educationService.Delete(request));
        }
    }
}
