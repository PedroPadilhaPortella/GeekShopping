using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IProductService, ProductService>(
    s => s.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
);

builder.Services.AddHttpClient<ICartService, CartService>(
    s => s.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"])
);

builder.Services.AddHttpClient<ICouponService, CouponService>(
    s => s.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"])
);

builder.Services.AddHttpClient<IOrderService, OrderService>(
    s => s.BaseAddress = new Uri(builder.Configuration["ServiceUrls:OrderAPI"])
);

// Configure IdentityServer Authentication
builder.Services
  .AddAuthentication(options =>
  {
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
  })
  .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
  .AddOpenIdConnect("oidc", options =>
  {
    options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
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

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console(LogEventLevel.Debug));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
