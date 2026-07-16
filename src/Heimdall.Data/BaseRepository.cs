using FluentResults;
using Heimdall.Data.Extensions;
using Heimdall.Abstractions;
using Heimdall.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ODataQuery;
using static FluentResults.Result;
using static Heimdall.Data.ErrorCodes;

namespace Heimdall.Data;

public abstract class BaseRepository<TEntity, TContext>(ILogger<IRepository<TEntity>> logger, TContext dbContext) : IRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
{
    private const int MaxPageSize = 1000;

    protected readonly ILogger _logger = logger;
    protected readonly TContext _dbContext = dbContext;
    protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    protected readonly string _entityName = typeof(TEntity).Name;

    public async Task<Result<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.EntityAdded(entity.Id, _entityName);

        return Ok(entity);
    }

    public async Task<Result<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.EntityUpdated(entity.Id, _entityName);

        return Ok(entity);
    }

    public async Task<Result<TEntity>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        if (entity is null)
        {
            return Result.WithRootError(EntityNotFound);
        }

        entity.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.EntityDeleted(id, _entityName);

        return Ok(entity);
    }

    public async Task<Result<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        return entity is not null ? entity : Result.WithRootError(EntityNotFound);
    }

    public Task<List<TEntity>> ListAsync(string? filter, string? orderBy, int page, int count, CancellationToken cancellationToken = default)
    {
        page = Math.Max(page, 1);
        count = Math.Clamp(count, 1, MaxPageSize);

        var query = _dbSet.ODataFilter(filter);
        query = string.IsNullOrWhiteSpace(orderBy)
            ? query.OrderBy(entity => entity.Id)
            : query.ODataOrderBy(orderBy);

        return query.Skip((page - 1) * count)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
