using GeekShopping.PaymentAPI.MessageConsumer;
using GeekShopping.PaymentAPI.MessageSender;
using GeekShopping.PaymentProcessor;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProcessPayment, ProcessPayment>();
builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
builder.Services.AddHostedService<RabbitMQPaymentConsumer>();

builder.Services.AddControllers();

builder.Services
  .AddAuthentication("Bearer")
  .AddJwtBearer("Bearer", options =>
  {
      options.Authority = "https://localhost:4435/";
      options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
  });

builder.Services
  .AddAuthorization(options =>
  {
      options.AddPolicy("ApiScope", policy =>
      {
          policy.RequireAuthenticatedUser();
          policy.RequireClaim("scope", "geek_shopping");
      });
  });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.PaymentAPI", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token!",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In= ParameterLocation.Header
          },
          new List<string> ()
        }
     });
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console(LogEventLevel.Debug));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekShopping.PaymentAPI v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
