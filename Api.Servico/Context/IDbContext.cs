using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace API.Persistencia
{
    public interface IDbContext : IDisposable
    {
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbChangeTracker ChangeTracker { get; }
        int SaveChanges();
    }
}
