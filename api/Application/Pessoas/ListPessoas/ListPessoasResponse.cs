namespace FinancialControllerServer.Application.Pessoas.ListPessoas;

public class ListPessoasResponse
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required int Idade { get; set; }
}