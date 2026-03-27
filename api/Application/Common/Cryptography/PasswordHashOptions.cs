namespace FinancialControllerServer.Application.Common.Cryptography;

public class PasswordHashOptions
{
    public string SenhaPepper { get; set; }
    public int DegreeOfParallelism { get; set; }
    public int Iterations { get; set; }
    public int MemorySize { get; set; }
    public int SaltSize { get; set; }
    public int HashSize { get; set; }
}
