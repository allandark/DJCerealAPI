using CerealAPI.src.Data;
using CerealAPI.src.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CerealAPI.src.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        // TODO imlpement
        Dictionary<string, Func<ProductFilterEntity, List<Product>, List<Product>>> FilterTable = 
            new Dictionary<string, Func<ProductFilterEntity, List<Product>, List<Product>>>
        {
            {   "=", 
                (filter, products ) =>
                {
                    return products.Where(p =>
                    {
                        var prop = typeof(Product).GetProperty(filter.Key);
                        if (prop == null) return false;
                        var value = prop.GetValue(p);                        
                        return value != null && value.Equals(filter.Value );                       
                    }).ToList();                    
                }                            
            }
        };


        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        


        // CRUD Definitions
        public Product Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                Product p = _context.Products.Find(id);
                _context.Products.Remove(p);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }            
        }

        public Product Get(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public ICollection<Product> GetAllFiltered(List<ProductFilterEntity> filter)
        {
            List<Product> products = null;
            try
            {
                products = _context.Products.ToList();
                foreach (var entry in filter)
                {
                    products = FilterTable[entry.Operator].Invoke(entry, products);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }  
            return products;
        }

        public bool Exists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public Product Update(Product product)
        { 
            try
            {
                _context.Products.Attach(product);
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return product;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }            
        }
    }
}
