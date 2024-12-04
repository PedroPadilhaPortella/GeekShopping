using Duende.IdentityServer.Services;
using GeekShopping.IdentityServer.Configurations;
using GeekShopping.IdentityServer.Data;
using GeekShopping.IdentityServer.Models;
using GeekShopping.IdentityServer.Seed;
using GeekShopping.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
    builder.Configuration["ConnectionStrings:MySqlConnectionString"],
    new MySqlServerVersion(new Version(8, 0, 39)))
);

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var builderServices = builder.Services.AddIdentityServer(options =>
{
  options.Events.RaiseErrorEvents = true;
  options.Events.RaiseInformationEvents = true;
  options.Events.RaiseFailureEvents = true;
  options.Events.RaiseSuccessEvents = true;
  options.EmitStaticAudienceClaim = true;
})
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<ApplicationUser>();

builder.Services.AddScoped<IDatabaseSeed, DatabaseSeed>();

builder.Services.AddScoped<IProfileService, ProfileService>();

builderServices.AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console(LogEventLevel.Debug));


var app = builder.Build();

var initializer = app.Services.CreateScope().ServiceProvider.GetService<IDatabaseSeed>();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

initializer.Seed();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
