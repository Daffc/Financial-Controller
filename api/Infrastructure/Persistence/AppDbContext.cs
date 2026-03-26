using Microsoft.EntityFrameworkCore;
using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            // Primary Key
            entity.HasKey(u => u.Id);

            // Properties
            entity.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.SenhaHash)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.DataCriacao)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Indexes
            entity.HasIndex(u => u.Email)
                .IsUnique();
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            // Primary Key
            entity.HasKey(p => p.Id);

            // Properties
            entity.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Idade)
                .IsRequired();

            entity.Property(p => p.DataCriacao)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Constraints
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Pessoa_Idade_Positiva",
                "\"Idade\" > 0"
            ));

            // Relationships
            entity.HasOne(p => p.Usuario)
                .WithMany(u => u.Pessoas)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            entity.HasIndex(u => u.UsuarioId);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            // Primary Key
            entity.HasKey(c => c.Id);

            // Properties
            entity.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(400);

            entity.Property(c => c.Finalidade)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(c => c.DataCriacao)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relationships
            entity.HasOne(c => c.Usuario)
                .WithMany(u => u.Categorias)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Constraints
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Categoria_Finalidade_Valida",
                "\"Finalidade\" IN (1, 2, 3)"
            ));

            // Indexes
            entity.HasIndex(u => u.UsuarioId);
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
            // Primary Key
            entity.HasKey(t => t.Id);

            // Properties
            entity.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(400);

            entity.Property(t => t.Valor)
                .IsRequired();

            entity.Property(t => t.Tipo)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(t => t.DataCriacao)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relationships
            entity.HasOne(t => t.Usuario)
                .WithMany(u => u.Transacoes)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(t => t.Pessoa)
                .WithMany(p => p.Transacoes)
                .HasForeignKey(c => c.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(t => t.Categoria)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(c => c.CategoriaId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            // Constraints
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Transacao_Tipo_Valido",
                "\"Tipo\" IN (1, 2)"
            ));
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Transacao_Valor_Positiva",
                "\"Valor\" > 0"
            ));

            // Indexes
            entity.HasIndex(u => u.UsuarioId);
            entity.HasIndex(u => u.PessoaId);
            entity.HasIndex(u => u.CategoriaId);
        });
    }
}