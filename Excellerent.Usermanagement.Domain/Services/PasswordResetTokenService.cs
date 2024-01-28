using Excellerent.SharedModules.Services;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Services
{
    public  class PasswordResetTokenService : CRUD<PasswordResetTokenEntity, PasswordResetToken>, IPasswordResetTokenService
    {
        public IPasswordResetTokenRepository _repository { get; }
        public PasswordResetTokenService(IPasswordResetTokenRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PasswordResetTokenEntity>> GetPasswordResetTokenByUserId(Guid userGuid)
        {
            IEnumerable<PasswordResetTokenEntity> resetTokenEntities = new List<PasswordResetTokenEntity>();
            try
            {
                var resetTokenModels = await _repository.FindAsync(ut => ut.UserId == userGuid);

                if (resetTokenModels != null && resetTokenModels.Any())
                    resetTokenEntities = resetTokenModels.Select(t => new PasswordResetTokenEntity(t));
                return resetTokenEntities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<PasswordResetTokenEntity> FindOneToDelete(Guid id)
        {
           var model = await this._repository.FindOneAsyncForDelete(t => t.Guid == id);
            return model != null ? new PasswordResetTokenEntity(model) : null;
        }

    }
}
