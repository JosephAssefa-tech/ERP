using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Excellerent.UserManagement.Presentation.Helper
{
   public static class PasswordCryptographyPbkdf2
    {
        public static string HashPassword(string password, byte[] salt = null, bool needsOnlyHash = false)
        {
            if (salt == null || salt.Length != 16)
            {
                // generate a 128-bit salt using a secure PRNG
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (needsOnlyHash) return hashed;
            // password will be concatenated with salt using ':'
            return $"{hashed}:{Convert.ToBase64String(salt)}";
        }
        public static bool VerifyPassword(string hashedPassword, string passwordClaimed)
        {
            // retrieve both salt and password from 'hashedPasswordWithSalt'
            var passwordAndHash = hashedPassword.Split(':');
            if (passwordAndHash == null || passwordAndHash.Length != 2)
                return false;
           
            var salt = Convert.FromBase64String(passwordAndHash[1]);
            
            var hashOfpasswordToCheck = HashPassword(passwordClaimed, salt, true);
           
            return passwordAndHash[0] == hashOfpasswordToCheck;
            
        }
        public static string CreateToken()
        {
            string randomText = PasswordHelper.GenerateDefaultPassword();
           var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return  Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: randomText,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA256,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));
        }
    }
}

