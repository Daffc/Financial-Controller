using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Usuario
{
    // Properties
    [Key]
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public DateTime DataCriacao { get; private set; }

    // Navigation properties
    public virtual ICollection<Pessoa> Pessoas { get; private set; } = new List<Pessoa>();
    public virtual ICollection<Categoria> Categorias { get; private set; } = new List<Categoria>();
}