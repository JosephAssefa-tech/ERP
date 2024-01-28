using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Presentation.MailService
{
   public interface ISendMailService
    {
        Task<bool> NotifyAccountCreation(MailSenderOptions option, UserEntity user, String Referer);
        Task<bool> NotifyPasswordResetByAdmin(MailSenderOptions option, UserEntity user, String Referer);
        Task<bool> NotifyPasswordResetLink(MailSenderOptions option, UserEntity user,string resetLinkUr);
        Task<bool> NotifyPasswordResetAction(MailSenderOptions option, UserEntity user);
    }
}
