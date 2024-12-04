using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:4435/";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddOcelot();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console(LogEventLevel.Debug));


var app = builder.Build();

await app.UseOcelot();

app.Run();