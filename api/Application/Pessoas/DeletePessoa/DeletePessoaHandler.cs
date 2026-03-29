using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Application.Pessoas.DeletePessoa;

public class DeletePessoaHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public DeletePessoaHandler(IPessoaRepository pessoaRepository, IUsuarioRepository usuarioRepository)
    {
        _pessoaRepository = pessoaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task Handle(Guid pessoaId, Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var pessoa = await _pessoaRepository.GetByIdAndUsuarioId(pessoaId, usuarioId);

        if (pessoa is null || pessoa.UsuarioId != usuarioId)
            throw new NotFoundException("Pessoa não encontrada");

        _pessoaRepository.Delete(pessoa);
        await _pessoaRepository.Save();
    }
}