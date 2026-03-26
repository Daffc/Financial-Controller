using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

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
}