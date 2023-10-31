using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using SocialFilm.Domain.Common;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Persistance.Context;
public sealed class AppDbContext : IdentityDbContext<User, Role, string>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
    }

    public override int SaveChanges()
    {
        var entities = ChangeTracker.Entries<BaseEntity>();
        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Property(x => x.UpdatedAt).IsModified = false;
                entity.Property(x => x.CreatedAt).CurrentValue = DateTime.Now;
            }
            if (entity.State == EntityState.Modified)
            {
                entity.Property(x => x.CreatedAt).IsModified = false;
                entity.Property(x => x.UpdatedAt).CurrentValue = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker.Entries<BaseEntity>();
        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Property(x => x.UpdatedAt).IsModified = false;
                entity.Property(x => x.CreatedAt).CurrentValue = DateTime.Now;
            }
            if (entity.State == EntityState.Modified)
            {
                entity.Property(x => x.CreatedAt).IsModified = false;
                entity.Property(x => x.UpdatedAt).CurrentValue = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}