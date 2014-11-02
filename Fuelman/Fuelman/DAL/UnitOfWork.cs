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
        private GenericRepository<Model> modelRepository;
        private GenericRepository<RefillUnit> refillUnitRepository;

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

        public GenericRepository<Model> ModelRepository
        {
            get
            {
                if (this.modelRepository == null)
                {
                    this.modelRepository = new GenericRepository<Model>(context);
                }
                return this.modelRepository;
            }
        }

        public GenericRepository<RefillUnit> RefillUnitRepository
        {
            get
            {
                if (this.refillUnitRepository == null)
                {
                    this.refillUnitRepository = new GenericRepository<RefillUnit>(context);
                }
                return this.refillUnitRepository;
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