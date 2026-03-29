using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;
using FinancialControllerServer.Application.Common.Interfaces;

namespace FinancialControllerServer.Application.Categorias.CreateCategoria;

public class CreateCategoriaHandler
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IUsuarioRepository _usuarioRepository;


    public CreateCategoriaHandler( ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository)
    {
        _categoriaRepository = categoriaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<CreateCategoriaResponse> Handle(CreateCategoriaRequest request, Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var categoria = new Categoria(
            request.Descricao!,
            request.Finalidade!.Value,
            usuarioId
        );

        await _categoriaRepository.Create(categoria);
        await _categoriaRepository.Save();

        return new CreateCategoriaResponse
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade
        };
    }
}