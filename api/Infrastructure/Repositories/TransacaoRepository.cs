using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Infrastructure.Persistence;
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
}