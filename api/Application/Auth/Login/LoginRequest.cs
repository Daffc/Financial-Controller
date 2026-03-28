using System.ComponentModel.DataAnnotations;

namespace FinancialControllerServer.Application.Auth.Login;

public class LoginRequest
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string? Senha { get; set; }
}