using System.Security.Cryptography;

namespace Projeto_Aplicado_II_API.Infrastructure
{
    public static class StringsExtensions
    {
        private static readonly int iterations = 100_000;
        private static readonly int bytesAmount = 32;

        public static (byte[] passwordHash, byte[] saltHash) HashPassword(this string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            byte[] hashBytes;

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations, HashAlgorithmName.SHA256))
            {
                hashBytes = pbkdf2.GetBytes(bytesAmount);
            }

            return (hashBytes, saltBytes);
        }

        public static bool VerifyPassword(this string informedPassword, byte[] storedPasswordBytes, byte[] saltBytes)
        {
            byte[] hashBytes;

            using (var pbkdf2 = new Rfc2898DeriveBytes(informedPassword, saltBytes, iterations, HashAlgorithmName.SHA256))
            {
                hashBytes = pbkdf2.GetBytes(bytesAmount);
            }

            for (int i = 0; i < bytesAmount; i++)
            {
                if (hashBytes[i] != storedPasswordBytes[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
