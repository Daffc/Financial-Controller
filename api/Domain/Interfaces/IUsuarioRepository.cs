using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<bool> EmailExists(string email);
    Task Create(Usuario usuario);
    Task Save();
    Task<Usuario?> GetByEmail(string email);
    Task<Usuario?> GetById(Guid id);
}