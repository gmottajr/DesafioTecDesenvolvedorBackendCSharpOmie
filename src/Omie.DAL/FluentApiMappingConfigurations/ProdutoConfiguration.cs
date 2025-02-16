using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;

namespace Omie.DAL.FuentApiMappingConfigurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Fields configurations
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR(80)")
            .HasMaxLength(80);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasColumnType("VARCHAR(500)")
            .HasMaxLength(500);

        builder.Property(p => p.Valor)
            .IsRequired();

        builder.Property(p => p.Imagem)
            .HasMaxLength(125)
            .HasColumnType("VARCHAR(120)")
            .IsRequired(true);

        builder.Property(p => p.Categoria)
            .HasMaxLength(50)
            .HasColumnType("VARCHAR(50)")
            .IsRequired(true);

        builder.Property(p => p.Marca)
            .HasMaxLength(30)
            .HasColumnType("VARCHAR(30)")
            .IsRequired(true);

        builder.Property(p => p.Unidade)
            .HasMaxLength(10)
            .HasColumnType("VARCHAR(10)")
            .IsRequired(true);

        builder.Property(p => p.Tipo)
            .HasMaxLength(30)
            .HasColumnType("VARCHAR(30)")
            .IsRequired(false);

        builder.Property(p => p.Codigo)
            .HasColumnType("CHAR(12)")
            .HasMaxLength(12)
            .IsRequired(false);

        builder.Property(p => p.EstoqueId)
            .IsRequired(false);

        builder.HasCheckConstraint("CK_Produtos_Valor", "Valor > 0");
        builder.HasCheckConstraint("CK_Produtos_Codigo", "LEN(Codigo) = 12");
        builder.HasCheckConstraint("CK_Produtos_Unidade", "LEN(Unidade) <= 10 AND LEN(Unidade) > 0");
        builder.HasCheckConstraint("CK_Produtos_Categoria", "LEN(Categoria) <= 50 AND LEN(Categoria) > 2");
        builder.HasCheckConstraint("CK_Produtos_Marca", "LEN(Marca) <= 30 AND LEN(Marca) > 2");
        builder.HasCheckConstraint("CK_Produtos_Tipo", "LEN(Tipo) <= 30 and LEN(Tipo) > 2");
        builder.HasCheckConstraint("CK_Produtos_EstoqueId", "EstoqueId > 0");
        builder.HasCheckConstraint("CK_Produtos_Descricao", "LEN(Descricao) <= 500 AND LEN(Descricao) > 10");
        builder.HasCheckConstraint("CK_Produtos_Name", "LEN(Name) <= 80 AND LEN(Name) > 2");
        builder.HasCheckConstraint("CK_Produtos_Imagem", "LEN(Imagem) <= 125 AND LEN(Imagem) > 5");
        builder.HasCheckConstraint("CK_Produtos_CreatedAt", "CreatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Produtos_UpdatedAt", "UpdatedAt <= GETDATE()");
        builder.HasIndex(p => p.Codigo).IsUnique();
    }
}

