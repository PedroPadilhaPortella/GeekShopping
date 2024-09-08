using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
  public class ProductService : IProductService
  {
    private readonly HttpClient _client;

    public ProductService(HttpClient client)
    {
      _client = client ?? throw new ArgumentException(nameof(client));
    }

    public const string basePath = "api/v1/product";

    public async Task<IEnumerable<Product>> FindAll()
    {
      var response = await _client.GetAsync(basePath);
      return await response.ReadContentAs<List<Product>>();
    }

    public async Task<Product> FindById(long id)
    {
      var response = await _client.GetAsync($"{basePath}/{id}");
      return await response.ReadContentAs<Product>();
    }

    public async Task<Product> Create(Product product)
    {
      var response = await _client.PostAsJson(basePath, product);
      if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
      return await response.ReadContentAs<Product>();
    }

    public async Task<Product> Update(Product product)
    {
      var response = await _client.PutAsJson(basePath, product);
      if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
      return await response.ReadContentAs<Product>();
    }

    public async Task<bool> Delete(long id)
    {
      var response = await _client.DeleteAsync($"{basePath}/{id}");
      if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
      return await response.ReadContentAs<bool>();
    }
  }
}
