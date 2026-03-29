using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task Create(Categoria categoria);
    Task Save();
}