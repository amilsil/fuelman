using Fuelman.DAL;
using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fuelman.Controllers.API
{
    public class RefillController : ApiController
    {
        private IRepository<Refill> RefillRepository;

        public RefillController()
        {
            UnitOfWork uof = new UnitOfWork();
            RefillRepository = uof.RefillRepository;
        }
        // GET api/Refill
        public IEnumerable<Refill> GetRefills()
        {
            var queryValues = Request.RequestUri.ParseQueryString();
            int vehicleId = 0;
            if (queryValues.Count > 0)
            {
                if (queryValues.HasKeys())
                {
                    Int32.TryParse(queryValues["vehicleId"], out vehicleId);
                }
            }

            var Refills = RefillRepository.Get( r => r.VehicleId == vehicleId);
            return Refills.AsEnumerable();
        }

        // GET api/Refill/5
        public Refill GetRefill(int id)
        {
            Refill Refill = RefillRepository.GetByID(id);
            if (Refill == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Refill;
        }

        // PUT api/Refill/5
        public HttpResponseMessage PutRefill(int id, Refill Refill)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != Refill.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            RefillRepository.Update(Refill);

            try
            {
                RefillRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Refill
        public HttpResponseMessage PostRefill(Refill Refill)
        {
            if (ModelState.IsValid)
            {
                RefillRepository.Insert(Refill);
                RefillRepository.Save();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Refill);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = Refill.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Refill/5
        public HttpResponseMessage DeleteRefill(int id)
        {
            Refill Refill = RefillRepository.GetByID(id);
            if (Refill == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

           RefillRepository.Delete(Refill);

            try
            {
                RefillRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, Refill);
        }

        protected override void Dispose(bool disposing)
        {
            //RefillRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
