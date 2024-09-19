using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
  public class ProductService : IProductService
  {
    private readonly HttpClient _httpClient;
    public const string basePath = "api/v1/product";

    public ProductService(HttpClient httpClient)
    {
      _httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
    }

    public async Task<IEnumerable<Product>> FindAll(string token)
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
      var response = await _httpClient.GetAsync(basePath);
      return await response.ReadContentAs<List<Product>>();
    }

    public async Task<Product> FindById(long id, string token)
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
      var response = await _httpClient.GetAsync($"{basePath}/{id}");
      return await response.ReadContentAs<Product>();
    }

    public async Task<Product> Create(Product product, string token)
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
      var response = await _httpClient.PostAsJson(basePath, product);
      if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
      return await response.ReadContentAs<Product>();
    }

    public async Task<Product> Update(Product product, string token)
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
      var response = await _httpClient.PutAsJson(basePath, product);
      if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
      return await response.ReadContentAs<Product>();
    }

    public async Task<bool> Delete(long id, string token)
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
      var response = await _httpClient.DeleteAsync($"{basePath}/{id}");
      if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
      return await response.ReadContentAs<bool>();
    }
  }
}
