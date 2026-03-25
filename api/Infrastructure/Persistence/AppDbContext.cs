using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

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
                .HasForeignKey(cr => cr.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            entity.HasIndex(u => u.UsuarioId)
                .IsUnique();
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            // Primary Key
            entity.HasKey(p => p.Id);

            // Properties
            entity.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(400);

            entity.Property(p => p.Finalidade)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(p => p.DataCriacao)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relationships
            entity.HasOne(c => c.Usuario)
                .WithMany(u => u.Categorias)
                .HasForeignKey(cr => cr.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Constraints
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Categoria_Finalidade_Valida",
                "\"Finalidade\" IN (1, 2, 3)"
            ));

            // Indexes
            entity.HasIndex(u => u.UsuarioId)
                .IsUnique();
        });
    }
}