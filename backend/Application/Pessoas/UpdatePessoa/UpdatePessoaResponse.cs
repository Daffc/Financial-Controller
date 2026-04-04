namespace FinancialControllerServer.Application.Pessoas.UpdatePessoa;

public class UpdatePessoaResponse{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required int Idade { get; set; }
}