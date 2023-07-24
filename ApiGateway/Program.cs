using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json",optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddJwtAuthentication();

var app = builder.Build();
await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
