namespace GenericRepository.Contracts;

public interface IRepository
{
    Task<TEntity?> GetAsync<TEntity>(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder,
        CancellationToken cancellationToken = default)
        where TEntity : class;

    Task<List<TEntity>> GetListAsync<TEntity>(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder,
        CancellationToken cancellationToken = default)
        where TEntity : class;

    Task<TResult?> GetReadOnlyAsync<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
        CancellationToken cancellationToken = default)
        where TEntity : class;

    IAsyncEnumerable<TResult> GetReadOnlyListAsync<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder)
        where TEntity : class;
    
    Task<bool> AnyAsync<TEntity>(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder,
        CancellationToken cancellationToken = default)
        where TEntity : class;

    Task AddAsync<TEntity>(TEntity item, CancellationToken cancellationToken = default) where TEntity : class;

    void Remove<TEntity>(TEntity item) where TEntity : class;

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}