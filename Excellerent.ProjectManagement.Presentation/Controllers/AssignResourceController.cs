using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ProjectManagement.Presentation.Models.PostModels;
using Excellerent.ProjectManagement.Presentation.Models.UpdateModels;
using Excellerent.SharedModules.DTO;
using Excellerent.UserManagement.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class AssignResourceController : ControllerBase
    {
        private readonly IAssignResourceService _assignResourceService;

        public AssignResourceController(IAssignResourceService assignResourceService)
        {
            _assignResourceService = assignResourceService;
        }
        [HttpPost]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "Assign_Resource" })]
        public ResponseDTO AddAssignResource(AssignResourcePostModel model)
        {
            return _assignResourceService.AssginResourceForProject(model.MappToEntity()).Result;


        }
        [HttpGet]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "View_Resources" })]
        public Task<IEnumerable<AssignResourcEntity>> GetAssignResource()
        {
            return _assignResourceService.GetAll();


        }

        [HttpGet("ByProject")]
    
        public async Task<ResponseDTO> GetAssignResourceByProject(Guid projectGuid)
        {
             return await  _assignResourceService.GetAssignResourceByProject(projectGuid);
  

        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "View_Resources" })]
        public AssignResourcEntity GetAssignResourceById(Guid id)
        {
            return _assignResourceService.GetOneAssignResource(id).Result;
        }


        [HttpPut]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "Update_Resources" })]
        public ResponseDTO UpdateAssignResource([FromBody]  AssignedResoureUpdateModel modelItem)
        {
            return _assignResourceService.Update(modelItem.MappToEntity()).Result;

        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "Delete_Resources" })]
        public ResponseDTO DeleteAssignResource(Guid id)
        {
           return _assignResourceService.UnassginResourceOfProject(id).Result;

        }


        [HttpGet("check_assignment_status/")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Employee" })]
        public async Task<bool> CheckEmployeeAssignmentStatus(Guid id)
        {
            return await _assignResourceService.GetAssignmentStatus(id);
           
        }

    }
}
