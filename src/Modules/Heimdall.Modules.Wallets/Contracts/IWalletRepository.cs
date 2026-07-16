using FluentResults;
using Heimdall.Data;
using Heimdall.Modules.Wallets.Domain;

namespace Heimdall.Modules.Wallets.Contracts;

public interface IWalletRepository : IRepository<Wallet>
{
    public Task<Result<Wallet>> GetByIdAsync(Guid userId, Guid walletId, CancellationToken cancellationToken = default);

    public Task<List<Wallet>> ListByUserAsync(Guid userId, string? filter, string? orderBy, CancellationToken cancellationToken = default);

    public Task<Result<Transaction>> GetTransactionByIdAsync(Guid userId, Guid walletId, Guid transactionId, CancellationToken cancellationToken = default);

    public Task<List<Transaction>> ListTransactionsAsync(Guid userId, Guid walletId, string? filter, string? orderBy, CancellationToken cancellationToken = default);
}
