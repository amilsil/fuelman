﻿using System;
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
            toyotaBrand.Models.Add(new Model() { ModelName = "Corolla" });
            toyotaBrand.Models.Add(new Model() { ModelName = "Vios" });
            toyotaBrand.Models.Add(new Model() { ModelName = "Prius" });
            var vitzModel = new Model() { ModelName = "Vitz" };
            toyotaBrand.Models.Add(vitzModel);

            var nissanBrand = new Brand() { BrandName = "Nissan" };
            nissanBrand.Models.Add(new Model() { ModelName = "FB15" });
            nissanBrand.Models.Add(new Model() { ModelName = "N16" });

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
            dbContext.Brands.Add(nissanBrand);
            dbContext.Vehicles.Add(myToyota);

            base.Seed(dbContext);
        }
    }
}