using System;

namespace Excellerent.UserManagement.Presentation.Models.GetModels
{
    public class LoginViewModel
    {
            public Guid Guid { get; set; }
            // public string FirstName { get; set; }
            // public string MiddleName { get; set; }
            // public string LastName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Token { get; set; }
            public Guid EmployeeId { get; set; }
    }

    }
