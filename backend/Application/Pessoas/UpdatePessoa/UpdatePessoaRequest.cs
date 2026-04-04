using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinancialControllerServer.Application.Pessoas.UpdatePessoa;

public class UpdatePessoaRequest
{
    [Required(ErrorMessage = "Id é obrigatório")]
    public Guid? Id;
    
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Nome deve conter entre 1 e 200 caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Idade é obrigatória")]
    [Range(0, 150, ErrorMessage = "Idade deve estar entre 0 e 150")]
    public int? Idade { get; set; }
}