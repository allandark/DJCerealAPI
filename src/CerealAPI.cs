using Microsoft.EntityFrameworkCore;

namespace CerealAPI.src
{
    public class CerealAPI
    {
        /// <summary>
        /// Singleton Instance 
        /// </summary>
        private static CerealAPI instance = null;

        /// <summary>
        /// Mutex for get instance
        /// </summary>
        private static readonly object padlock = new object();
        public static CerealAPI Instance
        { 
            get
            {
                lock(padlock)
                {
                    if(instance == null)
                    {
                        instance = new CerealAPI();
                    }
                    return instance;
                }
            } 
        }


        private AppDbContext dbContext;

        /// <summary>
        /// Contructor for Cereal API class
        /// </summary>
        private CerealAPI()
        {
            dbContext = InitializeDB();
        }

        

        public void Create(Product p)
        {
            using(dbContext)
            {
                dbContext.Products.Add(p);
                dbContext.SaveChanges();
            }
        }

        public Product Read(int id)
        {
            Product p = null;
            using (dbContext)
            {
                try
                {
                    p = dbContext.Products.ElementAt(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                dbContext.SaveChanges();               
            }
            return p;
        }

        public void Update(int id, Product p)
        {
            using (dbContext)
            {
                try
                {
                    // TODO: implement update
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Delete(int id) 
        {
            // TODO: implement update
        }


        private AppDbContext InitializeDB() 
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();


            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(
                    config.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection"))
                ));

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<AppDbContext>();     
        }

    }
}
