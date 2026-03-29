using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Enums;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Domain.Entities;

public class Categoria
{
    // Properties
    [Key]
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public FinalidadeCategoria Finalidade { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Guid UsuarioId { get; private set; }

    // Navigation properties
    public Usuario? Usuario { get; private set; }
    public virtual ICollection<Transacao> Transacoes { get; private set; } = new List<Transacao>();

    public Categoria(string descricao, FinalidadeCategoria finalidade, Guid usuarioId)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new BadRequestException("Descricao é obrigatória");

        if (!Enum.IsDefined(typeof(FinalidadeCategoria), finalidade))
            throw new BadRequestException("Finalidade inválida");

        Descricao = descricao;
        Finalidade = finalidade;
        UsuarioId = usuarioId;
    }
}