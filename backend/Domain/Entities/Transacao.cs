using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Enums;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Domain.Entities;

public class Transacao
{
    // Properties
    [Key]
    public Guid Id { get; private set; }
    public string Descricao { get; private set; } = default!;
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public DateOnly Data { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Guid PessoaId { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Guid UsuarioId { get; private set; }

    // Navigation properties
    public Usuario? Usuario { get; private set; }
    public Pessoa? Pessoa { get; private set; }
    public Categoria? Categoria { get; private set; }

    private Transacao() { }
    public Transacao(string descricao, decimal valor, TipoTransacao tipo, DateOnly data, Pessoa pessoa, Categoria categoria, Guid usuarioId)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new BadRequestException("Descricao é obrigatória");

        if (valor <= 0)
            throw new BadRequestException("Valor deve ser maior que zero");

        if (!Enum.IsDefined(typeof(TipoTransacao), tipo))
            throw new BadRequestException("Tipo inválido");

        if (data == default)
            throw new ArgumentException("Data é obrigatória");

        if (tipo == TipoTransacao.RECEITA)
            pessoa.ValidateCanHaveReceita();

        categoria.ValidateTipo(tipo);

        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        Data = data;
        PessoaId = pessoa.Id;
        CategoriaId = categoria.Id;
        UsuarioId = usuarioId;
    }
}