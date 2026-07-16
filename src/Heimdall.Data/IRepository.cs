using FluentResults;
using Heimdall.Abstractions;

namespace Heimdall.Data;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    public Task<Result<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task<Result<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task<Result<TEntity>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<Result<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<List<TEntity>> ListAsync(string? filter, string? orderBy, int page, int count, CancellationToken cancellationToken = default);
}
