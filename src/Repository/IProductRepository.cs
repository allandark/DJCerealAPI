using CerealAPI.src.Models;

namespace CerealAPI.src.Repository
{
    /// <summary>
    /// Interface to be injected into the ProductController. Its purpose is to abstract away the database calls
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Returns all products in database
        /// </summary>
        /// <returns></returns>
        ICollection<Product> GetAll();
        
        /// <summary>
        /// Return all products given a filter list
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        ICollection<Product> GetAllFiltered(List<ProductFilterEntity> filter);
        
        /// <summary>
        /// Returns product with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product or null if product was not found</returns>
        Product Get(int id);

        /// <summary>
        /// Checks the database for product with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if product exists, false if not</returns>
        bool Exists(int id);

        /// <summary>
        /// Creates a product and upload it do databse
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product if sucessful or null if a a failure occured</returns>
        Product Create(Product product);

        /// <summary>
        /// Modifies product and uploads it to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product if sucessful or null if a a failure occured</returns>
        Product Update(Product product);

        /// <summary>
        /// Removes product from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if successful. False if no product was found at id</returns>
        bool Delete(int id);
    }
}
