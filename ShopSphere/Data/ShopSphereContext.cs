using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data
{
    using Microsoft.EntityFrameworkCore;
    using ShopSphere.Models;
    using System;
    using System.Collections.Generic;

    public class ShopSphereContext : DbContext
    {
       

        public string DbPath { get; }   
        public DbSet<User> Users { get; set; }

        public ShopSphereContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "shopsphere.db");
            Console.WriteLine(DbPath);
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

 
  
}
