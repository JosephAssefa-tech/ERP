namespace Excellerent.ApplicantTracking.Presentation.Dtos
{
    public class ApplicantPostModelDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; internal set; }
        public string ProfileImage { get; set; }
        public string ResumeFile { get; set; }

    }
}