using Excellerent.ApplicantTracking.Domain.Entities;

namespace Excellerent.ApplicantTracking.Presentation.AccoutService
{
    public interface IAuthService
    {
        public string Authenticate(ApplicantEntity applicantEntity);
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);

    }
}
