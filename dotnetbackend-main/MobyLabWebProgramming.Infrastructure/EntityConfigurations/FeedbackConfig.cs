using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class FeedbackConfig : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Score)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
        builder.Property(e => e.Quality)
           .IsRequired();
        builder.Property(e => e.Content)
           .IsRequired()
           .HasMaxLength(255);
        builder.Property(e => e.Anonimous)
           .IsRequired();

        builder.HasOne(e => e.User) 
            .WithMany(e => e.Feedbacks)  
            .HasForeignKey(e => e.UserId) 
            .HasPrincipalKey(e => e.Id) 
            .OnDelete(DeleteBehavior.SetNull);

    }
}
