using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.DTOs
{
    public class EmployeeDTO 
    {
        public virtual Guid Guid { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedbyUserGuid { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string PhoneNumberPrefix { get; set; }
        public DateTime HiredDate { get; set; }

        public EmployeeDTO(Employee employee)
        {
            Guid = employee.Guid;
            IsActive = employee.IsActive;
            IsDeleted = employee.IsDeleted;
            CreatedDate = employee.CreatedDate;
            CreatedbyUserGuid = employee.CreatedbyUserGuid;
            Name = employee.FullName; // employee.FirstName + " " + (employee.FatherName != null ? employee.FatherName + " " : " ") + employee.GrandFatherName;
            Role = employee.EmployeeOrganization.Role.Name;
            Email = employee.EmployeeOrganization.CompaynEmail;
            PhoneNumberPrefix = employee.MobilePhone;
            HiredDate = employee.EmployeeOrganization.JoiningDate;
            if(employee.EmployeeOrganization!=null)
            {
                Role = employee.EmployeeOrganization.Role.Name;
                Email = employee.EmployeeOrganization.CompaynEmail;
                HiredDate = employee.EmployeeOrganization.JoiningDate;
                //ExtractPhone(employee.EmployeeOrganization.PhoneNumber);
            }
        }

        private void ExtractPhone(string phoneNumber)
        {
            PhoneNumberPrefix = phoneNumber;
            int phone;
            bool extracted = int.TryParse(phoneNumber.Substring(4), out phone);
        }
    }
}
