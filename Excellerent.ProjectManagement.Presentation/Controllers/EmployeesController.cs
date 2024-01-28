using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Excellerent.ProjectManagement.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employee;
        public EmployeesController(IEmployeeService employee)
        {
            _employee = employee;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Employee> GetAll()
        {
            return _employee.GetEmployee();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public List<Employee> GetById(Guid id)
        {

            return _employee.GetEmployeeById(id);
        }
    }
}

   
