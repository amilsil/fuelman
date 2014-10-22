using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuelman.DAL
{
    public class VehicleRepository : IVehicleRepository
    {
        VehicleDbContext context;

        public VehicleRepository(VehicleDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Models.Vehicle> GetVehicles()
        {
            return this.context.Vehicles.ToList();
        }

        public Models.Vehicle GetVehicleByID(int VehicleId)
        {
            return this.context.Vehicles.Find(VehicleId);
        }

        public void InsertVehicle(Models.Vehicle Vehicle)
        {
            this.context.Vehicles.Add(Vehicle);
        }

        public void DeleteVehicle(int VehicleID)
        {
            Vehicle vehicle = this.context.Vehicles.Find(VehicleID);
            this.context.Vehicles.Remove(vehicle);
        }

        public void UpdateVehicle(Models.Vehicle Vehicle)
        {
            this.context.Entry(Vehicle).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}