using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SocialFilm.Domain.Entities;

namespace SocialFilm.Persistance.Configurations;

public sealed class FilmDetailConfiguration : IEntityTypeConfiguration<FilmDetail>
{
    public void Configure(EntityTypeBuilder<FilmDetail> builder)
    {
        builder.ToTable("FilmDetails");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name);

        builder
            .HasMany(e => e.SavedFilms)
            .WithOne()
            .HasForeignKey(e => e.FilmId)
            .IsRequired();
    }
}
