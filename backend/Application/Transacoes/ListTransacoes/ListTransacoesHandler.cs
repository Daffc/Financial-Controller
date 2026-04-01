using Microsoft.EntityFrameworkCore;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;
using FinancialControllerServer.Infrastructure.Persistence;

namespace FinancialControllerServer.Application.Transacoes.ListTransacoes;

public class ListTransacoesHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly AppDbContext _dbContex;

    public ListTransacoesHandler(IUsuarioRepository usuarioRepository, AppDbContext dbContex)
    {
        _usuarioRepository = usuarioRepository;
        _dbContex = dbContex;
    }

    public async Task<List<ListTransacoesResponse>> Handle(ListTransacoesRequest request, Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");


        var query = _dbContex.Transacoes
            .AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId);

        // FILTERS
        if(request.DataInicio.HasValue)
            query = query.Where(t => t.Data >= request.DataInicio.Value);
        if(request.DataFim.HasValue)
            query = query.Where(t => t.Data <= request.DataFim.Value);
        if(request.Tipo.HasValue)
            query = query.Where(t => t.Tipo == request.Tipo.Value);
        if(request.PessoaId.HasValue)
            query = query.Where(t => t.PessoaId == request.PessoaId.Value);
        if(request.CategoriaId.HasValue)
            query = query.Where(t => t.CategoriaId == request.CategoriaId.Value);

        var result = await query
            .OrderByDescending(t => t.Data)
            .Select(t => new ListTransacoesResponse
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = (int)t.Tipo,
                Data = t.Data,
                PessoaNome = t.Pessoa!.Nome,
                CategoriaDescricao = t.Categoria!.Descricao
            })
            .ToListAsync();

        return result;
    }
}