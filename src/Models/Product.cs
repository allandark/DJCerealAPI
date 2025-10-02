using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CerealAPI.src.Models
{
    /// <summary>
    /// The product model stored on the database
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key] public int Id { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }

        public string Mfr { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

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



