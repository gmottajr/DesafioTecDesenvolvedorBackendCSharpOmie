﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;
using Omie.Domain.Entities;

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
        
        builder.Property(v => v.Cliente)
            .IsRequired()
            .HasColumnType("VARCHAR(40)")
            .HasMaxLength(40);

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

        builder.HasMany(v => v.Itens)
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
            t.HasCheckConstraint("CK_Vendas_CreatedAt", "CreatedAt <= DATEADD(MINUTE, 30, GETDATE())");
            t.HasCheckConstraint("CK_Vendas_UpdatedAt", "UpdatedAt <= DATEADD(MINUTE, 30, GETDATE())");
            t.HasCheckConstraint("CK_Vendas_DeletedAt", "DeletedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_CompletedAt", "CompletedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_CancelledAt", "CancelledAt <= GETDATE()");
            t.HasCheckConstraint("CK_Vendas_CodigoVenda", "Len(CodigoVenda) > 12 ");
            t.HasCheckConstraint("CK_Vendas_Cliente", "Len(Cliente) > 2 and Len(Cliente) <= 40");
        });
        builder.HasIndex(v => v.CodigoVenda).IsUnique();
    }
}

