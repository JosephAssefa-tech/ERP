using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Entities
{
    public class PasswordResetTokenEntity : BaseEntity<PasswordResetToken>
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
       
        public PasswordResetTokenEntity(PasswordResetToken model) : base(model)
        {
            UserId = model.UserId;
            Token = model.Token;
            TokenExpiry = model.TokenExpiry;
        }

        public PasswordResetTokenEntity()
        {
        }

        public override PasswordResetToken MapToModel()
        {
            return new PasswordResetToken()
            {
                UserId = UserId,
                Token = Token,
                TokenExpiry = TokenExpiry,
                Guid = Guid
            };
        }

        public override PasswordResetToken MapToModel(PasswordResetToken t)
        {
            return t;
        }
    }
}
