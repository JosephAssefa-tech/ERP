using System;

namespace Excellerent.UserManagement.Presentation.Models.PostModel
{
    public class UserPostModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        // public string FirstName { get; set; }
        // public string MiddleName { get; set; }
        // public string LastName { get; set; }
        public string FullName { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid[] GroupIds { get;set; }
    }
}
