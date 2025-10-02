namespace CerealAPI.src.Models
{
    public class ProductFactory
    {

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

        public static Dictionary<string, string> TypeStrings = new Dictionary<string, string>
        {
            { "hot", "H"},
            { "cold", "C"}
        };

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
        public static List<string> AcceptableTypes = new List<string>
        {
            "H",
            "C"
        };


        static public Product CreateProduct(string csv_row)
        {
            var values = csv_row.Split(';');
            Product p = new Product();
            try
            {
                p.Name = values[0];
                p.Mfr = ParseChar(values[1], AcceptableManufactures);
                p.Type = ParseChar(values[2], AcceptableTypes);
                p.Calories = ParseInt(values[3]);
                p.Protien = ParseInt(values[4]);
                p.Fat = ParseInt(values[5]);
                p.Sodium = ParseInt(values[6]);
                p.Fiber = ParseFloat(values[7]);
                p.Carbo = ParseFloat(values[8]);
                p.Sugars = ParseInt(values[9]);
                p.Potass = ParseInt(values[10]);
                p.Vitamins = ParseInt(values[11]);
                p.Shelf = ParseInt(values[12]);
                p.Weight = ParseFloat(values[13]);
                p.Cups = ParseFloat(values[14]);
                p.Rating = ParseFloat(RemoveLeadingChars(values[15], "."));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return p;
        }

        private static string ParseChar(string manu, List<string> acceptable_chars)
        {
            string res = "";
            try
            {
                res = manu;
                if (!acceptable_chars.Contains(res))
                {
                    throw new Exception(string.Format("Invalid char provided: {0}", manu));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return res;
        }


        private static int ParseInt(string value_in)
        {
            int valueOut = 0;
            bool result;
            result = int.TryParse(value_in, out valueOut);
            if (!result)
            {
                throw new Exception(string.Format("Parse error int: {}", value_in));
            }

            return valueOut;
        }
        private static float ParseFloat(string value_in)
        {
            float valueOut = 0;
            bool result;
            result = float.TryParse(value_in, out valueOut);
            if (!result)
            {
                throw new Exception(string.Format("Parse error float: {}", value_in));
            }

            return valueOut;
        }

        private static string RemoveLeadingChars(string str, string delim)
        {
            int last_index = str.LastIndexOf(delim);
            int index = str.IndexOf(delim);
            string result = str;
            while (index != last_index)
            {
                result = result.Remove(index, 1);
                index = result.IndexOf(delim);
                last_index = result.LastIndexOf(delim);
            }
            return result;
        }
    }

}
