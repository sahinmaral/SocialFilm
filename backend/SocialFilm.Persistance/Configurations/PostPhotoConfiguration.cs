using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SocialFilm.Domain.Entities;

namespace SocialFilm.Persistance.Configurations;

public sealed class PostPhotoConfiguration : IEntityTypeConfiguration<PostPhoto>
{
    public void Configure(EntityTypeBuilder<PostPhoto> builder)
    {
        builder.ToTable("PostPhotos");
        builder.HasKey(x => x.Id);
    }
}