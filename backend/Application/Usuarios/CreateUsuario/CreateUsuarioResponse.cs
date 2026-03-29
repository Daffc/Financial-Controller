namespace FinancialControllerServer.Application.Usuarios.CreateUsuario;

public class CreateUsuarioResponse{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
}