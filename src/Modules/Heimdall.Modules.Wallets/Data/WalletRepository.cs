using FluentResults;
using Heimdall.Data;
using Heimdall.Modules.Wallets.Contracts;
using Heimdall.Modules.Wallets.Domain;
using Heimdall.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ODataQuery;
using static Heimdall.Modules.Wallets.ErrorCodes;

namespace Heimdall.Modules.Wallets.Data;

internal sealed class WalletRepository(ILogger<IRepository<Wallet>> logger, WalletsDbContext dbContext)
    : BaseRepository<Wallet, WalletsDbContext>(logger, dbContext), IWalletRepository
{
    public async Task<Result<Wallet>> GetByIdAsync(Guid userId, Guid walletId, CancellationToken cancellationToken = default)
    {
        var wallet = await _dbSet.FirstOrDefaultAsync(wallet => wallet.Id == walletId && wallet.UserId == userId, cancellationToken);

        return wallet is not null ? wallet : Result.WithRootError(WalletNotFound);
    }

    public Task<List<Wallet>> ListByUserAsync(Guid userId, string? filter, string? orderBy, CancellationToken cancellationToken = default) =>
        _dbSet.Where(wallet => wallet.UserId == userId)
            .ODataFilter(filter)
            .ODataOrderBy(orderBy)
            .ToListAsync(cancellationToken);

    public async Task<Result<Transaction>> GetTransactionByIdAsync(Guid userId, Guid walletId, Guid transactionId, CancellationToken cancellationToken = default)
    {
        if (!await OwnsWalletAsync(userId, walletId, cancellationToken))
        {
            return Result.WithRootError(WalletNotFound);
        }

        var transaction = await _dbContext.Transactions
            .FirstOrDefaultAsync(transaction => transaction.Id == transactionId && transaction.WalletId == walletId, cancellationToken);

        return transaction is not null ? transaction : Result.WithRootError(TransactionNotFound);
    }

    public async Task<List<Transaction>> ListTransactionsAsync(Guid userId, Guid walletId, string? filter, string? orderBy, CancellationToken cancellationToken = default)
    {
        if (!await OwnsWalletAsync(userId, walletId, cancellationToken))
        {
            return [];
        }

        return await _dbContext.Transactions.Where(transaction => transaction.WalletId == walletId)
            .ODataFilter(filter)
            .ODataOrderBy(orderBy)
            .ToListAsync(cancellationToken);
    }

    private Task<bool> OwnsWalletAsync(Guid userId, Guid walletId, CancellationToken cancellationToken) =>
        _dbSet.AnyAsync(wallet => wallet.Id == walletId && wallet.UserId == userId, cancellationToken);
}
