using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Enums;

namespace FinancialControllerServer.Application.Categorias.CreateCategoria;

public class CreateCategoriaRequest
{
    [Required(ErrorMessage = "Descricao é obrigatória")]
    [StringLength(400, MinimumLength = 1, ErrorMessage = "Descricao deve conter entre 1 e 400 caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Finalidade é obrigatória")]
    [Range(1, 3, ErrorMessage = "Finalidade inválida")]
    public FinalidadeCategoria? Finalidade { get; set; }
}