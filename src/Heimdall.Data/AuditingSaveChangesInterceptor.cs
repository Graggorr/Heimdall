using Heimdall.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Heimdall.Data;

public sealed class AuditingSaveChangesInterceptor(TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Stamp(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        Stamp(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void Stamp(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }

        var utcNow = timeProvider.GetUtcNow().UtcDateTime;

        foreach (var entry in dbContext.ChangeTracker.Entries<ICreated>())
        {
            if (entry.State is EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
            }
        }

        foreach (var entry in dbContext.ChangeTracker.Entries<ILastUpdated>())
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.LastUpdatedAt = utcNow;
            }
        }
    }
}
