using Excellerent.ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface
{
    public interface IEmployeeService
    {
        public IEnumerable<Employee> GetEmployee();
        public List<Employee> GetEmployeeById(Guid id);
    }
}
