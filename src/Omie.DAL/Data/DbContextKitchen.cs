using Omie.Domain;
using Omie.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Omie.DAL;

public class DbContextOmie: DbContext
{
    public DbContextOmie(DbContextOptions<DbContextOmie> options) : base(options)
    {
    }

    public DbSet<Venda> Vendas { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Produto>()
            .HasKey(m => new { m.Id });
        modelBuilder.Entity<Produto>()
            .Property(m => m.Name)
            .HasMaxLength(125)
            .IsRequired(true);

        modelBuilder.Entity<Produto>()
            .Property(m => m.Descricao)
            .HasMaxLength(255)
            .IsRequired(true);

        modelBuilder.Entity<Produto>()
            .Property(m => m.Valor)
            .IsRequired(true);
        
        modelBuilder.Entity<Produto>()
            .Property(m => m.CreatedAt)
            .IsRequired(true);

        modelBuilder.Entity<Produto>()
            .Property(m => m.UpdatedAt)
            .IsRequired(false);

        modelBuilder.Entity<Produto>()
            .Property(p => p.Categoria)
            .HasMaxLength(50)
            .IsRequired(true);

        modelBuilder.Entity<Produto>()
            .Property(p => p.Imagem)
            .HasMaxLength(255)
            .IsRequired(true);

        modelBuilder.Entity<Venda>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Venda>()
            .HasMany(m => m.Items)
            .WithOne(m => m.Venda)
            .HasForeignKey(m => m.VendaId);

        modelBuilder.Entity<Venda>()
            .Property(m => m.CodigoVenda)
            .HasMaxLength(6)
            .IsRequired(true);

        modelBuilder.Entity<Venda>()
            .Property(m => m.CreatedAt)
            .IsRequired(true);

        modelBuilder.Entity<Venda>()
            .Property(m => m.DataDaVenda)
            .IsRequired(true);

        modelBuilder.Entity<Venda>()
            .Property(m => m.CompletedAt)
            .IsRequired(false);

        modelBuilder.Entity<Venda>()
            .Property(m => m.CancelledAt)
            .IsRequired(false);

        modelBuilder.Entity<Venda>()
            .Property(m => m.DeletedAt)
            .IsRequired(false);

        modelBuilder.Entity<Venda>()
            .HasOne(m => m.Cliente)
            .WithMany(m => m.Pedido)
            .HasForeignKey(m => m.ClienteId);
    
        modelBuilder.Entity<Item>()
            .HasKey(m => new { m.VendaId, m.ProdutoId });

        modelBuilder.Entity<Item>()
            .HasOne(m => m.Produto)
            .WithMany()
            .HasForeignKey(m => m.ProdutoId);

        modelBuilder.Entity<Item>()
            .HasOne(m => m.Venda)
            .WithMany(m => m.Items)
            .HasForeignKey(m => m.VendaId);

        modelBuilder.Entity<Item>()
            .Property(m => m.Quantidade)
            .IsRequired(true);

        modelBuilder.Entity<Item>()
            .Property(m => m.Valor)
            .IsRequired(true);

        modelBuilder.Entity<Produto>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Cliente>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Cliente>()
            .Property(m => m.Nome)
            .HasMaxLength(80)
            .IsRequired(true);

        modelBuilder.Entity<Cliente>()
            .Property(m => m.Status)
            .IsRequired(true);

        modelBuilder.Entity<Cliente>()
            .Property(m => m.CreatedAt);

        modelBuilder.Entity<Cliente>()
            .Property(m => m.CPF)
            .HasMaxLength(11)
            .IsRequired(true);

        modelBuilder.Entity<Cliente>()
            .HasMany(m => m.Pedido)
            .WithOne(p => p.Cliente)
            .HasForeignKey(m => m.ClienteId);

        modelBuilder.Entity<Cliente>()
            .Property(m => m.Email)
            .HasMaxLength(80)
            .IsRequired(true);

        modelBuilder.Entity<Cliente>()
            .Property(m => m.Telefone)
            .HasMaxLength(11)
            .IsRequired(true);

        modelBuilder.Entity<Cliente>()
            .HasOne(m => m.Endereco)
            .WithOne()
            .HasForeignKey<Endereco>(m => m.ClienteId);

        modelBuilder.Entity<Cliente>()
            .Property(m => m.Observacao)
            .HasMaxLength(255)
            .IsRequired(false);

        modelBuilder.Entity<Endereco>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Endereco>()
            .Property(m => m.CEP)
            .HasMaxLength(8)
            .IsRequired(true);

        modelBuilder.Entity<Endereco>()
            .Property(m => m.Logradouro)
            .HasMaxLength(80)
            .IsRequired(true);

        modelBuilder.Entity<Endereco>()
            .Property(m => m.Numero)
            .HasMaxLength(10)
            .IsRequired(true);

        modelBuilder.Entity<Endereco>()
            .Property(m => m.Bairro)
            .HasMaxLength(80)
            .IsRequired(true);

        modelBuilder.Entity<Endereco>()
            .Property(m => m.Cidade)
            .HasMaxLength(80)
            .IsRequired(true);

        modelBuilder.Entity<Endereco>()
            .Property(m => m.Estado)
            .HasMaxLength(2)
            .IsRequired(true);

        modelBuilder.Entity<Endereco>()
            .Property(m => m.Complemento)
            .HasMaxLength(80)
            .IsRequired(false);

        modelBuilder.Entity<Endereco>()
            .Property(m => m.ClienteId)
            .IsRequired(true);

    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var entries = ChangeTracker.Entries<EntityBaseRoot<long>>();
        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    break;
            }
        }
    }
}
