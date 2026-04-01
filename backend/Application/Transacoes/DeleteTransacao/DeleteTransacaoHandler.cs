using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Application.Transacoes.DeleteTransacao;

public class DeleteTransacaoHandler
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public DeleteTransacaoHandler(ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository)
    {
        _transacaoRepository = transacaoRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task Handle(Guid transacaoId, Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var transacao = await _transacaoRepository.GetByIdAndUsuarioId(transacaoId, usuarioId);

        if (transacao is null || transacao.UsuarioId != usuarioId)
            throw new NotFoundException("Transação não encontrada");

        _transacaoRepository.Delete(transacao);
        await _transacaoRepository.Save();
    }
}