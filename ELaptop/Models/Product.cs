using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELaptop.Models
{
    public class Product
    {
        [Key]
        public int Product_id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public string Product_name { get; set; }
        [Required(ErrorMessage = "Product category is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Product Price is required.")]
        public decimal Price { get; set; }

        [Required]
        public int Storage { get; set; }

        [Required]
        public string CPU { get; set; }

        [Required]
        public int RAM { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public System.Data.Entity.DbSet<ELaptop.Models.Order> Orders { get; set; }
    }
}