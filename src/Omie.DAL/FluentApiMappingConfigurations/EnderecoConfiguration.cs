using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;

namespace Omie.DAL.FuentApiMappingConfigurations;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");

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
        

        builder.HasCheckConstraint("CK_Endereco_CreatedAt", "CreatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Endereco_UpdatedAt", "UpdatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Endereco_Numero", "LEN(Numero) <= 10 and LEN(Numero) > 0");
        builder.HasCheckConstraint("CK_Endereco_CEP", "LEN(CEP) = 8");
        builder.HasCheckConstraint("CK_Endereco_Estado", "LEN(Estado) = 2");
        builder.HasCheckConstraint("CK_Endereco_Cidade", "LEN(Cidade) <= 40 and LEN(Cidade) > 1");

    }
}

