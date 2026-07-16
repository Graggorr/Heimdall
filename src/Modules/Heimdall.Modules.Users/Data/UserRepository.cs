using Heimdall.Data;
using Heimdall.Modules.Users.Contracts;
using Heimdall.Modules.Users.Domain;
using Microsoft.Extensions.Logging;

namespace Heimdall.Modules.Users.Data;

internal sealed class UserRepository(ILogger<IRepository<User>> logger, UsersDbContext dbContext)
    : BaseRepository<User, UsersDbContext>(logger, dbContext), IUserRepository;
