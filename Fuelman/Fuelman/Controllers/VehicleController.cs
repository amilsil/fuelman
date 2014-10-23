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
using Fuelman.DAL;
using Fuelman.Models;

namespace Fuelman.Controllers
{
    public class VehicleController : ApiController
    {
        private IRepository<Vehicle> vehicleRepository;
        private IRepository<Model> modelRepository;
        private IRepository<Brand> brandRepository;

        public VehicleController()
        {
            UnitOfWork uof = new UnitOfWork();
            vehicleRepository = uof.VehicleRepository;
            modelRepository = uof.ModelRepository;
            brandRepository = uof.BrandRepository;
        }

        public VehicleController(
            IRepository<Vehicle> vehicleRepository,
            IRepository<Brand> brandRepository,
            IRepository<Model> modelRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.modelRepository = modelRepository;
            this.brandRepository = brandRepository;
        }


        // GET api/Vehicle
        public IEnumerable<Vehicle> GetVehicles()
        {
            var vehicles = vehicleRepository.Get(null);
            return vehicles.AsEnumerable();
        }

        // GET api/Vehicle/5
        public Vehicle GetVehicle(int id)
        {
            Vehicle vehicle = vehicleRepository.GetByID(id);
            if (vehicle == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return vehicle;
        }

        // PUT api/Vehicle/5
        public HttpResponseMessage PutVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != vehicle.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            vehicleRepository.Update(vehicle);

            try
            {
                vehicleRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Vehicle
        public HttpResponseMessage PostVehicle(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                Model model = modelRepository.GetByID(vehicle.ModelId);
                vehicle.Model = model;
                vehicle.Brand = brandRepository.GetByID(vehicle.BrandId);
                
                vehicleRepository.Insert(vehicle);
                vehicleRepository.Save();


                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, vehicle);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = vehicle.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Vehicle/5
        public HttpResponseMessage DeleteVehicle(int id)
        {
            Vehicle vehicle = vehicleRepository.GetByID(id);
            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            vehicleRepository.Delete(vehicle);

            try
            {
                vehicleRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, vehicle);
        }

        protected override void Dispose(bool disposing)
        {
            // db.Dispose();
            base.Dispose(disposing);
        }
    }
}