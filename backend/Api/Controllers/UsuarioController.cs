using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialControllerServer.Application.Usuarios.CreateUsuario;

namespace FinancialControllerServer.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
[Produces("application/json")]
[SwaggerTag("Endpoints de gerenciamento de Usuarios")]
public sealed class UsuarioController : ControllerBase
{
    private readonly CreateUsuarioHandler _createUsuarioHandler;

    public UsuarioController(CreateUsuarioHandler createUsuarioHandler)
    {
        _createUsuarioHandler = createUsuarioHandler;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Cria novo Usuario",
        Description = "Senha requirements:\n" +
                    "- 8-20 caracters\n" +
                    "- 1 letra maiúscula\n" +
                    "- 1 letra minúscula\n" +
                    "- 1 número\n" +
                    "- 1 caractere especial")]
    [SwaggerResponse(StatusCodes.Status200OK, "Usuario criado", typeof(CreateUsuarioResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Email já cadastrado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> Create ([FromBody] CreateUsuarioRequest request)
    {
        var result = await _createUsuarioHandler.Handle(request);
        return Ok(result);
    }
}