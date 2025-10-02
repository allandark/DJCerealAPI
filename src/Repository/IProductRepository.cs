using CerealAPI.src.Models;

namespace CerealAPI.src.Repository
{
    public interface IProductRepository
    {
        ICollection<Product> GetAll();
        ICollection<Product> GetAllFiltered(List<ProductFilterEntity> filter);
        Product Get(int id);
        bool Exists(int id);
        Product Create(Product product);
        Product Update(Product product);
        bool Delete(int id);
    }
}
