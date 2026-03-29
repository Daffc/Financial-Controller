using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Enums;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Domain.Entities;

public class Transacao
{
    // Properties
    [Key]
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Guid PessoaId { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Guid UsuarioId { get; private set; }

    // Navigation properties
    public Usuario? Usuario { get; private set; }
    public Pessoa? Pessoa { get; private set; }
    public Categoria? Categoria { get; private set; }

    public Transacao(string descricao, decimal valor, TipoTransacao tipo, Guid pessoaId, Guid categoriaId, Guid usuarioId)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new BadRequestException("Descricao é obrigatória");

        if (valor <= 0)
            throw new BadRequestException("Valor deve ser maior que zero");

        if (!Enum.IsDefined(typeof(TipoTransacao), tipo))
            throw new BadRequestException("Tipo inválido");

        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        PessoaId = pessoaId;
        CategoriaId = categoriaId;
        UsuarioId = usuarioId;
    }
}