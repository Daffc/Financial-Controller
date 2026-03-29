using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialControllerServer.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _dbContext;

    public CategoriaRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Categoria categoria)
    {
        await _dbContext.Categorias.AddAsync(categoria);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Categoria>> ListByUsuarioId(Guid usuarioId)
    {
        return await _dbContext.Categorias
            .Where(p => p.UsuarioId == usuarioId)
            .OrderBy(p => p.Descricao)
            .ToListAsync();
    }
}