using Excellerent.SharedModules.Interface.Service;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces
{
    public interface IPasswordResetTokenService: ICRUD<PasswordResetTokenEntity, PasswordResetToken>
    {
        Task<IEnumerable<PasswordResetTokenEntity>> GetPasswordResetTokenByUserId(Guid userGuid);
    }
}
