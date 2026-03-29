using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialControllerServer.Api.Common.Extensions;
using FinancialControllerServer.Application.Categorias.CreateCategoria;
using FinancialControllerServer.Application.Categorias.ListCategorias;

namespace FinancialControllerServer.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/categorias")]
[Produces("application/json")]
[SwaggerTag("Endpoints de gerenciamento de Categorias")]
public sealed class CategoriaController : ControllerBase
{
    private readonly CreateCategoriaHandler _createCategoriaHandler;
    private readonly ListCategoriasHandler _listCategoriasHandler;

    public CategoriaController(CreateCategoriaHandler createCategoriaHandler, ListCategoriasHandler listCategoriasHandler)
    {
        _createCategoriaHandler = createCategoriaHandler;
        _listCategoriasHandler = listCategoriasHandler;
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

    [HttpGet]
    [SwaggerOperation(Summary = "Lista Categorias atreladas a Usuario")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista recuperada", typeof(ListCategoriasResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> List()
    {
        var usuarioId = User.GetUserId();
        var result = await _listCategoriasHandler.Handle(usuarioId);
        return Ok(result);
    }
}