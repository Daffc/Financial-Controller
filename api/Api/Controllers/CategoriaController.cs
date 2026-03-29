using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialControllerServer.Api.Common.Extensions;
using FinancialControllerServer.Application.Categorias.CreateCategoria;

namespace FinancialControllerServer.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/categorias")]
[Produces("application/json")]
[SwaggerTag("Endpoints de gerenciamento de Categorias")]
public sealed class CategoriaController : ControllerBase
{
    private readonly CreateCategoriaHandler _createCategoriaHandler;

    public CategoriaController(CreateCategoriaHandler createCategoriaHandler)
    {
        _createCategoriaHandler = createCategoriaHandler;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria nova Categoria")]
    [SwaggerResponse(StatusCodes.Status200OK, "Categoria criada", typeof(CreateCategoriaResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> Create([FromBody] CreateCategoriaRequest request)
    {
        var usuarioId = User.GetUserId();
        var result = await _createCategoriaHandler.Handle(request, usuarioId);
        return Ok(result);
    }
}