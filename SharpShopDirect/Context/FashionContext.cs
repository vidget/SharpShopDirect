using Microsoft.AspNet.Identity.EntityFramework;
using SharpShopDirect.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SharpShopDirect.Context
{
    public class FashionContext : IdentityDbContext
    {

        // You can add custom code to this file. Changes will not be overwritten.
        //
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema,
        // please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx


        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Item> Items { get; set; }




    }
}