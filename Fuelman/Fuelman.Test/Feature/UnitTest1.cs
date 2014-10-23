using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuelman.DAL;
using Fuelman.Models;
using Fuelman.Test.Models;

namespace Fuelman.Test.Feature
{
    [TestClass]
    public class UnitTest1
    {
        private Vehicle Insert_And_Prepare_Vehicle()
        {
            // GenericRepository<Vehicle> vehicleRepository = uof.VehicleRepository;
            InMemoryGenericRepository<Vehicle> vehicleRepository = new InMemoryGenericRepository<Vehicle>();

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

            vehicleRepository.Insert(myAudiA4);

            return myAudiA4;
        }

        [TestMethod]
        public void Fuel_Calculate_Two_Fulltank_Refills()
        {
            // Create a vehicle and Refill it twice.
            // UnitOfWork uof = new UnitOfWork();
            Vehicle vehicle = this.Insert_And_Prepare_Vehicle();

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40.0f, IsFullTank = true, Odometer = 2000,
                RefillDate = DateTime.Today.AddDays(-5) 
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40f, IsFullTank = true, Odometer = 2400,
                RefillDate = DateTime.Today.AddDays(-2)
            });

            // Calculate the Fuel Economy.
            FuelEconomyCalculator fc = FuelEconomyCalculator.Calculator;
            FuelEconomyResult feResult = fc.GetFuelEconomy(vehicle);
            FuelEconomyEntry fee = feResult[0];

            Assert.AreEqual(400f / 40, fee.Economy);
        }

        [TestMethod]
        public void Fuel_Calculate_Fulltank_Partial_Fulltank_Refills()
        {
            // Create a vehicle and Refill it twice.
            // UnitOfWork uof = new UnitOfWork();
            Vehicle vehicle = this.Insert_And_Prepare_Vehicle();

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40.0f, IsFullTank = true, Odometer = 2000,
                RefillDate = DateTime.Today.AddDays(-8)
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 20f, IsFullTank = false, Odometer = 2200, 
                RefillDate = DateTime.Today.AddDays(-5)
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40f, IsFullTank = true, Odometer = 2600,
                RefillDate = DateTime.Today.AddDays(-2)
            });

            // Calculate the Fuel Economy.
            FuelEconomyCalculator fc = FuelEconomyCalculator.Calculator;
            FuelEconomyResult feResult = fc.GetFuelEconomy(vehicle);
            FuelEconomyEntry fee = feResult[0];

            Assert.AreEqual(600f / 60f, fee.Economy);
        }

        [TestMethod]
        public void Fuel_Calculate_Partial_Fulltank_Fulltank_Refills()
        {
            // Create a vehicle and Refill it twice.
            // UnitOfWork uof = new UnitOfWork();
            Vehicle vehicle = this.Insert_And_Prepare_Vehicle();

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 20.0f, IsFullTank = false, Odometer = 2000,
                RefillDate = DateTime.Today.AddDays(-8)
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40f, IsFullTank = true, Odometer = 2200,
                RefillDate = DateTime.Today.AddDays(-5)
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40f, IsFullTank = true, Odometer = 2600,
                RefillDate = DateTime.Today.AddDays(-2)
            });

            // Calculate the Fuel Economy.
            FuelEconomyCalculator fc = FuelEconomyCalculator.Calculator;
            FuelEconomyResult feResult = fc.GetFuelEconomy(vehicle);
            FuelEconomyEntry fee = feResult[0];

            Assert.AreEqual(400f / 40f, fee.Economy);
        }

        [TestMethod]
        public void Fuel_Calculate_Five_Mixed_Refills()
        {
            // Create a vehicle and Refill it twice.
            // UnitOfWork uof = new UnitOfWork();
            Vehicle vehicle = this.Insert_And_Prepare_Vehicle();

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 20.0f, IsFullTank = false, Odometer = 2000,
                RefillDate = DateTime.Today.AddDays(-10)
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40f, IsFullTank = true, Odometer = 2200,
                RefillDate = DateTime.Today.AddDays(-8)
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40f, IsFullTank = true, Odometer = 2600,
                RefillDate = DateTime.Today.AddDays(-5)
            });
            
            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 30f, IsFullTank = false, Odometer = 2700,
                RefillDate = DateTime.Today.AddDays(-2)
            });

            vehicle.Refills.Add(new Refill()
            {
                RefillAmount = 40f, IsFullTank = true, Odometer = 3100,
                RefillDate = DateTime.Today
            });
            // Calculate the Fuel Economy.
            FuelEconomyCalculator fc = FuelEconomyCalculator.Calculator;
            FuelEconomyResult feResult = fc.GetFuelEconomy(vehicle);
            
            Assert.AreEqual(400f / 40f, feResult[0].Economy);
            Assert.AreEqual(500f / 70f, feResult[1].Economy);
        }
    }
}
