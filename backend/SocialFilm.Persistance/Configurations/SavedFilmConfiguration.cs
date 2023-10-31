using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SocialFilm.Domain.Entities;

namespace SocialFilm.Persistance.Configurations;

public sealed class SavedFilmConfiguration : IEntityTypeConfiguration<SavedFilm>
{
    public void Configure(EntityTypeBuilder<SavedFilm> builder)
    {
        builder.ToTable("SavedFilms");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.UserId, x.FilmId });

        builder.HasOne(sf => sf.Film)
            .WithMany(f => f.SavedFilms)
            .HasForeignKey(sf => sf.FilmId);
    }
}
