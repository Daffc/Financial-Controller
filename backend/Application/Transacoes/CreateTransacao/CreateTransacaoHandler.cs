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
        
        // DOMAIN ROLES
        if (request.Valor is null || request.Valor <= 0)
            throw new BadRequestException("Valor deve ser maior que 0");

        if (pessoa.Idade < 18 && request.Tipo == TipoTransacao.RECEITA)
            throw new BadRequestException("Pessoa menor de idade não pode ter receita");

        if (!ValidCategoriaTipo(categoria.Finalidade, request.Tipo!.Value))
            throw new BadRequestException("Categoria não permite este Tipo");


        var transacao = new Transacao(
            request.Descricao!,
            request.Valor.Value,
            request.Tipo!.Value,
            request.Data.Value,
            pessoa.Id,
            categoria.Id,
            usuario.Id
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

    private static bool ValidCategoriaTipo(FinalidadeCategoria finalidade, TipoTransacao tipo)
    {
        return finalidade switch
        {
            FinalidadeCategoria.AMBAS => true,
            FinalidadeCategoria.RECEITA => tipo == TipoTransacao.RECEITA,
            FinalidadeCategoria.DESPESA => tipo == TipoTransacao.DESPESA,
            _ => false
        };
    }
}