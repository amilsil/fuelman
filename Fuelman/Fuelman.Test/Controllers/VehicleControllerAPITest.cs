using System;
using Fuelman.Controllers;
using Fuelman.Models;
using Fuelman.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuelman.Test.Controllers
{
    [TestClass]
    public class VehicleControllerAPITest
    {
        [TestMethod]
        public void TestMethod1()
        {
            InMemoryGenericRepository<Vehicle> vehicleRepository = new InMemoryGenericRepository<Vehicle>();
            VehicleController vehicleController = new VehicleController(
                new InMemoryGenericRepository<Vehicle>(),
                new InMemoryGenericRepository<Brand>(),
                new InMemoryGenericRepository<Model>());
        }
    }
}
