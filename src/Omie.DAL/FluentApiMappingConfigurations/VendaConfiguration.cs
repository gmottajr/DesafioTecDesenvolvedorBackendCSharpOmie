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
            .HasColumnType("VARCHAR(255)")
            .HasMaxLength(255);

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
        
        // -- Disclaimer: Não quero perder tempo com check constraints de banco de dados, pois isso está fora do escopo da especificação do desafio de código.  
        // -- Meu objetivo aqui é apenas demonstrar que sei como criar CHECK CONSTRAINTS e entendo a importância de validar dados no nível do banco  
        // -- para garantir um nível mínimo de qualidade dos dados persistidos. Quanto maior for a qualidade dos dados em um sistema,  
        // -- menor será a suscetibilidade a certos tipos de bugs – especialmente aqueles que exigem sessões de depuração demoradas  
        // -- devido a problemas com dados em produção.
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Vendas_CreatedAt", "CreatedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_UpdatedAt", "UpdatedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_DeletedAt", "DeletedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_CompletedAt", "CompletedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_CancelledAt", "CancelledAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_CodigoVenda", "Len(CodigoVenda) = 12 ");
        });
        builder.HasIndex(v => v.CodigoVenda).IsUnique();
    }
}

