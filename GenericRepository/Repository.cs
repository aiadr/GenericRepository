using GenericRepository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository;

public class Repository<TDbContext> : IRepository where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public Repository(TDbContext context)
    {
        _context = context;
    }

    public Task<TEntity?> GetAsync<TEntity>(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return queryBuilder(_context.Set<TEntity>()).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetListAsync<TEntity>(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return queryBuilder(_context.Set<TEntity>()).ToListAsync(cancellationToken);
    }

    public Task<TResult?> GetReadOnlyAsync<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return queryBuilder(_context.Set<TEntity>().AsNoTracking()).FirstOrDefaultAsync(cancellationToken);
    }

    public IAsyncEnumerable<TResult> GetReadOnlyListAsync<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder)
        where TEntity : class
    {
        return queryBuilder(_context.Set<TEntity>().AsNoTracking()).AsAsyncEnumerable();
    }

    public Task<bool> AnyAsync<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return queryBuilder(_context.Set<TEntity>().AsNoTracking()).AnyAsync(cancellationToken);
    }

    public async Task AddAsync<TEntity>(TEntity item, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await _context.AddAsync(item, cancellationToken);
    }

    public void Remove<TEntity>(TEntity item) where TEntity : class
    {
        _context.Remove(item);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}