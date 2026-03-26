using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Pessoa
{
    // Properties
    [Key]
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Guid UsuarioId { get; private set; }

    // Navigation properties
    public Usuario? Usuario { get; private set; }
    public virtual ICollection<Transacao> Transacoes { get; private set; } = new List<Transacao>();
}