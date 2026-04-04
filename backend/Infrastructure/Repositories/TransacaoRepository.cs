using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Infrastructure.Persistence;
using FinancialControllerServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinancialControllerServer.Infrastructure.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _dbContext;

    public TransacaoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Transacao transacao)
    {
        await _dbContext.Transacoes.AddAsync(transacao);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Transacao>> ListByUsuarioId(Guid usuarioId)
    {
        return await _dbContext.Transacoes
            .Where(t => t.UsuarioId == usuarioId)
            .OrderByDescending(t => t.DataCriacao)
            .ToListAsync();
    }

    public async Task<Transacao?> GetByIdAndUsuarioId(Guid transacaoId, Guid usuarioId)
    {
        return await _dbContext.Transacoes
            .FirstOrDefaultAsync(t => t.Id == transacaoId && t.UsuarioId == usuarioId);
    }

    public void Delete(Transacao transacao)
    {
        _dbContext.Transacoes
            .Remove(transacao);
    }
    
    public async Task<bool> ExistsReceitaByPessoaId(Guid pessoaId)
    {
        return await _dbContext.Transacoes
            .AnyAsync(t => t.PessoaId == pessoaId && t.Tipo == TipoTransacao.RECEITA);
    }
}