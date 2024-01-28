using System;

namespace Excellerent.ApplicantTracking.Presentation.Modlels.ViewModels
{
    public class SigninViewModel
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public string ProfileImage { get; set; }
    }

}
