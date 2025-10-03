using CerealAPI.src.Data;
using CerealAPI.src.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CerealAPI.src.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
      

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        


        //--- CRUD Definitions ---//

        /// <summary>
        /// Implements interface
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Implements interface
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Implements interface
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product Get(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }


        /// <summary>
        /// Implements interface
        /// </summary>
        /// <returns></returns>
        public ICollection<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// Implements interface
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ICollection<Product> GetAllFiltered(List<ProductFilterEntity> filter)
        {
            List<Product> products = null;
            try
            {
                // Apply filtering
                products = _context.Products.ToList();
                foreach (var entry in filter)
                {
                    if(entry.Key == "sort")
                    {
                        continue;
                    }
                    products = ProductFilter.FilterTable[entry.Operator].Invoke(entry, products);
                }
                // Only sort the first query  
                var sort_filter = filter.Where(p => p.Key == "sort").ToList();
                products = ProductFilter.Sort(products, sort_filter[0]); 

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }  
            return products;
        }

        /// <summary>
        /// Implements interface
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        /// <summary>
        /// Imlpements interface
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
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
