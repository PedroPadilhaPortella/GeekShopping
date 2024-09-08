using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
  public interface IProductService
  {
    Task<IEnumerable<Product>> FindAll();
    Task<Product> FindById(long id);
    Task<Product> Create(Product product);
    Task<Product> Update(Product product);
    Task<bool> Delete(long id);
  }
}
