using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task Create(Categoria categoria);
    Task Save();
    Task<List<Categoria>> ListByUsuarioId(Guid usuarioId);
    Task<Categoria?> GetByIdAndUsuarioId(Guid categoriaId, Guid usuarioId);
}