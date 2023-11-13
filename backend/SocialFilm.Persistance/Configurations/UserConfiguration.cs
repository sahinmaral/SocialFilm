using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SocialFilm.Domain.Entities;

namespace SocialFilm.Persistance.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);


        builder
            .HasMany(u => u.SavedFilms)
            .WithOne()
            .HasForeignKey(sf => sf.UserId)
            .IsRequired();

        builder
            .HasMany(u => u.Posts)
            .WithOne()
            .HasForeignKey(p => p.UserId)
            .IsRequired();
    }
}


