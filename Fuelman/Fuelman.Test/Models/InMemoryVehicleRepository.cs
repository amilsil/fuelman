using Fuelman.DAL;
using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuelman.Test.Models
{
    class InMemoryVehicleRepository : IVehicleRepository
    {
        private List<Vehicle> _db = new List<Vehicle>();

        public Exception ExceptionToThrow { get; set; }

        public IEnumerable<Fuelman.Models.Vehicle> GetVehicles()
        {
            return _db.ToList();
        }

        public Fuelman.Models.Vehicle GetVehicleByID(int VehicleId)
        {
            return _db.FirstOrDefault(d => d.VehicleId == VehicleId);
        }

        public void InsertVehicle(Fuelman.Models.Vehicle Vehicle)
        {
            _db.Add(Vehicle);
        }

        public void DeleteVehicle(int VehicleID)
        {
            _db.Remove(GetVehicleByID(VehicleID));
        }

        public void UpdateVehicle(Fuelman.Models.Vehicle Vehicle)
        {
            // Nothing to do. :)
        }

        public void Save()
        {
            // Nothing to do :)
        }
    }
}
