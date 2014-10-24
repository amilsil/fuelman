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
            // Refill Units 
            RefillUnit unit = new RefillUnit() { Unit = "l" };
            dbContext.RefillUnits.Add(unit);

            // Brand with several models.
            var toyotaBrand = new Brand() { BrandName = "Toyota" };
            var vitzModel = new Model() { ModelName = "Vitz" };
            toyotaBrand.Models.Add(vitzModel);

            // Car
            Vehicle myToyota = new Vehicle()
            {
                Brand = toyotaBrand,
                Model = vitzModel,
                Name = "MyVitz",
                RefillUnit = unit
            };

            // Refill
            myToyota.Refills.Add(
                new Refill()
                {
                    RefillAmount = 5000.00f,
                    Odometer = 1000,
                    RefillDate = DateTime.Today.AddDays(-5),
                    IsFullTank = true
                }
            );

            // Refill
            myToyota.Refills.Add(
                new Refill()
                {
                    RefillAmount = 5000.00f,
                    Odometer = 1200,
                    RefillDate = DateTime.Today,
                    IsFullTank = true
                }
            );  

            dbContext.Brands.Add(toyotaBrand);
            dbContext.Vehicles.Add(myToyota);

            base.Seed(dbContext);
        }
    }
}