using Heimdall.Modules.Users;
using Heimdall.Modules.Wallets;
using ServiceCollection.Extensions.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterModule(new UsersModule(builder.Configuration))
    .RegisterModule(new WalletsModule(builder.Configuration));
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.Run();
