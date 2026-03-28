using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Application.Pessoas.CreatePessoa;

public class CreatePessoaHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public CreatePessoaHandler(IPessoaRepository pessoaRepository, IUsuarioRepository usuarioRepository)
    {
        _pessoaRepository = pessoaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<CreatePessoaResponse> Handle(CreatePessoaRequest request, Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var pessoa = new Pessoa(
            request.Nome,
            request.Idade,
            usuarioId
        );

        await _pessoaRepository.Create(pessoa);
        await _pessoaRepository.Save();

        return new CreatePessoaResponse
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };
    }
}