using FluentResults;
using Heimdall.Common.Extensions;
using Heimdall.Data.Common;
using Heimdall.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ODataQuery;
using static Heimdall.Data.ErrorCodes;

namespace Heimdall.Data.Repositories.Internal;

internal sealed class UserRepository(ILogger<IRepository<User>> logger, ApplicationDbContext dbContext) 
    : BaseRepository<User>(logger, dbContext), IUserRepository
{
    public async Task<Result<Wallet>> GetWalletById(Guid userId, Guid walletId, CancellationToken cancellationToken = default)
    {
        var user = await GetUserByIdWithIncludes(userId, cancellationToken);

        if (user is null)
        {
            return Result.WithRootError(UserNotFound);
        }
        
        var wallet = user.Wallets.FirstOrDefault(wallet => wallet.Id == walletId && !wallet.IsDeleted);
        
        return wallet is not null ? wallet : Result.WithRootError(WalletNotFound);
    }

    public async Task<List<Wallet>> GetWalletsByFilter(Guid userId, string? filter, string? orderBy, CancellationToken cancellationToken = default)
    {
        var user = await GetUserByIdWithIncludes(userId, cancellationToken);

        return user is not null
            ? await _dbContext.Wallets.ODataFilter(filter).ODataOrderBy(orderBy).Include(wallet => wallet.Transactions)
                .ToListAsync(cancellationToken)
            : [];
    }

    public async Task<Result<Transaction>> GetTransactionById(Guid userId, Guid walletId, Guid transactionId, CancellationToken cancellationToken = default)
    {
        var walletResult = await GetWalletById(userId, walletId, cancellationToken);

        if (walletResult.IsFailed)
        {
            return walletResult.ToResult();
        }

        var transaction = walletResult.Value.Transactions.FirstOrDefault(transaction => transaction.Id == transactionId);

        return transaction is null ? Result.WithRootError(TransactionNotFound) : transaction;
    }

    public async Task<List<Transaction>> GetTransactionsByFilter(Guid userId, string? filter, string? orderBy,
        CancellationToken cancellationToken = default)
    {
        var user = await GetUserByIdWithIncludes(userId, cancellationToken);
        
        return user is not null
            ? await _dbContext.Transactions.ODataFilter(filter).ODataOrderBy(orderBy).ToListAsync(cancellationToken)
            : [];
    }
    
    private Task<User?> GetUserByIdWithIncludes(Guid userId, CancellationToken cancellationToken = default)
    => _dbSet.Include(user => user.Wallets).ThenInclude(wallet => wallet.Transactions)
        .FirstOrDefaultAsync(user => user.Id == userId && !user.IsDeleted, cancellationToken);
}
