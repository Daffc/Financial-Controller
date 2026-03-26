using System.ComponentModel.DataAnnotations;

namespace FinancialControllerServer.Application.Usuarios.CreateUsuario;

public class CreateUsuarioRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(200, MinimumLength = 8, ErrorMessage = "Nome deve conter entre 8 e 200 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [StringLength(255, ErrorMessage = "Email deve conter no máximo 255 caracteres")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$",
        ErrorMessage = "A senha deve ter entre 8 e 20 caracteres com pelo menos 1 maiúsculo, 1 minúsculo, 1 número e 1 caractere especial")]
    public string Senha { get; set; }
}