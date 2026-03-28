namespace FinancialControllerServer.Application.Common.Cryptography;

public class PasswordHashOptions
{
    public required string Pepper { get; set; }
    public required int DegreeOfParallelism { get; set; }
    public required int Iterations { get; set; }
    public required int MemorySize { get; set; }
    public required int SaltSize { get; set; }
    public required int HashSize { get; set; }
}
