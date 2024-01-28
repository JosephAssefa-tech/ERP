using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Excellerent.UserManagement.Presentaion.AuthTokenServices
{
    public class AuthTokenService: IAuthTokenService
    {
        public IUserRepository _repository { get; }
        private readonly IConfigurationSection _jwtConfig;

        public AuthTokenService(IConfigurationSection jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string AuthToken(UserEntity userEntity)
        {
            if (userEntity == null)
                return null;

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig["SecretKey"]));
            var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
            audience: _jwtConfig["Audience"],
            issuer: _jwtConfig["Issuer"],
            claims: new List<Claim>
            {
                new Claim("Email", userEntity.Email)
            },
            expires: DateTime.Now.AddMinutes(300000),
                signingCredentials: credentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
