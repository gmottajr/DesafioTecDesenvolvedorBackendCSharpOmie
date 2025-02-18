using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;
using Omie.Domain.Entities;

namespace Omie.DAL.FuentApiMappingConfigurations;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos", t =>
        {
             // -- Disclaimer: Não quero perder tempo com check constraints de banco de dados, pois isso está fora do escopo da especificação do desafio de código.  
            // -- Meu objetivo aqui é apenas demonstrar que sei como criar CHECK CONSTRAINTS e entendo a importância de validar dados no nível do banco  
            // -- para garantir um nível mínimo de qualidade dos dados persistidos. Quanto maior for a qualidade dos dados em um sistema,  
            // -- menor será a suscetibilidade a certos tipos de bugs – especialmente aqueles que exigem sessões de depuração demoradas  
            // -- devido a problemas com dados em produção.
            t.HasCheckConstraint("CK_Endereco_CreatedAt", "CreatedAt <= DATEADD(MINUTE, 30, GETDATE())");
            t.HasCheckConstraint("CK_Endereco_UpdatedAt", "UpdatedAt <= DATEADD(MINUTE, 30, GETDATE())");
            t.HasCheckConstraint("CK_Endereco_Numero", "LEN(Numero) <= 10 and LEN(Numero) > 0");
            t.HasCheckConstraint("CK_Endereco_CEP", "LEN(CEP) = 8");
            t.HasCheckConstraint("CK_Endereco_Estado", "LEN(Estado) = 2");
            t.HasCheckConstraint("CK_Endereco_Cidade", "LEN(Cidade) <= 40 and LEN(Cidade) > 1");
        });

        // Primary Key
        builder.HasKey(e => e.Id);

        // Fields configurations
        builder.Property(e => e.Logradouro)
            .IsRequired()
            .HasColumnType("VARCHAR(50)")
            .HasMaxLength(50);

        builder.Property(e => e.Numero)
            .IsRequired()
            .HasColumnType("VARCHAR(10)")
            .HasMaxLength(10);

        builder.Property(e => e.Complemento)
            .HasMaxLength(40)
            .HasColumnType("VARCHAR(40)")
            .IsRequired(false);

        builder.Property(e => e.Bairro)
            .IsRequired()
            .HasColumnType("VARCHAR(40)")
            .HasMaxLength(40);

        builder.Property(e => e.Cidade)
            .IsRequired()
            .HasColumnType("VARCHAR(40)")
            .HasMaxLength(40);

        builder.Property(e => e.Estado)
            .IsRequired()
            .HasColumnType("CHAR(2)")
            .HasMaxLength(2);

        builder.Property(e => e.CEP)
            .IsRequired()
            .HasColumnType("CHAR(8)")
            .HasMaxLength(8);

        builder.Property(e => e.ClienteId)
            .IsRequired(true);
    }
}

