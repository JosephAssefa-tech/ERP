using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellerent.UserManagement.Presentation.Models.GetModels
{
    public class UserGetModel
    {
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        // public string FirstName { get; set; }
        // public string MiddleName { get; set; }
        // public string LastName { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public DateTime LastActivityDate { get; set; }
        public  EmployeeEntity Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
