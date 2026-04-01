using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Enums;

namespace FinancialControllerServer.Application.Transacoes.ListTransacoes;

public class ListTransacoesRequest
{
    public DateOnly? DataInicio { get; set; }
    public DateOnly? DataFim { get; set; }

    [EnumDataType(typeof(TipoTransacao), ErrorMessage = "Tipo inválido")]
    public TipoTransacao? Tipo { get; set;}
    public Guid? PessoaId { get; set; }
    public Guid? CategoriaId { get; set; }
}