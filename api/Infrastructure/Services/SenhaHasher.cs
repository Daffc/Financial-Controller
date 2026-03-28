using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using FinancialControllerServer.Application.Common.Cryptography;
using FinancialControllerServer.Application.Common.Interfaces;

namespace FinancialControllerServer.Infrastructure.Services;

public class SenhaHasher : ISenhaHasher
{
    private readonly PasswordHashOptions _passwordHashOptions;

    public SenhaHasher(IOptions<PasswordHashOptions> passwordHashOptions)
    {
        _passwordHashOptions = passwordHashOptions.Value;
    }

    public string Hash(string senha)
    {
        var salt = RandomNumberGenerator.GetBytes(_passwordHashOptions.SaltSize);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha + _passwordHashOptions.Pepper))
        {
            Salt = salt,
            DegreeOfParallelism = _passwordHashOptions.DegreeOfParallelism,
            Iterations = _passwordHashOptions.Iterations,
            MemorySize = _passwordHashOptions.MemorySize
        };

        var hash = argon2.GetBytes(_passwordHashOptions.HashSize);
        
        return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);
    }

    public bool Verify(string senha, string hash)
    {
        var parts = hash.Split('.');
        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = parts[1];

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha + _passwordHashOptions.Pepper))
        {
            Salt = salt,
            DegreeOfParallelism = _passwordHashOptions.DegreeOfParallelism,
            Iterations = _passwordHashOptions.Iterations,
            MemorySize = _passwordHashOptions.MemorySize
        };

        var computedHash = Convert.ToBase64String(argon2.GetBytes(_passwordHashOptions.HashSize));

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(computedHash),
            Convert.FromBase64String(storedHash)
        );
    }
}
