using System;

namespace Models
{
    public class BCryptUtilities
    {
        public static string encodePassword(string password){
            string newSalt = BCrypt.Net.BCrypt.GenerateSalt(10);
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(password, newSalt);

            return hashPassword;
        }
        public static bool passwordMatch(string password, string hashPassword){
            if(BCrypt.Net.BCrypt.Verify(password, hashPassword))
                return true;
            else
                return false;
        }

    }
}