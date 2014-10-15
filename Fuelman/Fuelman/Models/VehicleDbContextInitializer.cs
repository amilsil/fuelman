using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fuelman.Models
{
    public class VehicleDbContextInitializer : DropCreateDatabaseAlways<VehicleDbContext>
    {
        protected override void Seed(VehicleDbContext dbContext)
        {
            // seed data
            var toyota = new Brand() { BrandName = "Toyota" };
            toyota.Models.Add(new Model() { ModelName = "Corolla" });
            toyota.Models.Add(new Model() { ModelName = "Vitz" });
            toyota.Models.Add(new Model() { ModelName = "Vios" });
            toyota.Models.Add(new Model() { ModelName = "Prius" });
            dbContext.Brands.Add(toyota);


                
            base.Seed(dbContext);
        }
    }
}