using CerealAPI.src.Data;
using CerealAPI.src.Models;
using CerealAPI.src.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace CerealAPI.src
{
    /// <summary>
    /// Utility class
    /// </summary>
    public class Utils
    {

        /// <summary>
        /// String representations of filter operations 
        /// </summary>
        static public string[] FilterOps = new string[]
        {
            "=",
            ">=",
            "<=",
            ">",
            "<",
            "!="
        };

        /// <summary>
        /// Dictionary to convert query string into product member name
        /// </summary>
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


        /// <summary>
        /// Dictionary for converting query represention into db convention
        /// </summary>
        public static Dictionary<string, string> ManufacturerStrings = new Dictionary<string, string>()
        {
            { "american_home_food_products", "A" },
            { "general_mills", "G" },
            { "kelloggs", "K" },
            { "nabisco", "N" },
            { "post", "P" },
            { "quaker_oats", "Q" },
            { "ralston_purina", "R" },

        };


        /// <summary>
        /// Dictionary for converting query represention into db convention
        /// </summary>
        public static Dictionary<string, string> TypeStrings = new Dictionary<string, string>
        {
            { "hot", "H"},
            { "cold", "C"}
        };

        /// <summary>
        /// List of acceptable manufacturer letters
        /// </summary>
        public static List<string> AcceptableManufactures = new List<string>
        {
            "A",
            "G",
            "K",
            "N",
            "P",
            "Q",
            "R"
        };

        /// <summary>
        /// List of acceptable types
        /// </summary>
        public static List<string> AcceptableTypes = new List<string>
        {
            "H",
            "C"
        };

        /// <summary>
        /// Converts manufacturer query string into database string
        /// </summary>
        /// <param name="manu"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ManufacturerToLetter(string manu)
        {
            string result = null;
            if(!ManufacturerStrings.TryGetValue(manu.ToLower(), out result))
            {
                throw new Exception(string.Format("Invalid manufacturer: {0}",manu));
            }
            return result;
        }

        public static string TypeToLetter(string type)
        {
            string result = null;
            if (!TypeStrings.TryGetValue(type.ToLower(), out result))
            {
                throw new Exception(string.Format("Invalid type: {0}", type));
            }
            return result;            
        }


        /// <summary>
        /// Splits the query into filter elements
        /// </summary>
        /// <param name="query"></param>
        /// <returns>List of filter entries. Returns empty list if no query present</returns>
        static public List<ProductFilterEntity> ParseQuery(IQueryCollection query)
        {
            List<ProductFilterEntity> filter = new List<ProductFilterEntity>();
            if (query == null)
            {
                return filter;
            }

            foreach(var entry in query)
            {
                // Check for assignment operation
                if (entry.Value.ToString().Length > 0 && entry.Key != "sort")
                {
                    var category = "";
                    if (ProductMemberNames.TryGetValue(entry.Key.ToLower(), out category))
                    {
                        var value = entry.Value.ToString();
                        if (category == "Mfr")
                        {
                            value = ManufacturerToLetter(value);
                        }
                        else if (category == "Type")
                        {
                            value = TypeToLetter(value);

                        }
                        filter.Add(new ProductFilterEntity(category, "=", value));
                    }
                }

                // Check for other operations
                foreach (var op in FilterOps)
                {

                    if (entry.Key.Contains(op))
                    {
                        var values = entry.Key.Split(op);
                        string category = "";
                        string value = values[1];
                        if (ProductMemberNames.TryGetValue(values[0].ToLower(), out category))
                        {
                            if (category == "Mfr")
                            {
                                value = ManufacturerToLetter(value);
                            }
                            else if (category == "Type")
                            {
                                value = TypeToLetter(value);

                            }
                            filter.Add(new ProductFilterEntity(category, op, value));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Category does not exist: {0}!", values[0]));
                            filter.Clear();
                            return filter;
                        }
                    }
                                                               
                }

                // Check for sort
                if(entry.Key == "sort")
                {
                    string category = "";
                    var values = entry.Value.ToString().Split("_");
                    if(ProductMemberNames.TryGetValue(values[0].ToLower(), out category))
                    {
                        // If values.Length == 1 Then its ascending order
                        filter.Add(new ProductFilterEntity(entry.Key, null, category, values.Length == 1));
                    }
                    
                }
            }
            return filter;
        }


        /// <summary>
        /// Reads a text file and splits it into lines TODO: Params
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="delim"></param>
        /// <returns>List of strings for each line</returns>
        static public List<string[]> ReadFile(string path, char delim = ',')
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
       

      
    }
}
