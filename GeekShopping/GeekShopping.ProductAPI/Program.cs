using GeekShopping.ProductAPI.Data;
using GeekShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
    builder.Configuration["ConnectionStrings:MySqlConnectionString"],
    new MySqlServerVersion(new Version(8, 0, 39)))
);

builder.Services.AddSingleton(AutoMapperConfiguration.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();

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
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.ProductAPI", Version = "v1" });
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
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
