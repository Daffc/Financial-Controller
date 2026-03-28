using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialControllerServer.Api.Common.Extensions;
using FinancialControllerServer.Application.Pessoas.CreatePessoa;

namespace FinancialControllerServer.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/pessoas")]
[Produces("application/json")]
[SwaggerTag("Endpoints de gerenciamento de Pessoas")]
public sealed class PessoaController : ControllerBase
{
    private readonly CreatePessoaHandler _createPessoaHandler;

    public PessoaController(CreatePessoaHandler createPessoaHandler)
    {
        _createPessoaHandler = createPessoaHandler;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria nova Pessoa")]
    [SwaggerResponse(StatusCodes.Status200OK, "Pessoa criada", typeof(CreatePessoaResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> Create ([FromBody] CreatePessoaRequest request)
    {
        var usuarioId = User.GetUserId();
        var result = await _createPessoaHandler.Handle(request, usuarioId);
        return Ok(result);
    }
}