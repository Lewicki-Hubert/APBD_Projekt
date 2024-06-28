using Projekt.Exceptions;

namespace Projekt.Utilities
{
    public static class PasswordSecurity
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static void CheckPassword(string password, string hashedPassword)
        {
            if (!BCrypt.Net.BCrypt.Verify(password, hashedPassword))
            {
                throw new UnauthenticatedException("Provided password is incorrect.");
            }
        }
    }
}