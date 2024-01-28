
using Excellerent.ResourceManagement.Domain.DTOs;
using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.UserManagement.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
  
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "View_Employee" })]
        public async Task<ResponseDTO> Get()
        {
            return new ResponseDTO(ResponseStatus.Success,"Fetch All Succesfull",await _employeeService.GetAllEmployeesAsync());
        }

        [HttpGet("GetAllEmployeeDashboard")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "View_Employee" })]
        public async Task<PredicatedResponseDTO> GetAllEmployeeDashboard(string searhKey, int pageIndex, int pageSize)
        {
            return await _employeeService.GetAllEmployeesDashboardAsync(searhKey, pageIndex, pageSize);
        }

        [HttpGet("GetAllEmployeeDashboardFilter")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
         Arguments = new object[] { "View_Employee" })]
        public async Task<PredicatedResponseDTO> GetAllEmployeeDashboardFilter([FromQuery] EmployeeSpecParams paginationParams)
        {
            return await _employeeService.GetAllEmployeesDashboardFilterAsync(paginationParams);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "Create_Employee" })]
        public async Task<ResponseDTO> Post(EmployeeEntity employee)
        {
            if (!(await _employeeService.CheckEmailAddressExistence(employee.EmployeeOrganization.CompaynEmail, Guid.Empty)).Data)
            {
                return new ResponseDTO(ResponseStatus.Success, "Entry Succesfull", await _employeeService.AddNewEmployeeEntry(employee.MapToModel()));
            }
            else 
            {
                return new ResponseDTO(ResponseStatus.Error, "There is already registered employee with the email you provided",employee);
            }
        }

        [HttpPut]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "Update_Employee,Update_My_Profile" })]
        public async Task<ResponseDTO> EditEmployee(Employee employee)
        {
            return  new ResponseDTO(ResponseStatus.Success,"Employee Data Updated Succesfully", await _employeeService.UpdateEmployee(employee));
        }

        [HttpGet("GetEmployeeWithID")]
        [Authorize]
        public  ResponseDTO GetEmployeeWithID(Guid employeeId)
        {
            var employeeList =  _employeeService.GetEmployeesById(employeeId);
            if (employeeList != null)
            {
                return new ResponseDTO(ResponseStatus.Success, "", employeeList);
            }
            else
            {
                return new ResponseDTO(ResponseStatus.Error, "There are no employees found based on your searching criteria", employeeList);
            }
        }

        [HttpGet("GetEmployeeSelection")]
        [Authorize]
        //[TypeFilter(typeof(EPPAutorizeFilter),
        //    Arguments = new object[] { "View_Employee" })]
        public Task<IEnumerable<EmployeeDTO>> GetEmployeeSelection()
        {
            return _employeeService.GetSelections();
        }

        [HttpGet("GetEmployeeSelectionById")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "View_Employee" })]
        public Task<EmployeeDTO> GetEmployeeSelectionById(Guid employeeGuid)
        {
            return _employeeService.GetSelection(employeeGuid);
        }

        [HttpGet("GetEmployeeSelectionByEmail")]
        [Authorize]
       
        public async Task<Employee> GetEmployeeSelectionByEmail(string employeeEmail)
        {
            return  await _employeeService.GetEmployeesByEmailAsync(employeeEmail);
        }

        [HttpDelete("DeleteEmployee")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter), Arguments = new object[] { "Delete_Employee" })]
        public async Task<ResponseDTO> DeleteEmployee(Guid employeeId)
        {
            return await _employeeService.ChangeIsDeletedStatus(employeeId);
        }

        [HttpGet("GetReportingManagers")]
        [Authorize]
        public async Task<ResponseDTO> GetReportingManagers()
        {
            return await _employeeService.GetEmployeeListForReportingManager();
        }

        [HttpGet("FilterData")]
        [Authorize]
        public async Task<ResponseDTO> GetMenufilter()
        {
            return await _employeeService.GetFilterMenu();
        }

        [HttpGet("ReportFilterData")]
        //[Authorize]
        public async Task<ResponseDTO> GetReportFilter()
        {
            return await _employeeService.GetReportFilter();
        }

        [HttpGet("checkidnumber")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "Create_Employee" })]
        public async Task<bool> CheckIdNumber(string idNumber)
        {
            return await _employeeService.CheckIdNumber(idNumber);
        }

        [HttpGet("checkemail")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "Create_Employee,Update_Employee,Update_My_Profile" })]
        public async Task<ResponseDTO> CheckEmailAddressExistence(string email, Guid guid)
        {
            return await _employeeService.CheckEmailAddressExistence(email, Guid.Parse(guid.ToString()));
        }

        [HttpGet("checkphone")]
        [Authorize]
        [TypeFilter(typeof(EPPAutorizeFilter),
            Arguments = new object[] { "Create_Employee,Update_Employee,Update_My_Profile" })]
        public async Task<ResponseDTO> CheckPhoneNumberExistence(string phone, Guid? guid)
        {
            return await _employeeService.CheckPhoneNumberExistence(phone, (Guid)guid);
        }


        [HttpGet("checkDepartment")]
        [Authorize]
        public async Task<bool> CheckDepartment(string idNumber)
        {
            return await _employeeService.CheckDeptData(Guid.Parse(idNumber));
        }

        [HttpGet("checkRole")]
        [Authorize]
        public async Task<bool> CheckRole(string idNumber)
        {
            return await _employeeService.CheckRole(Guid.Parse(idNumber));
        }
        [HttpGet("checkCountry")]
        [Authorize]
        public async Task<bool> checkCountry(string idNumber)
        {
            return await _employeeService.CheckCountry(Guid.Parse(idNumber));
        }
        [HttpGet("checkDutyStation")]
        [Authorize]
        public async Task<bool> checkDutyStation(string idNumber)
        {
            return await _employeeService.CheckDutyStation(Guid.Parse(idNumber));
        }
        [HttpGet("GetEmpAssignmentsCounts")]
       // [Authorize]
        public async Task<ResponseDTO> GetEmployeesAssignmentsCounts() 
        {
            return await _employeeService.GetEmployeesCount();
        }

        [HttpGet("GetEmployeeAssignments")]
       // [Authorize]
        public async Task<PredicatedResponseDTO> GetEmployeeAssignments([FromQuery] EmpAssignmentSpecParams specParams)
        {
            return await _employeeService.GetEmployeeAssignments(specParams);
        }

    }
}
