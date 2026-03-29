using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialControllerServer.Api.Common.Extensions;
using FinancialControllerServer.Application.Transacoes.CreateTransacao;
using FinancialControllerServer.Application.Transacoes.ListTransacoes;

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

    public TransacaoController(CreateTransacaoHandler createTransacaoHandler, ListTransacoesHandler listTransacoesHandler)
    {
        _createTransacaoHandler = createTransacaoHandler;
        _listTransacoesHandler = listTransacoesHandler; 
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria nova Transacao")]
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
    public async Task<IActionResult> List()
    {
        var usuarioId = User.GetUserId();
        var result = await _listTransacoesHandler.Handle(usuarioId);
        return Ok(result);
    }
}