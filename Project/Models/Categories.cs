using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Imagecat { get; set; }


        public virtual ICollection<Product> Products { get; set; }

    }
}