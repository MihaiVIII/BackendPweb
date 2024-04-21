using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class AdreseFacturareConfiguration : IEntityTypeConfiguration<AdreseFacturare>
{
    public void Configure(EntityTypeBuilder<AdreseFacturare> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.City)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Street)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.SNumber)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(4095)
            .IsRequired(false);
        builder.Property(e => e.Scara)
            .IsRequired(false);
        builder.Property(e => e.Bloc)
            .IsRequired(false);
        builder.Property(e => e.Apartament)
            .IsRequired(false);
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(e => e.User) // This specifies a one-to-many relation.
            .WithMany(e => e.Addreses) // This provides the reverse mapping for the one-to-many relation. 
            .HasForeignKey(e => e.UserId) // Here the foreign key column is specified.
            .HasPrincipalKey(e => e.Id) // This specifies the referenced key in the referenced table.
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // This specifies the delete behavior when the referenced entity is removed.
    }
}
