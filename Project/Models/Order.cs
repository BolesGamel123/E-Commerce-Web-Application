using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        
        public string OrderEmail { get; set; }
        
        public string Address { get; set; }
        public string UserID { get; set; }
        public double TotalPrice { get; set; }
       [ForeignKey("UserID")]
        public virtual IdentityUser IdentityUser { get; set; }
        public virtual ICollection <Product> Product { get; set; }


    }
}