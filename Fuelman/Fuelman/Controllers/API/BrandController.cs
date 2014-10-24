using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fuelman.Models;
using Fuelman.DAL;

namespace Fuelman.Controllers.API
{
    public class BrandController : ApiController
    {
        private IRepository<Brand> brandRepository;
        private IRepository<Model> modelRepository;

        public BrandController()
        {
            UnitOfWork uof = new UnitOfWork();
            modelRepository = uof.ModelRepository;
            brandRepository = uof.BrandRepository;
        }
        // GET api/Brand
        public IEnumerable<Brand> GetBrands()
        {
            var brands = brandRepository.Get(null);
            return brands.AsEnumerable();
        }

        // GET api/Brand/5
        public Brand GetBrand(int id)
        {
            Brand brand = brandRepository.GetByID(id);
            if (brand == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return brand;
        }

        // PUT api/Brand/5
        public HttpResponseMessage PutBrand(int id, Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != brand.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            brandRepository.Update(brand);

            try
            {
                brandRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Brand
        public HttpResponseMessage PostBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                brandRepository.Insert(brand);
                brandRepository.Save();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, brand);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = brand.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Brand/5
        public HttpResponseMessage DeleteBrand(int id)
        {
            Brand brand = brandRepository.GetByID(id);
            if (brand == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

           brandRepository.Delete(brand);

            try
            {
                brandRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, brand);
        }

        protected override void Dispose(bool disposing)
        {
            //brandRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}