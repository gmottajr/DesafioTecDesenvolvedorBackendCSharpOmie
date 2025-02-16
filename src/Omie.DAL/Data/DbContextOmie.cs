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
