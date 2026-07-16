using Microsoft.Extensions.Logging;
using static Microsoft.Extensions.Logging.LogLevel;

namespace Heimdall.Data.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(Level = Debug, Message = "A new {EntityName} has been added with ID {Id}")]
    public static partial void EntityAdded(this ILogger logger, Guid id, string entityName);

    [LoggerMessage(Level = Debug, Message = "{EntityName} with ID {Id} has been updated.")]
    public static partial void EntityUpdated(this ILogger logger, Guid id, string entityName);

    [LoggerMessage(Level = Debug, Message = "{EntityName} with ID {Id} has been deleted.")]
    public static partial void EntityDeleted(this ILogger logger, Guid id, string entityName);
}
