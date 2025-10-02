using CerealAPI.src.Data;
using CerealAPI.src.Models;
using Microsoft.EntityFrameworkCore;

namespace CerealAPI.src
{
    /// <summary>
    /// Class for populating the database. Note: Should be called with: "dotnet run seed"
    /// </summary>
    public class Seed
    {
  
        /// <summary>
        /// Function to parse the csv file and upload it to the database
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="dataPath"></param>
        static public void SeedDatabase(AppDbContext context, string dataPath)
        {
            context.Database.EnsureCreated();
            var lines = Utils.ReadFile(dataPath);

            for (int i = 2, j = 0; i < lines.Count; i++, j++)
            {
                Product p = ProductFactory.CreateProduct(lines[i][0]);
                context.Products.Add(p);
                Console.WriteLine(string.Format("{0}. Product created: {1}", j, p.Name));

            }
            context.SaveChanges();
        }

    }
}
