
using Excellerent.Usermanagement.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentaion.AuthTokenServices
{
    public interface IAuthTokenService
    {
        public string AuthToken(UserEntity userEntity);
    }
}
