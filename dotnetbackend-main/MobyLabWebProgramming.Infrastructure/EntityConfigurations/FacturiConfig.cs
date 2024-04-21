using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class FacturiConfig : IEntityTypeConfiguration<Facturi>
{
    public void Configure(EntityTypeBuilder<Facturi> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Pret)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
        builder.Property(e => e.ShippingTime)
           .IsRequired();
        builder.Property(e => e.State)
           .IsRequired();
        builder.HasOne(e => e.Cart) 
            .WithOne(e => e.Facturi)  
            .HasForeignKey<Facturi>(e => e.CartId) 
            .HasPrincipalKey<ShopCart>(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // This specifies the delete behavior when the referenced entity is removed.

        builder.HasOne(e => e.User) 
            .WithMany(e => e.Facturi)  
            .HasForeignKey(e => e.UserId) 
            .HasPrincipalKey(e => e.Id) 
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Address)
            .WithMany(e => e.Facturi)
            .HasForeignKey(e => e.IdAdd)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
