using Excellerent.ReportManagement.Core.Interfaces.IRepositories;
using Excellerent.SharedModules.Helpers;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Infrastructure.RepoServices
{
    public class MailRepoService : IMailRepoService
    {
        private readonly IOptions<MailConfigurationOptions> _smtpOption;

        public MailRepoService(
            IOptions<MailConfigurationOptions> smtpOption
            )
        {
            _smtpOption = smtpOption;
        }

        public async Task<bool> SendEmail(MailMessage mail)
        {
            var options = _smtpOption.Value;
            mail.From = new MailAddress(_smtpOption.Value.FromAddress, "Excellerent");

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpOption.Value.UserName, _smtpOption.Value.Password);
            client.Port = _smtpOption.Value.Port;
            client.Host = _smtpOption.Value.Server;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            await client.SendMailAsync(mail);
            mail.Dispose();
            client.Dispose();
            return true;
        }

        public async Task<bool> SendEmail(IEnumerable<MailMessage> mails)
        {
            var success = true;
            foreach (var mail in mails)
            {
                success = await SendEmail(mail);
            }
            return success;
        }

    }
}
