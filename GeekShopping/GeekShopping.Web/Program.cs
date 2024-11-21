using Duende.IdentityServer.Services;
using GeekShopping.Web.Data;
using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services;
using GeekShopping.Web.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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

builder.Services
  .AddAuthentication(options =>
  {
      options.DefaultScheme = "Cookies";
      options.DefaultChallengeScheme = "oidc";
  })
  .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
  .AddOpenIdConnect("oidc", options =>
  {
      options.Authority = "https://localhost:4430/";
      options.GetClaimsFromUserInfoEndpoint = true;
      options.ClientId = "geek_shopping";
      options.ClientSecret = "my_super_secret";
      options.ResponseType = OpenIdConnectResponseType.Code;
      options.ClaimActions.MapJsonKey("role", "role", "role");
      options.ClaimActions.MapJsonKey("sub", "sub", "sub");
      options.TokenValidationParameters.NameClaimType = "name";
      options.TokenValidationParameters.RoleClaimType = "role";
      options.Scope.Add("geek_shopping");
      options.SaveTokens = true;
  });

builder.Services.AddScoped<IDatabaseSeedService, DatabaseSeedService>();

builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddSingleton(AutoMapperConfiguration.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<ICouponRepository, CouponRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builderServices.AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();

var initializer = app.Services.CreateScope().ServiceProvider.GetService<IDatabaseSeedService>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

initializer.Seed();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
