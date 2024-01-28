using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Specification;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Infrastructure.Repositories
{
    public  class PasswordResetTokenRepository: AsyncRepository<PasswordResetToken>, IPasswordResetTokenRepository
    {
        private readonly EPPContext _context;

        public PasswordResetTokenRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
        public new async Task<IEnumerable<PasswordResetToken>> FindAsync(Expression<Func<PasswordResetToken, bool>> criteria)
        {
            return (await GetQueryAsync()).AsNoTracking().Where(criteria);
        }


    }
}
