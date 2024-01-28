using Excellerent.ApplicantTracking.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Excellerent.ApplicantTracking.Presentation.AccoutService
{
    public class AuthService : IAuthService
    {
        private readonly IConfigurationSection _jwtConfig;

        public AuthService(IConfigurationSection jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string Authenticate(ApplicantEntity applicantEntity)
        {
            if (applicantEntity == null)
                return null;
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig["SecretKey"]));
            var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
            audience: _jwtConfig["Audience"],
            issuer: _jwtConfig["Issuer"],
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string HashPassword(string password)
        {
            return BC.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BC.Verify(password, hashedPassword);
        }
    }
}
