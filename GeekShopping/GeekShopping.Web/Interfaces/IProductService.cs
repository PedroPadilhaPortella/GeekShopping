using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
  public interface IProductService
  {
    Task<IEnumerable<Product>> FindAll(string token);
    Task<Product> FindById(long id, string token);
    Task<Product> Create(Product product, string token);
    Task<Product> Update(Product product, string token);
    Task<bool> Delete(long id, string token);
  }
}
