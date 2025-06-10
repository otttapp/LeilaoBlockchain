using System.Security.Cryptography;

namespace Projeto_Aplicado_II_API.Infrastructure.Extensions
{
    public static class StringsExtensions
    {
        public static (byte[] passwordHash, byte[] saltHash) HashPassword(this string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);

            return (hashBytes, saltBytes);
        }

        public static bool VerifyPassword(this string informedPassword, byte[] storedPasswordBytes, byte[] saltBytes)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(informedPassword, saltBytes, 100_000, HashAlgorithmName.SHA256);

            var qtdBytes = 32;

            byte[] hashBytes = pbkdf2.GetBytes(qtdBytes);

            for (int i = 0; i < qtdBytes; i++)
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
