using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialControllerServer.Api.Common.Extensions;
using FinancialControllerServer.Application.Transacoes.CreateTransacao;
using FinancialControllerServer.Application.Transacoes.ListTransacoes;
using FinancialControllerServer.Application.Transacoes.DeleteTransacao;

namespace FinancialControllerServer.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/transacoes")]
[Produces("application/json")]
[SwaggerTag("Endpoints de gerenciamento de Transacaos")]
public sealed class TransacaoController : ControllerBase
{
    private readonly CreateTransacaoHandler _createTransacaoHandler;
    private readonly ListTransacoesHandler _listTransacoesHandler;
    private readonly DeleteTransacaoHandler _deleteTransacaoHandler;

    public TransacaoController(CreateTransacaoHandler createTransacaoHandler, ListTransacoesHandler listTransacoesHandler, DeleteTransacaoHandler deleteTransacaoHandler)
    {
        _createTransacaoHandler = createTransacaoHandler;
        _listTransacoesHandler = listTransacoesHandler;
        _deleteTransacaoHandler = deleteTransacaoHandler;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria nova Transação")]
    [SwaggerResponse(StatusCodes.Status200OK, "Transacao criada", typeof(CreateTransacaoResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> Create([FromBody] CreateTransacaoRequest request)
    {
        var usuarioId = User.GetUserId();
        var result = await _createTransacaoHandler.Handle(request, usuarioId);
        return Ok(result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista Transações do usuário")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista recuperada", typeof(List<ListTransacoesResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno", typeof(ApiErrorResponse))]
    public async Task<IActionResult> List([FromQuery] ListTransacoesRequest request)
    {
        var usuarioId = User.GetUserId();
        var result = await _listTransacoesHandler.Handle(request, usuarioId);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Remove uma Transação")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Transação removida")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autenticado", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Pessoa não encontrada", typeof(ApiErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(ApiErrorResponse))]
    public async Task<IActionResult> Delete(Guid id)
    {
        var usuarioId = User.GetUserId();
        await _deleteTransacaoHandler.Handle(id, usuarioId);
        return NoContent();
    }
}