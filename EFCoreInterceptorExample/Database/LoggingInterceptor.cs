using EFCoreInterceptorExample.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;

namespace EFCoreInterceptorExample.Database;

public class LoggingInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        LogChanges(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        LogChanges(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void LogChanges(DbContext? dbContext)
    {
        if (dbContext == null)
        {
            return;
        }

        var logEntries = dbContext.ChangeTracker.Entries()
            .Where(p => p.Entity is ILoggable && (p.State == EntityState.Added || p.State == EntityState.Modified || p.State == EntityState.Deleted))
            .Select(p => new Log()
            {
                EntityName = p.Entity.GetType().Name,
                Operation = p.State.ToString(),
                EntityId = ((ILoggable)p.Entity).Id,
                BeforeState = p.State == EntityState.Added ? null : JsonSerializer.Serialize(p.OriginalValues.ToObject()),
                AfterState = p.State == EntityState.Deleted ? null : JsonSerializer.Serialize(p.CurrentValues.ToObject()),
            }).ToList();

        if (logEntries.Count != 0)
        {
            dbContext.Set<Log>().AddRange(logEntries);
        }
    }
}
