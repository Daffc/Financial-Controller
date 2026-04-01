namespace FinancialControllerServer.Application.Transacoes.ListTransacoes;

public class ListTransacoesResponse
{
    public Guid Id { get; set; }
    public required string Descricao { get; set; }
    public decimal Valor { get; set; }
    public int Tipo { get; set; }
    public DateOnly Data { get; set; }
    public required string PessoaNome { get; set; }
    public required string CategoriaDescricao { get; set; }
}