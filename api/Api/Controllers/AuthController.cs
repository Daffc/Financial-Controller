using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialControllerServer.Application.Auth.Login;

namespace FinancialControllerServer.Api.Controllers;

[ApiController]
[Route("api/auth")]
[Produces("application/json")]
[SwaggerTag("Endpoints de gerenciamento de Autenticação de Usuario")]
public sealed class AuthController : ControllerBase
{
    private readonly LoginHandler _loginHandler;

    public AuthController( LoginHandler loginHandler)
    {
        _loginHandler = loginHandler;
    }

    [HttpPost]
    [SwaggerOperation( Summary = "Autentica usuário e retorna token JWT")]
    [SwaggerResponse(StatusCodes.Status200OK, "Autenticado com sucesso", typeof(LoginResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação | Credenciais inválidas", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> Login ([FromBody] LoginRequest request)
    {
        var result = await _loginHandler.Handle(request);
        return Ok(result);
    }
}