using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;
using Omie.Domain.Entities;

namespace Omie.DAL.FuentApiMappingConfigurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items", t =>
        {
            t.HasCheckConstraint("CK_Item_CreatedAt", "CreatedAt <= DATEADD(MINUTE, 30, GETDATE())");
            t.HasCheckConstraint("CK_Item_UpdatedAt", "UpdatedAt <= DATEADD(MINUTE, 30, GETDATE())");
            t.HasCheckConstraint("CK_Vendas_Quatidade", "Quantidade > 0");
            t.HasCheckConstraint("CK_Vendas_ValorUnitario", "ValorUnitario >= 0");
            t.HasCheckConstraint("CK_Vendas_Produrto", "LEN(Produto) >= 2 and LEN(Produto) <= 100");
        });

        // Primary Key
        builder.HasKey(i => new {i.Id});

        // Foreign Keys
        builder.HasOne(i => i.Venda)
            .WithMany(v => v.Itens)
            .HasForeignKey(i => i.VendaId)
            .IsRequired();

        builder.Property(p => p.Produto)
                    .HasMaxLength(100)
                    .IsRequired();

        builder.Property(i => i.Quantidade)
            .IsRequired();

        builder.Property(i => i.ValorUnitario)
            .IsRequired();
        
        builder.Ignore(i => i.ValorTotal);
        builder.HasIndex(i => new {i.VendaId, i.Produto}).IsUnique();
    }
}

