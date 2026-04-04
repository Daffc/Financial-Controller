using System.Threading.Tasks;
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

    public async Task<Pessoa?> GetByIdAndUsuarioId(Guid pessoaId, Guid usuarioId)
    {
        return await _dbContext.Pessoas
            .FirstOrDefaultAsync(p => p.Id == pessoaId && p.UsuarioId == usuarioId);
    }

    public void Delete(Pessoa pessoa)
    {
        _dbContext.Pessoas
            .Remove(pessoa);
    }

    public async Task<Pessoa?> GetByIdWithTransacoes(Guid pessoaId, Guid usuarioId)
    {
        return await _dbContext.Pessoas
            .Include(p => p.Transacoes)
            .FirstOrDefaultAsync(p => p.Id == pessoaId && p.UsuarioId == usuarioId);
    }
}