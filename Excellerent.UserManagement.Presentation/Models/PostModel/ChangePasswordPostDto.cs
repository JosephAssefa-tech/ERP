
namespace Excellerent.UserManagement.Presentation.Models.PostModel
{
    public class ChangePasswordPostDto
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
