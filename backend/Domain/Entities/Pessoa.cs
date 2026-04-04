using System.ComponentModel.DataAnnotations;
using FinancialControllerServer.Domain.Exceptions;
using FinancialControllerServer.Domain.Enums;

namespace FinancialControllerServer.Domain.Entities;

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

    public Pessoa(string nome, int idade, Guid usuarioId)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BadRequestException("Nome é obrigatório");

        if (idade < 0 || idade > 150)
            throw new BadRequestException("Idade inválida");

        Nome = nome;
        Idade = idade;
        UsuarioId = usuarioId;
    }

    public void Update(string nome, int idade, bool haveReceita)
    {
        if(string.IsNullOrWhiteSpace(nome))
            throw new BadRequestException("Nome é obrigatório");

        if (idade < 0 || idade > 150)
            throw new BadRequestException("Idade inválida");

        if (idade < 18 && haveReceita)
            throw new BadRequestException("Pessoa menor de idade não pode possuir receitas");

        Nome = nome;
        Idade = idade;
    }

    public void ValidateCanHaveReceita()
    {
        if(Idade < 18)
            throw new BadRequestException("Pessoa menor de idade não pode possuir receitas");
    }
}