using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Domain.Interfaces;

public interface IPessoaRepository
{
    Task Create(Pessoa pessoa);
    Task Save();
    Task<List<Pessoa>> ListByUsuarioId(Guid usuarioId);
    Task<Pessoa?> GetByIdAndUsuarioId(Guid pessoaId, Guid usuarioId);
    void Delete(Pessoa pessoa);
    Task<Pessoa?> GetByIdWithTransacoes(Guid pessoaId, Guid usuarioId);
}