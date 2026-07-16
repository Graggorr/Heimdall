using FluentResults;
using Heimdall.Common.Extensions;
using Heimdall.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ODataQuery;
using static FluentResults.Result;
using static Heimdall.Data.ErrorCodes;

namespace Heimdall.Data.Common;

internal abstract class BaseRepository<TEntity>(ILogger<IRepository<TEntity>> logger, ApplicationDbContext dbContext) : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly ILogger _logger = logger;
    protected readonly ApplicationDbContext _dbContext = dbContext;
    protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    protected readonly string _tableName = nameof(TEntity);
    
    public async Task<Result> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.EntityAdded(entity.Id, _tableName);
        
        return Ok();
    }

    public async Task<Result<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Ok(entity);
    }

    public async Task<Result<TEntity>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        if (entity is null)
        {
            return Ok();
        }
        
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Ok(entity);
    }

    public async Task<Result<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted, cancellationToken);

        return entity is not null ? entity : Result.WithRootError(EntityNotFound);
    }

    public Task<List<TEntity>> ListAsync(string? filter, string? orderBy, int page, int count, CancellationToken cancellationToken) => 
        _dbSet.ODataFilter(filter)
            .ODataOrderBy(orderBy)
            .Skip((page - 1) * count)
            .Take(count)
            .ToListAsync(cancellationToken);
}
