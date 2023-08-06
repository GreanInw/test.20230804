using BGCTest.Api.DbContexts.Bases;
using Microsoft.EntityFrameworkCore;

namespace BGCTest.Api.Repositories.Bases
{
    public abstract class Repository<TInterfaceDbContext, TEntity> : IRepository<TEntity>
        where TEntity : class
        where TInterfaceDbContext : IDbContext
    {
        public Repository(TInterfaceDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        protected TInterfaceDbContext DbContext { get; }
        protected internal DbSet<TEntity> DbSet { get; }

        public IQueryable<TEntity> All => DbContext.Set<TEntity>();

        public void Add(TEntity entity) => _ = DbSet.Add(entity);

        public async Task AddAsync(TEntity entity) => _ = await DbSet.AddAsync(entity);

        public void Delete(TEntity entity) => DbSet.Remove(entity);

        public void Edit(TEntity entity) => DbSet.Entry(entity).State = EntityState.Modified;

        public ValueTask<TEntity> GetByAsync(params object[] keyValues) => DbSet.FindAsync(keyValues);
    }
}