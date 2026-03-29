using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialControllerServer.Api.Common.Extensions;
using FinancialControllerServer.Application.Pessoas.CreatePessoa;
using FinancialControllerServer.Application.Pessoas.ListPessoas;
using FinancialControllerServer.Application.Pessoas.DeletePessoa;

namespace FinancialControllerServer.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/pessoas")]
[Produces("application/json")]
[SwaggerTag("Endpoints de gerenciamento de Pessoas")]
public sealed class PessoaController : ControllerBase
{
    private readonly CreatePessoaHandler _createPessoaHandler;
    private readonly ListPessoasHandler _listPessoasHandler;
    private readonly DeletePessoaHandler _deletePessoaHandler;

    public PessoaController(CreatePessoaHandler createPessoaHandler, ListPessoasHandler listPessoasHandler, DeletePessoaHandler deletePessoaHandler)
    {
        _createPessoaHandler = createPessoaHandler;
        _listPessoasHandler = listPessoasHandler;
        _deletePessoaHandler = deletePessoaHandler;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria nova Pessoa")]
    [SwaggerResponse(StatusCodes.Status200OK, "Pessoa criada", typeof(CreatePessoaResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> Create([FromBody] CreatePessoaRequest request)
    {
        var usuarioId = User.GetUserId();
        var result = await _createPessoaHandler.Handle(request, usuarioId);
        return Ok(result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista Pessoas atreladas a Usuario")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista recuperada", typeof(ListPessoasResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> List()
    {
        var usuarioId = User.GetUserId();
        var result = await _listPessoasHandler.Handle(usuarioId);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Remove uma Pessoa")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Pessoa removida")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Pessoa não encontrada", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> Delete(Guid id)
    {
        var usuarioId = User.GetUserId();
        await _deletePessoaHandler.Handle(id, usuarioId);
        return NoContent();
    }
}