using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPasswordResetTokenRepository : IAsyncRepository<PasswordResetToken>
    {
        new Task<IEnumerable<PasswordResetToken>> FindAsync(Expression<Func<PasswordResetToken, bool>> criteria);
    }
}
