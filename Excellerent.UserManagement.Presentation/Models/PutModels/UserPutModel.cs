using System;

namespace Excellerent.UserManagement.Presentation.Models.PutModels
{
    public class UserPutModel
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        // public string FirstName { get; set; }
        // public string MiddleName { get; set; }
        // public string LastName { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
    }
}
