using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;

namespace Omie.DAL.FuentApiMappingConfigurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable(t =>
        {
            // -- Disclaimer: Não quero perder tempo com check constraints de banco de dados, pois isso está fora do escopo da especificação do desafio de código.  
            // -- Meu objetivo aqui é apenas demonstrar que sei como criar CHECK CONSTRAINTS e entendo a importância de validar dados no nível do banco  
            // -- para garantir um nível mínimo de qualidade dos dados persistidos. Quanto maior for a qualidade dos dados em um sistema,  
            // -- menor será a suscetibilidade a certos tipos de bugs – especialmente aqueles que exigem sessões de depuração demoradas  
            // -- devido a problemas com dados em produção.
            t.HasCheckConstraint("CK_Produtos_Valor", "Valor > 0");
            t.HasCheckConstraint("CK_Produtos_Codigo", "LEN(Codigo) = 12");
            t.HasCheckConstraint("CK_Produtos_Unidade", "LEN(Unidade) <= 10 AND LEN(Unidade) > 0");
            t.HasCheckConstraint("CK_Produtos_Categoria", "LEN(Categoria) <= 50 AND LEN(Categoria) > 2");
            t.HasCheckConstraint("CK_Produtos_Marca", "LEN(Marca) <= 30 AND LEN(Marca) > 2");
            t.HasCheckConstraint("CK_Produtos_EstoqueId", "EstoqueId > 0 or EstoqueId IS NULL");
            t.HasCheckConstraint("CK_Produtos_Descricao", "LEN(Descricao) <= 500 AND LEN(Descricao) > 10");
            t.HasCheckConstraint("CK_Produtos_Name", "LEN(Nome) <= 80 AND LEN(Nome) > 2");
            t.HasCheckConstraint("CK_Produtos_Imagem", "LEN(Imagem) <= 125 AND LEN(Imagem) > 5");
            t.HasCheckConstraint("CK_Produtos_CreatedAt", "CreatedAt <= DATEADD(MINUTE, 30, GETDATE())");
            t.HasCheckConstraint("CK_Produtos_UpdatedAt", "UpdatedAt <= DATEADD(MINUTE, 30, GETDATE())");
        });

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
            .HasColumnType("VARCHAR(125)")
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
            .IsRequired(true);

        builder.Property(p => p.EstoqueId)
            .IsRequired(false);

        builder.HasIndex(p => p.Codigo).IsUnique();
    }
}

