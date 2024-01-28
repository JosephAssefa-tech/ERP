using System;

namespace Excellerent.UserManagement.Presentation.Helper
{
    public static class PasswordHelper
    {
        public static string GenerateDefaultPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ123456789";
            string validNums = "123456789";
            string validSpecialChars = "@*#$";
            Random random = new Random();
            char[] chars = new char[length];
            chars[0] = validNums[random.Next(0, validNums.Length)];

            for (int i = 1; i < length-1; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            chars[length-1] = validSpecialChars[random.Next(0, validSpecialChars.Length)];
            return new string(chars);
        }
    }
}
