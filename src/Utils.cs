using CerealAPI.src.Data;
using CerealAPI.src.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CerealAPI.src
{
    public class Utils
    {


        static public string[] FilterOps = new string[]
        {
            "=",
            ">=",
            "<=",
            ">",
            "<",
            "!="
        };

        static public Dictionary<string, string> ProductMemberNames = new Dictionary<string, string>
        {
            {"name","Name" },
            {"manufacturer","Mfr" },            
            {"type","Type" },
            {"calories","Calories" },
            {"protien","Protien" },
            {"fat","Fat" },
            {"sodium","Sodium" },
            {"fiber","Fiber" },
            {"carbo","Carbo" },
            {"sugars","Sugars" },
            {"potass","Potass" },
            {"vitamins","Vitamins" },
            {"shelf","Shelf" },
            {"weight","Weight" },
            {"cups","Cups" },
            {"rating","Rating" }
        };

      





        static public List<string[]> ReadCSVFile(string path, char delim = ',')
        {
            List<string[]> rows = new List<string[]>();
            try
            {
                string[] lines = File.ReadAllLines(path);

                for (var i = 0; i < lines.Count(); i++)
                {
                    string[] values = lines[i].Split(delim);
                    rows.Add(values);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return rows;
        }

        


        public int ReadAll(AppDbContext context, string category, string value, out List<Product> productList)
        {
            productList = null;
            IEnumerable<Product> products = new List<Product>();
            try
            {
                var productsAll = context.Products.ToList();

                    switch(category)
                    {
                        case "manufacturer":
                            products = from p in productsAll
                                       where p.Mfr == value.ToUpper()
                                       select p;
                            break;
                        case "type":
                            products = from p in productsAll
                                       where p.Type == value.ToUpper()
                                       select p;
                            break;
                        case "calories":
                            products = from p in productsAll
                                       where p.Calories == int.Parse(value)
                                       select p;
                            break;
                        case "protien":
                            products = from p in productsAll
                                       where p.Protien == int.Parse(value)
                                       select p;
                            break;
                        case "fat":
                            products = from p in productsAll
                                       where p.Fat == int.Parse(value)
                                       select p;
                            break;
                        case "sodium":
                            products = from p in productsAll
                                       where p.Sodium == int.Parse(value)
                                       select p;
                            break;
                        case "fiber":
                            products = from p in productsAll
                                       where p.Fiber == float.Parse(value)
                                       select p;
                            break;
                        case "carbo":
                            products = from p in productsAll
                                       where p.Carbo == float.Parse(value)
                                       select p;
                            break;
                        case "sugars":
                            products = from p in productsAll
                                       where p.Sugars == int.Parse(value)
                                       select p;
                            break;
                        case "potass":
                            products = from p in productsAll
                                       where p.Potass == int.Parse(value)
                                       select p;
                            break;
                        case "vitamins":
                            products = from p in productsAll
                                       where p.Vitamins == int.Parse(value)
                                       select p;
                            break;
                        case "shelf":
                            products = from p in productsAll
                                       where p.Shelf == int.Parse(value)
                                       select p;
                            break;
                        case "weight":
                            products = from p in productsAll
                                       where p.Weight == float.Parse(value)
                                       select p;
                            break;
                        case "cups":
                            products = from p in productsAll
                                       where p.Cups == float.Parse(value)
                                       select p;
                            break;
                        case "rating":
                            products = from p in productsAll
                                       where p.Rating == float.Parse(value)
                                       select p;
                            break;
                    default:
                        products = productsAll;
                        break;
                    } 
                productList = products.ToList();
                return StatusCodes.Status200OK;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return StatusCodes.Status500InternalServerError;
        }

      
    }
}
