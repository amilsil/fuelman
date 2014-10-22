using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuelman.DAL
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetVehicles();
        Vehicle GetVehicleByID(int VehicleId);
        void InsertVehicle(Vehicle Vehicle);
        void DeleteVehicle(int VehicleID);
        void UpdateVehicle(Vehicle Vehicle);
        void Save();
    }
}
