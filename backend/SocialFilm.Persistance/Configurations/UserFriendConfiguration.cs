using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SocialFilm.Domain.Entities;

using System.Reflection.Emit;

namespace SocialFilm.Persistance.Configurations;

public sealed class UserFriendConfiguration : IEntityTypeConfiguration<UserFriend>
{
    public void Configure(EntityTypeBuilder<UserFriend> builder)
    {
        builder.ToTable("UserFriends");

        builder
            .HasKey(uf => new { uf.UserId, uf.FriendId });

        builder
            .HasOne(uf => uf.User)
            .WithMany(u => u.UserFriends)
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        builder
            .HasOne(uf => uf.Friend)
            .WithMany()
            .HasForeignKey(uf => uf.FriendId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
    }
}
