using Microsoft.EntityFrameworkCore;
using MusicEvent.Domain.Interfaces.Infra.Data;
using MusicEvent.Infra.Data.Context;
using System;

namespace MusicEvent.Infra.Data.Configuration
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MusicEventContext _dbContext;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(MusicEventContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
