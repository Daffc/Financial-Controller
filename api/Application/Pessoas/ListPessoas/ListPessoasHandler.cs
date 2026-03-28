using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Application.Pessoas.ListPessoas;

public class ListPessoasHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public ListPessoasHandler(IPessoaRepository pessoaRepository, IUsuarioRepository usuarioRepository)
    {
        _pessoaRepository = pessoaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<ListPessoasResponse>> Handle(Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var pessoas = await _pessoaRepository.ListByUsuarioId(usuarioId);

        return pessoas.Select(p => new ListPessoasResponse
        {
            Id = p.Id,
            Nome = p.Nome,
            Idade = p.Idade
        }).ToList();
    }
}