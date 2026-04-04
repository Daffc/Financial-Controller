using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Application.Pessoas.UpdatePessoa;

public class UpdatePessoaHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ITransacaoRepository _transacaoRepository;

    public UpdatePessoaHandler(IPessoaRepository pessoaRepository, IUsuarioRepository usuarioRepository, ITransacaoRepository transacaoRepository)
    {
        _pessoaRepository = pessoaRepository;
        _usuarioRepository = usuarioRepository;
        _transacaoRepository = transacaoRepository;
    }

    public async Task Handle(UpdatePessoaRequest request, Guid usuarioId)
    {
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var pessoa = await _pessoaRepository.GetByIdWithTransacoes(request.Id!.Value, usuarioId);
        if (pessoa is null)
            throw new NotFoundException("Pessoa não encontrada");

        var haveReceita = await _transacaoRepository.ExistsReceitaByPessoaId(pessoa.Id);
        pessoa.Update(request.Nome!, request.Idade!.Value, haveReceita);

        await _pessoaRepository.Save();
    }
}