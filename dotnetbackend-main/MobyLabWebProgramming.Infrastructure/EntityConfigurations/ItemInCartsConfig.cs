using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ItemsInCartsConfig : IEntityTypeConfiguration<Item_In_Carts>
{
    public void Configure(EntityTypeBuilder<Item_In_Carts> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Quantity)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(e => e.Cart) 
            .WithMany(e => e.Products)  
            .HasForeignKey(e => e.CartId) 
            .HasPrincipalKey(e => e.Id) 
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Item)
            .WithMany(e => e.References)
            .HasForeignKey(e => e.ItemId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
