using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuelman.DAL
{
    public class UnitOfWork : IDisposable
    {
        private VehicleDbContext context = new VehicleDbContext();
        private GenericRepository<Vehicle> vehicleRepository;
        private GenericRepository<Brand> brandRepository;

        public GenericRepository<Vehicle> VehicleRepository 
        { 
            get{
                if(this.vehicleRepository == null)
                {
                    this.vehicleRepository = new GenericRepository<Vehicle>(context);
                }
                return this.vehicleRepository;
            }
        }

        public GenericRepository<Brand> BrandRepository 
        { 
            get{
                if(this.brandRepository == null)
                {
                    this.brandRepository = new GenericRepository<Brand>(context);
                }
                return this.brandRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region IDisposable Members
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}