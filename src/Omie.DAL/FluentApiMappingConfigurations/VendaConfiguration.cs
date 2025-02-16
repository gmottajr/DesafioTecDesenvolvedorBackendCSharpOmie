using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;

namespace Omie.DAL.FuentApiMappingConfigurations;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.ToTable("Vendas");

        // Primary Key
        builder.HasKey(v => v.Id);

        // Fields configurations
        builder.Property(v => v.DataDaVenda)
            .IsRequired();

        builder.Property(v => v.CodigoVenda)
            .IsRequired()
            .HasColumnType("CHAR(12)")
            .HasMaxLength(12);

        builder.Property(v => v.DeletedAt)
            .IsRequired(false);

        builder.Property(v => v.CompletedAt)
            .IsRequired(false);

        builder.Property(v => v.CancelledAt)
            .IsRequired(false);

        // Foreign Key relationship to Cliente
        builder.HasOne(v => v.Cliente)
            .WithMany()
            .HasForeignKey(v => v.ClienteId)
            .IsRequired();

        builder.HasMany(v => v.Items)
            .WithOne(i => i.Venda)
            .HasForeignKey(i => i.VendaId)
            .IsRequired();

        builder.HasCheckConstraint("CK_Vendas_CreatedAt", "CreatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Vendas_UpdatedAt", "UpdatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Vendas_DeletedAt", "DeletedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Vendas_CompletedAt", "CompletedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Vendas_CancelledAt", "CancelledAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Vendas_CodigoVenda", "Len(CodigoVenda) = 12 ");
        builder.HasIndex(v => v.CodigoVenda).IsUnique();
    }
}

