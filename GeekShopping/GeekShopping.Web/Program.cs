using GeekShopping.Web.Data;
using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Repository;
using GeekShopping.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
    builder.Configuration["ConnectionStrings:MySqlConnectionString"],
    new MySqlServerVersion(new Version(8, 0, 39)))
);

builder.Services.AddSingleton(AutoMapperConfiguration.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICouponRepository, CouponRepository>();

//builder.Services.AddHttpClient<IProductService, ProductService>(
//    s => s.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
//);

builder.Services.AddHttpClient<ICartService, CartService>(
    s => s.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"])
);

//builder.Services.AddHttpClient<ICouponService, CouponService>(
//    s => s.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"])
//);

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
