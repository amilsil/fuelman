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
    public class RefillUnitController : ApiController
    {
        private IRepository<RefillUnit> refillUnitRepository;

        public RefillUnitController()
        {
            UnitOfWork uof = new UnitOfWork();
            refillUnitRepository = uof.RefillUnitRepository;
        }
        // GET api/RefillUnit
        public IEnumerable<RefillUnit> GetRefillUnits()
        {
            var RefillUnits = refillUnitRepository.Get(null);
            return RefillUnits.AsEnumerable();
        }

        // GET api/RefillUnit/5
        public RefillUnit GetRefillUnit(int id)
        {
            RefillUnit RefillUnit = refillUnitRepository.GetByID(id);
            if (RefillUnit == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return RefillUnit;
        }

        // PUT api/RefillUnit/5
        public HttpResponseMessage PutRefillUnit(int id, RefillUnit RefillUnit)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != RefillUnit.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            refillUnitRepository.Update(RefillUnit);

            try
            {
                refillUnitRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/RefillUnit
        public HttpResponseMessage PostRefillUnit(RefillUnit RefillUnit)
        {
            if (ModelState.IsValid)
            {
                refillUnitRepository.Insert(RefillUnit);
                refillUnitRepository.Save();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, RefillUnit);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = RefillUnit.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/RefillUnit/5
        public HttpResponseMessage DeleteRefillUnit(int id)
        {
            RefillUnit RefillUnit = refillUnitRepository.GetByID(id);
            if (RefillUnit == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

           refillUnitRepository.Delete(RefillUnit);

            try
            {
                refillUnitRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, RefillUnit);
        }

        protected override void Dispose(bool disposing)
        {
            //RefillUnitRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
