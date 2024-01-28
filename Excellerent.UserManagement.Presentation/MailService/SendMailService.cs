using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Presentation.MailService;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Services
{
    public class SendMailService : ISendMailService
    {
        public async Task<bool> NotifyAccountCreation(MailSenderOptions option, UserEntity user, String Referer)
        {
            try
            {

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(user.Email, $"{user.FullName} ")); // {user.FirstName} {user.LastName}
                msg.From = new MailAddress(option.FromAddress, "Excellerent");
                msg.Subject = "Your Excellerent EPP Account";
                string body = string.Empty;

                StringBuilder s = new StringBuilder();
               
                    s.Append($"<h1>Welcome to Excellerent Enterprise Project Portfolio!</h1>");
                    s.Append($"Dear <b><em>{user.FullName}</b>,</em>"); // {user.FirstName} {user.LastName}
                    s.Append($"<p>You have been invited to <a href='"+ Referer +"'>Enterprise Project Porfolio</a>. This email includes your account details, so keep it safe!</p>");
                    s.Append($"<p></p>");
                    s.Append($"<dd>User name: <em>{user.Email}</em></dd><br />");
                    s.Append($"<dd>Password: <em>{user.Password}</em></dd>");
                    s.Append($"<p></p>");


                msg.Body = s.ToString();
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(option.UserName, option.Password);
                client.Port = option.Port;
                client.Host = option.Server;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                await client.SendMailAsync(msg);
                msg.Dispose();
                client.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public Task<bool> NotifyPasswordResetAction(MailSenderOptions option, UserEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> NotifyPasswordResetByAdmin(MailSenderOptions option, UserEntity user, String Referer)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(user.Email, $"{user.FullName} ")); // {user.FirstName} {user.LastName}
                msg.From = new MailAddress(option.FromAddress, "Excellerent");
                msg.Subject = "Your Excellerent EPP Account";
                string body = string.Empty;

                StringBuilder s = new StringBuilder();
                
                    s.Append($"<h2>Enterprise Project Portfolio(EPP) - Password reset notification</h2>");
                    s.Append($"Dear <b><em>{user.FullName}</em>,</b>"); // {user.FirstName} {user.LastName}
                    s.Append($"<p>The password for your <a href='"+Referer+"'>Enterprise Project Porfolio</a> account ({user.Email}) is reset.</p>");
                    s.Append($"<p></p>");
                    s.Append($"<dd>New password: <em>{user.Password}</em></dd>");
                    s.Append($"<p></p>");

                msg.Body = s.ToString();
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(option.UserName, option.Password);
                client.Port = option.Port;
                client.Host = option.Server;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                await client.SendMailAsync(msg);
                msg.Dispose();
                client.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> NotifyPasswordResetLink(MailSenderOptions option, UserEntity user, string url)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(user.Email, $"{user.FullName} ")); // {user.FirstName} {user.LastName}
                msg.From = new MailAddress(option.FromAddress, "Excellerent");
                msg.Subject = "Password Reset Request";
                string body = string.Empty;

                StringBuilder s = new StringBuilder();
                s.Append($"<h2>Enterprise Project Portfolio(EPP)</h2>");
                s.Append($"<h4>Need to reset your password?</h4>");
                s.Append($"Dear <b><em>{user.FullName},</em></b>"); // {user.FirstName} {user.LastName}
                s.Append($"<p>We received password reset request for your Excellerent Enterprise Project Portfolio account.</p>");
                s.Append($"<p>You can reset your password by clicking the button below.<br />");
                s.Append($"<a href={url} style='background-color: #00A551; font-size: 14px; font-family: Helvetica, Arial, sans-serif; font-weight: 400; text-decoration: none; padding: 14px 20px; color: #ffffff; border-radius: 5px; display: inline-block; mso-padding-alt: 0;'>");
                s.Append($"<span style='mso-text-raise: 15pt;'>Reset Password</span></a></p>");
                s.Append($"<p>Or copy and paste the URL into your browser. <br /> ");
                s.Append($"<a href={url}>{url}</a></p>");
                s.Append($"<p></p>");
                s.Append($"<p>If you did not make this request, please ignore the request or reply to let us know. This password reset link is only valid for the next 30 minutes. </p>");
                s.Append($"<p></p>");
                s.Append($"<p></p>");
                s.Append($"<p></p>");

                s.Append($"<p style='font-size: x-small'>Request Info: {option.RequestInfo}</p>");
                msg.Body = s.ToString();
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(option.UserName, option.Password);
                client.Port = option.Port;
                client.Host = option.Server;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                await client.SendMailAsync(msg);
                msg.Dispose();
                client.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
