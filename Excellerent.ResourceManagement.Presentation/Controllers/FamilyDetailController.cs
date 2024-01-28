using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public  class FamilyDetailController:ControllerBase
    {
        private readonly IFamilyDetailService _familyDetailService;
        private readonly IRelationshipService _relationshipService;
        public FamilyDetailController(IFamilyDetailService familyDetailService, IRelationshipService relationshipService)
    {
        _familyDetailService=familyDetailService;
        _relationshipService = relationshipService;
    }
        [Authorize]
        [HttpPost("familydetails")]
        public async Task<ResponseDTO> AddFamilyDetails([FromBody] FamilyDetailsEntity familyDetail)
        {
           
                return await _familyDetailService.Add(familyDetail);
          
        }
        [Authorize]
        [HttpGet("GetFamilyDetailsByEmployeeId/{employeeId}")]
        public async Task<ActionResult<IEnumerable<FamilyDetailsEntity>>> GetFamilyDetailByEmployeeId(Guid employeeId)
        {
            var result = await _familyDetailService.GetFamilyDetailByEmployeeId(employeeId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetAllFamilyMembers")]
        public async Task<ActionResult<RelationshipEntity>> GetAllFamilyMembers()
        {
            var result = await _relationshipService.GetAll();
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<ResponseDTO> EditFamilyDetail(FamilyDetailsEntity familyDetailsEntity)
        {
            return await _familyDetailService.Update(familyDetailsEntity);
        }
        [Authorize]
        [HttpDelete]
        public async Task<bool> DeleteFamily(Guid id)
        {
            var result = await _familyDetailService.DeleteFamilyMember(id);
            return true;
        }
        

    }
}
