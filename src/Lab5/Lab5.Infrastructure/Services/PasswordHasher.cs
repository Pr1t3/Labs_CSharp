using Lab5.Application.Services;
using System.Security.Cryptography;
using System.Text;

namespace Lab5.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private static readonly byte[] FixedSalt = Encoding.UTF8.GetBytes("itmo_c#_2024");

    public string HashPassword(string password)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, FixedSalt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);
        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        string hashOfInput = HashPassword(password);
        return hashOfInput == hashedPassword;
    }
}