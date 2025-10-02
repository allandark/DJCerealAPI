using CerealAPI.src.Data;
using CerealAPI.src.Models;
using CerealAPI.src.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
        /// Dictionary containing the different operations to apply the a List<Product>
        /// </summary>
        static public Dictionary<string, Func<ProductFilterEntity, List<Product>, List<Product>>> FilterTable =
            new Dictionary<string, Func<ProductFilterEntity, List<Product>, List<Product>>>
        {
            {   "=",
                (filter, products ) =>
                {
                    return products.Where(p =>
                    {
                        var prop = typeof(Product).GetProperty(filter.Key);
                        if (prop == null) return false;
                        var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                        var value = prop.GetValue(p);
                        return value != null && value == convertedFilterValue;
                    }).ToList();
                }
            },
                {   "!=",
                (filter, products ) =>
                {
                    return products.Where(p =>
                    {
                        var prop = typeof(Product).GetProperty(filter.Key);
                        if (prop == null) return false;
                        var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                        var value = prop.GetValue(p);
                        return value != null &&  value != convertedFilterValue;
                    }).ToList();
                }
            },
            {   "<=",
                (filter, products ) =>
                {
                    return products.Where(p =>
                    {
                        var prop = typeof(Product).GetProperty(filter.Key);
                        if (prop == null) return false;
                        var value = prop.GetValue(p);
                        if (value == null || filter.Value == null) return false;
                        var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                        if (value is IComparable comparableValue && convertedFilterValue is IComparable comparableFilter)
                            return comparableValue.CompareTo(convertedFilterValue) <= 0;
                        return false;
                    }).ToList();
                }
            },
            {   "<",
                (filter, products ) =>
                {
                    return products.Where(p =>
                    {
                        var prop = typeof(Product).GetProperty(filter.Key);
                        if (prop == null) return false;
                        var value = prop.GetValue(p);
                        if (value == null || filter.Value == null) return false;
                        var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                        if (value is IComparable comparableValue && convertedFilterValue is IComparable comparableFilter)
                            return comparableValue.CompareTo(convertedFilterValue) < 0;
                        return false;
                    }).ToList();
                }
            },
            {   ">=",
                (filter, products ) =>
                {
                    return products.Where(p =>
                    {
                        var prop = typeof(Product).GetProperty(filter.Key);
                        if (prop == null) return false;
                        var value = prop.GetValue(p);
                        if (value == null || filter.Value == null) return false;
                        var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                        if (value is IComparable comparableValue && convertedFilterValue is IComparable comparableFilter)
                            return comparableValue.CompareTo(convertedFilterValue) >= 0;
                        return false;
                    }).ToList();
                }
            },
            {   ">",
                (filter, products ) =>
                {
                    return products.Where(p =>
                    {
                        var prop = typeof(Product).GetProperty(filter.Key);
                        if (prop == null) return false;
                        var value = prop.GetValue(p);
                        if (value == null || filter.Value == null) return false;
                        var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                        if (value is IComparable comparableValue && convertedFilterValue is IComparable comparableFilter)
                            return comparableValue.CompareTo(convertedFilterValue) > 0;
                        return false;
                    }).ToList();
                }
            }
        };


        /// <summary>
        /// Splits the query string into filter elements
        /// </summary>
        /// <param name="query"></param>
        /// <returns>List of filter entries</returns>
        static public List<ProductFilterEntity> ParseQuery(string query)
        {
            List<ProductFilterEntity> filter = new List<ProductFilterEntity>();
            if (query == null)
            {
                return filter;
            }
            var entreis = query.Split('&');
            foreach (var entry in entreis)
            {
                foreach (var op in Utils.FilterOps)
                {
                    if (entry.Contains(op))
                    {

                        var values = entry.Split(op);
                        string category = "";
                        string value = values[1];
                        if (ProductMemberNames.TryGetValue(values[0].ToLower(), out category))
                        {
                            if (category == "Mfr")
                            {
                                if (!ManufacturerStrings.TryGetValue(values[1].ToLower(), out value))
                                {
                                    Console.WriteLine(string.Format("Invalid manufacturer: {0}", value));
                                    filter.Clear();
                                    return filter;
                                }
                            }
                            else if (category == "Type")
                            {
                                if (!TypeStrings.TryGetValue(values[1].ToLower(), out value))
                                {
                                    Console.WriteLine(string.Format("Invalid type: {0}", value));
                                    filter.Clear();
                                    return filter;
                                }

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
