using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinancialControllerServer.Application.Pessoas.CreatePessoa;

public class CreatePessoaRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(200, MinimumLength = 8, ErrorMessage = "Nome deve conter entre 8 e 200 caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Idade é obrigatória")]
    [Range(0, 150, ErrorMessage = "Idade deve estar entre 0 e 150")]
    public int? Idade { get; set; }
}