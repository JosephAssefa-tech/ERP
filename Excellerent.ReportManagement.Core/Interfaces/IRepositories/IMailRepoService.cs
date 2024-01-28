using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Interfaces.IRepositories
{
    public interface IMailRepoService
    {
        Task<bool> SendEmail(MailMessage mail);
        Task<bool> SendEmail(IEnumerable<MailMessage> mail);
    }
}
