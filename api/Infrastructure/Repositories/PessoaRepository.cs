using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialControllerServer.Infrastructure.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _dbContext;

    public PessoaRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Pessoa pessoa)
    {
        await _dbContext.Pessoas.AddAsync(pessoa);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Pessoa>> ListByUsuarioId(Guid usuarioId)
    {
        return await _dbContext.Pessoas
            .Where(p => p.UsuarioId == usuarioId)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }
}