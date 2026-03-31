
using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Enums;

namespace FinancialControllerServer.Application.Transacoes.CreateTransacao;

public class CreateTransacaoRequest
{
    [Required(ErrorMessage = "Descricao é obrigatória")]
    [StringLength(400, MinimumLength = 1, ErrorMessage = "Descricao deve conter entre 1 e 400 caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Valor é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
    public decimal? Valor { get; set; }

    [Required(ErrorMessage = "Tipo é obrigatório")]
    [EnumDataType(typeof(TipoTransacao), ErrorMessage = "Tipo inválido")]
    public TipoTransacao? Tipo { get; set; }
    
    [Required(ErrorMessage = "Data é obrigatória")]
    public DateOnly? Data { get; set; }

    [Required(ErrorMessage = "PessoaId é obrigatório")]
    public Guid? PessoaId { get; set; }
    
    [Required(ErrorMessage = "CategoriaId é obrigatório")]
    public Guid? CategoriaId { get; set; }
}