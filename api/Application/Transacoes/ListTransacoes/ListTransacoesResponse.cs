namespace FinancialControllerServer.Application.Transacoes.ListTransacoes;

public class ListTransacoesResponse
{
    public Guid Id { get; set; }
    public required string Descricao { get; set; }
    public decimal Valor { get; set; }
    public int Tipo { get; set; }
    public Guid PessoaId { get; set; }
    public Guid CategoriaId { get; set; }
}