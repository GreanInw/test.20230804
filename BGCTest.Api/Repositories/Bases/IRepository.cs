namespace BGCTest.Api.Repositories.Bases
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All { get; }
        ValueTask<TEntity> GetByAsync(params object[] keyValues);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
    }
}
