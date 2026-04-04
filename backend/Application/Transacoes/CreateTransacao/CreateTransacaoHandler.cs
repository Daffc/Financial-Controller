using FinancialControllerServer.Domain.Entities;
using FinancialControllerServer.Domain.Interfaces;
using FinancialControllerServer.Domain.Exceptions;
using FinancialControllerServer.Domain.Enums;

namespace FinancialControllerServer.Application.Transacoes.CreateTransacao;

public class CreateTransacaoHandler
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ICategoriaRepository _categoriaRepository;


    public CreateTransacaoHandler(ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository, IPessoaRepository pessoaRepository, ICategoriaRepository categoriaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _usuarioRepository = usuarioRepository;
        _pessoaRepository = pessoaRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<CreateTransacaoResponse> Handle(CreateTransacaoRequest request, Guid usuarioId)
    {
        // VALIDATING REFERENCES
        var usuario = await _usuarioRepository.GetById(usuarioId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        var pessoa = await _pessoaRepository.GetByIdAndUsuarioId(request.PessoaId!.Value, usuarioId);
        if (pessoa is null)
            throw new NotFoundException("Pessoa não encontrada");

        var categoria = await _categoriaRepository.GetByIdAndUsuarioId(request.CategoriaId!.Value, usuarioId);
        if (categoria is null)
            throw new NotFoundException("Categoria não encontrada");

        // VALIDATING VALUES
        if (request.Data is null || request.Data == default)
            throw new BadRequestException("Data é obrigatória");

        if (request.Valor is null || request.Valor <= 0)
            throw new BadRequestException("Valor deve ser maior que 0");

        var transacao = new Transacao(
            descricao: request.Descricao!,
            valor: request.Valor!.Value,
            tipo: request.Tipo!.Value,
            data: request.Data!.Value,
            pessoa,
            categoria,
            usuarioId
        );

        await _transacaoRepository.Create(transacao);
        await _transacaoRepository.Save();

        return new CreateTransacaoResponse
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            Data = transacao.Data
        };
    }
}