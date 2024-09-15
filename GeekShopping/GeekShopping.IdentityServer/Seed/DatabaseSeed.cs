using System.Security.Claims;
using GeekShopping.IdentityServer.Configurations;
using GeekShopping.IdentityServer.Data;
using GeekShopping.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace GeekShopping.IdentityServer.Seed
{
  public class DatabaseSeed : IDatabaseSeed
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _user;
    private readonly RoleManager<IdentityRole> _role;

    public DatabaseSeed(
      ApplicationDbContext context,
      UserManager<ApplicationUser> user,
      RoleManager<IdentityRole> role
    )
    {
      _context = context;
      _user = user;
      _role = role;
    }

    public void Seed()
    {
      if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

      // Create Roles If Not Exists
      _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
      _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

      // Create Admin User and their Roles
      ApplicationUser admin = new ApplicationUser()
      {
        UserName = "pedro-admin",
        Email = "pedro.admin@gmail.com.br",
        EmailConfirmed = true,
        PhoneNumber = "+55 (11) 12345-6789",
        FirstName = "Pedro",
        LastName = "Portella"
      };

      _user.CreateAsync(admin, "Pedro123@").GetAwaiter().GetResult();

      _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

      var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
      {
        new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
        new Claim(JwtClaimTypes.GivenName, admin.FirstName),
        new Claim(JwtClaimTypes.FamilyName, admin.LastName),
        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
      }).Result;

      // Create Client User and their Roles
      ApplicationUser client = new ApplicationUser()
      {
        UserName = "pedro-client",
        Email = "pedro.client@gmail.com.br",
        EmailConfirmed = true,
        PhoneNumber = "+55 (11) 12345-6789",
        FirstName = "Pedro",
        LastName = "Henrique"
      };

      _user.CreateAsync(client, "Pedro123@").GetAwaiter().GetResult();

      _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

      var clientClaims = _user.AddClaimsAsync(admin, new Claim[]
      {
        new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
        new Claim(JwtClaimTypes.GivenName, client.FirstName),
        new Claim(JwtClaimTypes.FamilyName, client.LastName),
        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
      }).Result;
    }
  }
}
