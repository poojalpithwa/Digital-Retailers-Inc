using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELaptop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Product_Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Customer_Name { get; set; }

        [Required]
        public string Cusomer_Address { get; set; }

        [Required]
        public string Customer_Phone    { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}