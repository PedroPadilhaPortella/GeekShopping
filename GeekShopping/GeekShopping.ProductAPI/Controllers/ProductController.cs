using GeekShopping.ProductAPI.DTO;
using GeekShopping.ProductAPI.Repository;
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
    public async Task<ActionResult<List<ProductDTO>>> GetAll()
    {
      var products = await _productRepository.FindAll();
      return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetById(long id)
    {
      var product = await _productRepository.FindById(id);
      if (product.Id <= 0) return NotFound();
      return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDTO)
    {
      if (productDTO == null) return BadRequest();
      var product = await _productRepository.Create(productDTO);
      return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDTO>> Put(ProductDTO productDTO)
    {
      if (productDTO == null) return BadRequest();
      var product = await _productRepository.Update(productDTO);
      return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(long id)
    {
      bool status = await _productRepository.Delete(id);
      if (!status) return BadRequest();
      return Ok(status);
    }
  }
}