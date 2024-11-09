using GeekShopping.Web.DTO;
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
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ICartService _cartService;

    public HomeController(
        IProductRepository productRepository,
        ICartRepository cartRepository,
        ICartService cartService,
        ILogger<HomeController> logger
    ) {
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _cartService = cartService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      var products = await _productRepository.FindAll();
      return View(products);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var product = await _productRepository.FindById(id);
        return View(product);
    }

    [Authorize]
    [HttpPost]
    [ActionName("Details")]
    public async Task<IActionResult> PostDetails(ProductDTO product)
    {
        CartDTO cart = new CartDTO() {
            CartHeader = new CartHeaderDTO() {
                UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
            }
        };

        CartDetailDTO cartDetail = new CartDetailDTO() {
            Count = product.Count,
            ProductId = product.Id,
            Product = await _productRepository.FindById(product.Id)
        };

        List<CartDetailDTO> cartDetails = new List<CartDetailDTO>();
        cartDetails.Add(cartDetail);
        cart.CartDetails = cartDetails;

        var response = await _cartRepository.SaveOrUpdateCart(cart);
        if (response != null) {
            return RedirectToAction(nameof(Index));
        }

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