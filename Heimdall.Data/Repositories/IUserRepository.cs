using FluentResults;
using Heimdall.Data.Common;
using Heimdall.Data.Entities;

namespace Heimdall.Data.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<Result<Wallet>> GetWalletById(Guid userId, Guid walletId, CancellationToken cancellationToken = default);
    
    public Task<List<Wallet>> GetWalletsByFilter(Guid userId, string? filter, string? orderBy, CancellationToken cancellationToken = default);
    
    public Task<Result<Transaction>> GetTransactionById(Guid userId, Guid walletId, Guid transactionId, CancellationToken cancellationToken = default);
    
    public Task<List<Transaction>> GetTransactionsByFilter(Guid userId, string? filter, string? orderBy, CancellationToken cancellationToken = default);
}
