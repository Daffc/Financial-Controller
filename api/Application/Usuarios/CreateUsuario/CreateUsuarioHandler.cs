using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Application.Common.Interfaces;

namespace FinancialControllerServer.Application.Usuarios.CreateUsuario;

public class CreateUsuarioHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISenhaHasher _senhaHasher;

    public CreateUsuarioHandler( IUsuarioRepository usuarioRepository, ISenhaHasher senhaHasher)
    {
        _usuarioRepository = usuarioRepository;
        _senhaHasher = senhaHasher;
    }

    public async Task<CreateUsuarioResponse> Handle(CreateUsuarioRequest request)
    {
        var exists = await _usuarioRepository.EmailExists(request.Email);
        if (exists)
            throw new Exception("Email já cadastrado");

        var senhaHash = _senhaHasher.Hash(request.Senha);
        var usuario = new Usuario(request.Nome, request.Email, senhaHash);

        await _usuarioRepository.Create(usuario);
        await _usuarioRepository.Save();

        return new CreateUsuarioResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }
}