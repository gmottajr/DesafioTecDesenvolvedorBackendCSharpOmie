
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omie.Domain;

namespace Omie.DAL.FuentApiMappingConfigurations;



public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes", t =>
        {
            // -- Disclaimer: Não quero perder tempo com check constraints de banco de dados, pois isso está fora do escopo da especificação do desafio de código.  
            // -- Meu objetivo aqui é apenas demonstrar que sei como criar CHECK CONSTRAINTS e entendo a importância de validar dados no nível do banco  
            // -- para garantir um nível mínimo de qualidade dos dados persistidos. Quanto maior for a qualidade dos dados em um sistema,  
            // -- menor será a suscetibilidade a certos tipos de bugs – especialmente aqueles que exigem sessões de depuração demoradas  
            // -- devido a problemas com dados em produção.
            t.HasCheckConstraint("CK_Clientes_CPF", "LEN(CPF) = 11");
            t.HasCheckConstraint("CK_Clientes_Status", "Status IN (1, 2, 3, 4)");
            t.HasCheckConstraint("CK_Clientes_Telefone", "LEN(Telefone) BETWEEN 10 AND 20");
            t.HasCheckConstraint("CK_Clientes_Email", "LEN(Email) > 4 AND Email LIKE '%@%'");
            t.HasCheckConstraint("CK_Clientes_CreatedAt", "CreatedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Clientes_UpdatedAt", "UpdatedAt <= GETDATE()");
            t.HasCheckConstraint("CK_Clientes_EmailAtPos", "CHARINDEX('@', Email) > 0");
        });

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
            .HasColumnType("VARCHAR(100)")
            .IsRequired(true);

        builder.Property(c => c.Telefone)
            .HasMaxLength(20)
            .HasColumnType("VARCHAR(20)")
            .IsRequired();

        builder.Property(c => c.Observacao)
            .HasMaxLength(500)
            .HasColumnType("VARCHAR(500)")
            .IsRequired(false);

        // Relationships
        builder.HasOne(c => c.Endereco)
            .WithMany() // No navigation property in Endereco for Cliente (assuming)
            .HasForeignKey(c => c.EnderecoId)
            .IsRequired(false);

        builder.HasIndex(c => c.CPF).IsUnique(); // Define uma Unique Key
        builder.HasIndex(c => c.Email).IsUnique(); // Define uma Unique Key

    }
}
