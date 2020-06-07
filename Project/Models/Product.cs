using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
        public string Category { get; set; }
        [ForeignKey("Categories")]
        public int CategoriesID { get; set; }
       
        public virtual Categories Categories { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        
    }
}