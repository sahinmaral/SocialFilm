﻿namespace SocialFilm.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}
