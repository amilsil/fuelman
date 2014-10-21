using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fuelman.Models
{
    public class VehicleDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<RefillUnit> RefillUnits { get; set; }

        public VehicleDbContext()
            : base("name=DefaultConnection")
        {

        }
    }
}