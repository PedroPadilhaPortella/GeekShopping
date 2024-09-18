using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;

namespace GeekShopping.Web.Controllers
{
  public class ProductController : Controller
  {
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
      _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
      var accessToken = await GetAccessToken();
      var products = await _productService.FindAll(accessToken);
      return View(products);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Product product)
    {
      if (ModelState.IsValid)
      {
        var accessToken = await GetAccessToken();
        var response = await _productService.Create(product, accessToken);
        if (response != null) return RedirectToAction(nameof(Index));
      }
      return View(product);
    }

    public async Task<IActionResult> Update(int id)
    {
      var accessToken = await GetAccessToken();
      var product = await _productService.FindById(id, accessToken);
      return View(product);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Update(Product product)
    {
      if (ModelState.IsValid)
      {
        var accessToken = await GetAccessToken();
        var response = await _productService.Update(product, accessToken);
        if (response != null) return RedirectToAction(nameof(Index));
      }
      return View(product);
    }

    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
      var accessToken = await GetAccessToken();
      var model = await _productService.FindById(id, accessToken);
      if (model != null) return View(model);
      return NotFound();
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> Delete(Product product)
    {
      var accessToken = await GetAccessToken();
      var response = await _productService.Delete(product.Id, accessToken);
      if (response) return RedirectToAction(nameof(Index));
      return View(product);
    }

    private Task<string> GetAccessToken()
    {
        return HttpContext.GetTokenAsync("access_token");
    }
  }
}
