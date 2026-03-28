using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialControllerServer.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _dbContext;

    public UsuarioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task <bool> EmailExists(string email)
    {
        return await _dbContext.Usuarios.AnyAsync(x => x.Email == email);
    }

    public async Task Create(Usuario usuario)
    {
        await _dbContext.Usuarios.AddAsync(usuario);
    } 

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task <Usuario?> GetByEmail(string email)
    {
        return await _dbContext.Usuarios
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
    }
}