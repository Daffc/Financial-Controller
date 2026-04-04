using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Domain.Interfaces;

public interface ITransacaoRepository
{
    Task Create(Transacao transacao);
    Task Save();
    Task<List<Transacao>> ListByUsuarioId(Guid usuarioId);
    Task<Transacao?> GetByIdAndUsuarioId(Guid transacaoId, Guid usuarioId);
    void Delete(Transacao transacao);
    Task<bool> ExistsReceitaByPessoaId(Guid pessoaId);
}