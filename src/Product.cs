using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CerealAPI.src
{


    public class ProductFactory
    {
        public ProductFactory() { }

        static public Product CreateProduct(string csv_row)
        {
            var values = csv_row.Split(';');
            Product p = new Product();
            try
            {
                p.Name = values[0];
                p.Mfr = ParseChar(values[1], Product.AcceptableManufactures);
                p.Type = ParseChar(values[2], Product.AcceptableTypes);
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

        private static char ParseChar(string manu, List<char> acceptable_chars)
        {
            char res = '\0';
            try
            {
                res = char.Parse(manu);
                if(!acceptable_chars.Contains(res))
                {
                    throw new Exception(string.Format("Invalid char provided: {0}", manu));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);
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




    public class Product
    {
        [Key] int ID;

        //public static Dictionary<string, Manufacturers> ManuDict = new Dictionary<string, Manufacturers>
        //{
        //    {"A", Manufacturers.A },
        //    {"G", Manufacturers.G },
        //    {"K", Manufacturers.K },
        //    {"N", Manufacturers.N },
        //    {"P", Manufacturers.P },
        //    {"Q", Manufacturers.Q },
        //    {"R", Manufacturers.R }
        //};
        //public enum Manufacturers
        //{
        //    A,  //  American Home Food Products
        //    G,  // General Mills
        //    K,  // Kelloggs
        //    N,  // Nabisco
        //    P,  // Post
        //    Q,  // Quaker Oats
        //    R   // Ralston Purina
        //};


        //public static string[] manufacturerFullStrings = {
        //    "American Home Food Products",
        //    "General Mills",
        //    "Kelloggs",
        //    "Nabisco",
        //    "Post",
        //    "Quaker Oats",
        //    "Ralston Purina"
        //};

        //public static Dictionary<string, PType> PTypeDict = new Dictionary<string, PType>
        //{
        //    { "H", PType.Hot},
        //    { "C", PType.Cold }
        //};
        //public enum PType
        //{
        //    Hot,
        //    Cold
        //}
        //public static string[] pTypeFullStrings =
        //{
        //    "Hot",
        //    "Cold"
        //};

        public static List<char> AcceptableManufactures = new List<char>
        {
            'A',
            'G',
            'K',
            'N',
            'P',
            'Q',
            'R'
        };
        public static List<char> AcceptableTypes = new List<char>
        {
            'H',
            'C'
        };

        public int Id { get; set; }
        public string Name { get; set; }

        //public Manufacturers Mfr { get; set; }
        public char Mfr { get; set; }

        //public PType Type { get; set; }
        public char Type { get; set; }

        public int Calories { get; set; }
        public int Protien { get; set; }
        public int Fat { get; set; }
        public int Sodium { get; set; }
        public float Fiber { get; set; }
        public float Carbo { get; set; }
        public int Sugars { get; set; }
        public int Potass { get; set; }
        public int Vitamins { get; set; }
        public int Shelf { get; set; }
        public float Weight { get; set; }
        public float Cups { get; set; }
        public float Rating { get; set; }


    }

}



