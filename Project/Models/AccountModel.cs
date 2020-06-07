using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class AccountModel
    {
        public class ApplicationDBContext : IdentityDbContext<IdentityUser>
        {
            public ApplicationDBContext() : base("Project")
            {

            }

            public ApplicationDBContext(string connectioname) : base(connectioname)
            {

            }
            public DbSet<Order> Orders { get; set; }
           
            public DbSet<Product> Products { get; set; }
            public DbSet<Categories> Categories { get; set; }

        }
    }
}