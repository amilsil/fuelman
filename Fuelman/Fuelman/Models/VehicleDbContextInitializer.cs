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
            // Brand with several models.
            var toyota = new Brand() { BrandName = "Toyota" };
            toyota.Models.Add(new Model() { ModelName = "Corolla" });
            toyota.Models.Add(new Model() { ModelName = "Vios" });
            toyota.Models.Add(new Model() { ModelName = "Prius" });            
            var vitz = new Model() { ModelName = "Vitz" };
            toyota.Models.Add(vitz);
            
            // Car
            Vehicle myToyota = new Vehicle() { 
                Brand = toyota, 
                Model = vitz, 
                Name = "MyVitz"
            };

            dbContext.Brands.Add(toyota);
            dbContext.Vehicles.Add(myToyota);
            
            base.Seed(dbContext);
        }
    }
}