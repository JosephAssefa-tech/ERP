using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ProjectManagement.Domain.Models
{
    public class Employee : BaseAuditModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string PhoneNumberPrefix { get; set; }
        public DateTime HiredDate { get; set; }

        public Employee(Employee employee)
        {
            Name = employee.Name;
            Role = employee.Role;
            Email = employee.Email;
            Phone = employee.Phone;
            PhoneNumberPrefix = employee.PhoneNumberPrefix;

            HiredDate = employee.HiredDate;
        }
        public Employee()
        {

        }
        public Employee(Guid guid,string name,string role, string email,int phone,string phoneNumberPrefix,DateTime hiredDate)
        {
            Guid = guid;
            Name = name;
            Role = role;
            Email = email;
            Phone = phone;
            HiredDate = hiredDate;
            PhoneNumberPrefix = phoneNumberPrefix;
        }
    }
}
