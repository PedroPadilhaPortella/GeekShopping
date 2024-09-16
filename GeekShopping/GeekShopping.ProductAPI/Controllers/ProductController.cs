using GeekShopping.ProductAPI.DTO;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private IProductRepository _productRepository;

    public ProductController(IProductRepository repository)
    {
      _productRepository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<ProductDTO>>> GetAll()
    {
      var products = await _productRepository.FindAll();
      return Ok(products);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ProductDTO>> GetById(long id)
    {
      var product = await _productRepository.FindById(id);
      if (product.Id <= 0) return NotFound();
      return Ok(product);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO productDTO)
    {
      if (productDTO == null) return BadRequest();
      var product = await _productRepository.Create(productDTO);
      return Ok(product);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<ProductDTO>> Put([FromBody] ProductDTO productDTO)
    {
      if (productDTO == null) return BadRequest();
      var product = await _productRepository.Update(productDTO);
      return Ok(product);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<bool>> Delete(long id)
    {
      bool status = await _productRepository.Delete(id);
      if (!status) return BadRequest();
      return Ok(status);
    }
  }
}