using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using GeekShopping.Web.DTO;

namespace GeekShopping.Web.Controllers
{
  public class ProductController : Controller
  {
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
      var accessToken = await GetAccessToken();
      var products = await _productRepository.FindAll();
      return View(products);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(ProductDTO product)
    {
      if (ModelState.IsValid)
      {
        var accessToken = await GetAccessToken();
        var response = await _productRepository.Create(product);
        if (response != null) return RedirectToAction(nameof(Index));
      }
      return View(product);
    }

    public async Task<IActionResult> Update(int id)
    {
      var accessToken = await GetAccessToken();
      var product = await _productRepository.FindById(id);
      return View(product);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Update(ProductDTO product)
    {
      if (ModelState.IsValid)
      {
        var accessToken = await GetAccessToken();
        var response = await _productRepository.Update(product);
        if (response != null) return RedirectToAction(nameof(Index));
      }
      return View(product);
    }

    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
      var accessToken = await GetAccessToken();
      var model = await _productRepository.FindById(id);
      if (model != null) return View(model);
      return NotFound();
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> Delete(Product product)
    {
      var accessToken = await GetAccessToken();
      var response = await _productRepository.Delete(product.Id);
      if (response) return RedirectToAction(nameof(Index));
      return View(product);
    }

    private Task<string> GetAccessToken()
    {
        return HttpContext.GetTokenAsync("access_token");
    }
  }
}
