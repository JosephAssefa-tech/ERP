using Excellerent.SharedModules.Seed;

namespace Excellerent.ApplicantTracking.Domain.Models
{
    public class Applicant : BaseAuditModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string ResumeUpload { get; set; }

        public string ProfilePictureUpload { get; set; }


        public string Status { get; set; }




    }
}
