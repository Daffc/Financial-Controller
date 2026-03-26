using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Enums;

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
}