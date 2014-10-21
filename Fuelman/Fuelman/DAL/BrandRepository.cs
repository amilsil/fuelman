using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuelman.DAL
{
    public class BrandRepository : IBrandRepository, IDisposable
    {
        VehicleDbContext context;
        public BrandRepository(VehicleDbContext context)
        {
            this.context = context;
        }

        #region IBrandRepository Members
        public IEnumerable<Brand> GetBrands()
        {
            return this.context.Brands.ToList();
        }

        public IEnumerable<Model> GetModelsOfBrand(Brand brand)
        {
            
        }
        #endregion

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