using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Domain.Interfaces;

public interface IPessoaRepository
{
    Task Create(Pessoa pessoa);
    Task Save();
    Task<List<Pessoa>> ListByUsuarioId(Guid usuarioId);
}