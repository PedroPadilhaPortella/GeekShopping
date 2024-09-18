using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.Web.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;

    public HomeController(IProductService productService, ILogger<HomeController> logger)
    {
      _logger = logger;
      _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      var products = await _productService.FindAll(accessToken);
      return View(products);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var product = await _productService.FindById(id, accessToken);
        return View(product);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public IActionResult Login()
    {
      return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout()
    {
      return SignOut("Cookies", "oidc");
    }
  }
}