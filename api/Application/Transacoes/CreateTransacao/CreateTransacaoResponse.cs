using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Enums;

namespace FinancialControllerServer.Application.Transacoes.CreateTransacao;

public class CreateTransacaoResponse
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = default!;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
}