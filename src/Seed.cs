using CerealAPI.src.Data;
using CerealAPI.src.Models;
using Microsoft.EntityFrameworkCore;

namespace CerealAPI.src
{
    public class Seed
    {
  
        static public void SeedDatabase(AppDbContext context, string dataPath)
        {
            context.Database.EnsureCreated();
            var lines = Utils.ReadCSVFile(dataPath);

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
