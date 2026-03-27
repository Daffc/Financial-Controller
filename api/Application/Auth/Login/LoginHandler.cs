using FinancialControllerServer.Application.Auth.Login;
using FinancialControllerServer.Application.Common.Interfaces;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;
using FinancialControllerServer.Infrastructure.Repositories;



public class LoginHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISenhaHasher _senhaHasher;
    private readonly ITokenService _tokenService;

    public LoginHandler(IUsuarioRepository usuarioRepository, ISenhaHasher senhaHasher, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _senhaHasher = senhaHasher;
        _tokenService = tokenService;   
    }

    public async Task<LoginResponse> Handle(LoginRequest request)
    {
        var usuario = await _usuarioRepository.GetByEmail(request.Email);

        if(usuario is null)
            throw new BadRequestException("Credenciais inválidas");

        var valid = _senhaHasher.Verify(request.Senha, usuario.SenhaHash);

        if (!valid)
            throw new BadRequestException("Credenciais inválidas");

        var token = _tokenService.GenerateToken(usuario);

        return new LoginResponse
        {
            Token = token
        };
    }
}