namespace Excellerent.UserManagement.Presentation.Models.GetModels
{
    public class MailModel
    {
       public string UserName { get; set; }
       public string DisplayName { get; set; }
       public string Email { get; set; }
       public string Content { get; set; }
       public MailSenderOptions Smtp { get; set; }

    }

    public class MailSenderOptions
    {
        public string DisplayName { get; set; }
        public string Server { get; set; }
        public int Port { get; set;}
        public string FromAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RequestInfo { get; internal set; }
    }
    public enum UserNotificationType
    {
        AccountCreated,
        PasswordReset
    }

}
