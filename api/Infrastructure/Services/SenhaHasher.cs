using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using FinancialControllerServer.Application.Common.Interfaces;
using FinancialControllerServer.Application.Common;

namespace FinancialControllerServer.Infrastructure.Services;

public class SenhaHasher : ISenhaHasher
{
    private readonly SecurityOptions _securityOptions;

    public SenhaHasher(IOptions<SecurityOptions> securityOptions)
    {
        _securityOptions = securityOptions.Value;
    }

    public string Hash(string senha)
    {
        var salt = RandomNumberGenerator.GetBytes(_securityOptions.SaltSize);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha + _securityOptions.SenhaPepper))
        {
            Salt = salt,
            DegreeOfParallelism = _securityOptions.DegreeOfParallelism,
            Iterations = _securityOptions.Iterations,
            MemorySize = _securityOptions.MemorySize
        };

        var hash = argon2.GetBytes(_securityOptions.HashSize);
        
        return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);
    }

    public bool Verify(string senha, string hash)
    {
        var parts = hash.Split('.');
        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = parts[1];

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha + _securityOptions.SenhaPepper))
        {
            Salt = salt,
            DegreeOfParallelism = _securityOptions.DegreeOfParallelism,
            Iterations = _securityOptions.Iterations,
            MemorySize = _securityOptions.MemorySize
        };

        var computedHash = Convert.ToBase64String(argon2.GetBytes(_securityOptions.HashSize));

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(computedHash),
            Convert.FromBase64String(storedHash)
        );
    }
}
