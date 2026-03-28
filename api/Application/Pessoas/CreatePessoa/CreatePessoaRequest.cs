using System.ComponentModel.DataAnnotations;

namespace FinancialControllerServer.Application.Pessoas.CreatePessoa;

public class CreatePessoaRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(200, MinimumLength = 8, ErrorMessage = "Nome deve conter entre 8 e 200 caracteres")]
    public required string Nome { get; set; }

    [Range(0, 150, ErrorMessage = "Idade inválida")]
    public required int Idade { get; set; }
}