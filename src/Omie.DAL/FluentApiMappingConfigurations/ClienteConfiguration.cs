
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;

namespace Omie.DAL.FuentApiMappingConfigurations;



public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        // Primary Key
        builder.HasKey(c => c.Id);

        // Fields configurations
        builder.Property(c => c.Nome)
            .HasColumnType("VARCHAR(100)")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.CPF)
            .IsRequired()
            .HasColumnType("CHAR(11)")
            .HasMaxLength(11);  // Assuming CPF is a string with 11 characters

        builder.Property(c => c.Status)
            .HasColumnType("TINYINT")
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(c => c.Telefone)
            .HasMaxLength(20)
            .HasColumnType("VARCHAR(20)")
            .IsRequired(false);

        builder.Property(c => c.Observacao)
            .HasMaxLength(500)
            .HasColumnType("VARCHAR(500)")
            .IsRequired(false);

        // Relationships
        builder.HasOne(c => c.Endereco)
            .WithMany() // No navigation property in Endereco for Cliente (assuming)
            .HasForeignKey(c => c.EnderecoId)
            .IsRequired(false);

        builder.HasMany(c => c.Pedido)
            .WithOne(v => v.Cliente)  // Assuming Venda has a reference to Cliente
            .HasForeignKey(v => v.ClienteId)
            .IsRequired();

        builder.HasCheckConstraint("CK_Clientes_CPF", "LEN(CPF) = 11");
        builder.HasCheckConstraint("CK_Clientes_Status", "Status IN (1, 2, 3, 4)");
        builder.HasCheckConstraint("CK_Clientes_Telefone", "LEN(Telefone) BETWEEN 10 AND 20");
        builder.HasCheckConstraint("CK_Clientes_Email", "LEN(Email) > 4 AND Email LIKE '%@%'");
        builder.HasCheckConstraint("CK_Clientes_CreatedAt", "CreatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Clientes_UpdatedAt", "UpdatedAt <= GETDATE()");
        builder.HasCheckConstraint("CK_Clientes_EmailAtPos", "CHARINDEX('@', Email) > 0");
        builder.HasIndex(c => c.CPF).IsUnique(); // Define uma Unique Key
        builder.HasIndex(c => c.Email).IsUnique(); // Define uma Unique Key

    }
}
