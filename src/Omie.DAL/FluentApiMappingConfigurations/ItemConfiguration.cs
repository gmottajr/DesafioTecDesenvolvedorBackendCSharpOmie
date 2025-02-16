using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;

namespace Omie.DAL.FuentApiMappingConfigurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items", t =>
        {
            t.HasCheckConstraint("CK_Item_CreatedAt", "CreatedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Item_UpdatedAt", "UpdatedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_Quatidade", "Quantidade > 0");
            t.HasCheckConstraint("CK_Vendas_ValorTotal", "ValorTotal >= 0");
        });

        // Primary Key
        builder.HasKey(i => new {i.ProdutoId, i.VendaId});

        // Foreign Keys
        builder.HasOne(i => i.Venda)
            .WithMany(v => v.Items)
            .HasForeignKey(i => i.VendaId)
            .IsRequired();

        builder.HasOne(i => i.Produto)
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .IsRequired();

        // Fields configurations
        builder.Property(i => i.Quantidade)
            .IsRequired();

        builder.Property(i => i.ValorTotal)
            .IsRequired();
    }
}

