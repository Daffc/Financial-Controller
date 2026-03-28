namespace FinancialControllerServer.Application.Pessoas.CreatePessoa;

public class CreatePessoaResponse{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required int Idade { get; set; }
}