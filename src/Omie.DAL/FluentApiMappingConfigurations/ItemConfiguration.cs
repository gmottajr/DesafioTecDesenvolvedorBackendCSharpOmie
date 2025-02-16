using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;

namespace Omie.DAL.FuentApiMappingConfigurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");

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

        builder.HasCheckConstraint("CK_Item_CreatedAt", "CreatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Item_UpdatedAt", "UpdatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Vendas_Quatidade", "Quantidade > 0");
        builder.HasCheckConstraint("CK_Vendas_ValorTotal", "ValorTotal >= 0");
    }
}

