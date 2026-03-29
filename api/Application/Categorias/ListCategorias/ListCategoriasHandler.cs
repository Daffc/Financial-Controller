using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Application.Categorias.ListCategorias;

public class ListCategoriasHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public ListCategoriasHandler(ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository)
    {
        _categoriaRepository = categoriaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<ListCategoriasResponse>> Handle(Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var categorias = await _categoriaRepository.ListByUsuarioId(usuarioId);

        return categorias.Select(p => new ListCategoriasResponse
        {
            Id = p.Id,
            Descricao = p.Descricao,
            Finalidade = p.Finalidade
        }).ToList();
    }
}