using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SocialFilm.Domain.Entities;

namespace SocialFilm.Persistance.Configurations;

public sealed class FilmDetailGenreConfiguration : IEntityTypeConfiguration<FilmDetailGenre>
{
    public void Configure(EntityTypeBuilder<FilmDetailGenre> builder)
    {
        builder.ToTable("FilmDetailGenres");
        builder.HasKey(x => new { x.FilmDetailId, x.GenreId });

        builder
            .HasOne(fg => fg.FilmDetail)
            .WithMany(f => f.FilmDetailGenres)
            .HasForeignKey(fg => fg.FilmDetailId);

        builder
            .HasOne(fg => fg.Genre)
            .WithMany(f => f.FilmDetailGenres)
            .HasForeignKey(fg => fg.GenreId);
    }
}