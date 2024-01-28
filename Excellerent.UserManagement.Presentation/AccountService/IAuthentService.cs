
using Excellerent.Usermanagement.Domain.Entities;

namespace Excellerent.UserManagement.Presentation.AccoutService
{
    public interface IAuthentService
    {
        public string Authenticate(UserEntity userEntity);
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);

    }
}
