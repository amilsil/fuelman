using Fuelman.DAL;
using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fuelman.Controllers.API
{
    /// <summary>
    /// Calculates fuel economy statistics on a vehicle and returns a json object.
    /// </summary>
    public class StatController : ApiController
    {
        IRepository<Vehicle> vehicleRepository;

        public StatController()
        {
            UnitOfWork uof = new UnitOfWork();
            this.vehicleRepository = uof.VehicleRepository;
        }

        // GET api/stat/5
        public FuelEconomyResult GetStat(int id)
        {
            IEnumerable<Vehicle> vehicles = this.vehicleRepository.Get(
                v => v.Id == id, includeProperties: "Refills");

            Vehicle vehicle = vehicles.FirstOrDefault();

            FuelEconomyCalculator fc = FuelEconomyCalculator.Calculator;
            FuelEconomyResult feResult = fc.GetFuelEconomy(vehicle);

            return feResult;
        }

    }
}
