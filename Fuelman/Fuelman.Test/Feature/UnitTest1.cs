using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuelman.DAL;
using Fuelman.Models;

namespace Fuelman.Test.Feature
{
    [TestClass]
    public class UnitTest1
    {
        private Vehicle Insert_And_Prepare_Vehicle(UnitOfWork uof)
        {
            GenericRepository<Vehicle> vehicleRepository = uof.VehicleRepository;

            Brand audi = new Brand() { BrandName = "Audi" };
            Model a4 = new Model() { ModelName = "A4" };
            audi.Models.Add(a4);

            RefillUnit litres = new RefillUnit() { Unit = "l" };

            Vehicle myAudiA4 = new Vehicle()
            {
                Brand = audi,
                Model = a4,
                Name = "MyAudiA4",
                RefillUnit = litres
            };

            uof.VehicleRepository.Insert(myAudiA4);

            uof.Save();

            return myAudiA4;
        }

        private void Make_Two_Refills(Vehicle vehicle)
        {
            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40.0f,
                IsFullTank = true,
                Odometer = 2000,
                RefillDate = DateTime.Today.AddDays(-5),
                RefillUnit = vehicle.RefillUnit
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40f,
                IsFullTank = true,
                Odometer = 2400,
                RefillDate = DateTime.Today.AddDays(-2),
                RefillUnit = vehicle.RefillUnit
            });

        }

        [TestMethod]
        public void Test_Fuel_Calculation()
        {
            // Create a vehicle and Refill it twice.
            UnitOfWork uof = new UnitOfWork();
            Vehicle myAudiA4 = this.Insert_And_Prepare_Vehicle(uof);
            this.Make_Two_Refills(myAudiA4);
            uof.Save();

            // Calculate the Fuel Economy.
            FuelEconomyCalculator fc = FuelEconomyCalculator.Calculator;
            FuelEconomyResult feResult = fc.GetFuelEconomy(myAudiA4);
            FuelEconomyEntry fee = feResult[0];

            Assert.AreEqual(fee.Economy, 400f / 40);
        }
    }
}
