using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Application.Transacoes.ListTransacoes;

public class ListTransacoesHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITransacaoRepository _transacaoRepository;

    public ListTransacoesHandler(
        ITransacaoRepository transacaoRepository,
        IUsuarioRepository usuarioRepository)
    {
        _transacaoRepository = transacaoRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<ListTransacoesResponse>> Handle(Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var transacoes = await _transacaoRepository.ListByUsuarioId(usuarioId);

        return transacoes.Select(t => new ListTransacoesResponse
        {
            Id = t.Id,
            Descricao = t.Descricao,
            Valor = t.Valor,
            Tipo = (int)t.Tipo,
            PessoaId = t.PessoaId,
            CategoriaId = t.CategoriaId
        }).ToList();
    }
}