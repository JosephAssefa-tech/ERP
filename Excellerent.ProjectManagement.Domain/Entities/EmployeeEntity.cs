using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ProjectManagement.Domain.Entities
{
    public class EmployeeEntity : BaseEntity<Employee>
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string PhoneNumberPrefix { get; set; }

        public override Employee MapToModel()
        {
            throw new NotImplementedException();
        }

        public override Employee MapToModel(Employee t)
        {
            throw new NotImplementedException();
        }
    }
}
