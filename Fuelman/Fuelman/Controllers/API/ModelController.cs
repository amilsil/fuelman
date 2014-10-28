using Fuelman.DAL;
using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fuelman.Controllers.API
{
    public class ModelController : ApiController
    {
        private IRepository<Model> modelRepository;
        
        public ModelController()
        {
            UnitOfWork uof = new UnitOfWork();
            modelRepository = uof.ModelRepository;
        }
        // GET api/Model
        public IEnumerable<Model> GetModels()
        {
            var queryValues = Request.RequestUri.ParseQueryString();
            int brand = 0;
            if (queryValues.Count > 0)
            {
                if (queryValues.HasKeys())
                {
                    Int32.TryParse(queryValues["brand"], out brand);
                }
            }

            var Models = modelRepository.Get(m => m.Brand.Id == brand);
            return Models.AsEnumerable();
        }

        // GET api/Model/5
        public Model GetModel(int id)
        {
            Model Model = modelRepository.GetByID(id);
            if (Model == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Model;
        }

        // PUT api/Model/5
        public HttpResponseMessage PutModel(int id, Model Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != Model.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            modelRepository.Update(Model);

            try
            {
                modelRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Model
        public HttpResponseMessage PostModel(Model Model)
        {
            if (ModelState.IsValid)
            {
                modelRepository.Insert(Model);
                modelRepository.Save();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Model);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = Model.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Model/5
        public HttpResponseMessage DeleteModel(int id)
        {
            Model Model = modelRepository.GetByID(id);
            if (Model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            modelRepository.Delete(Model);

            try
            {
                modelRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, Model);
        }

        protected override void Dispose(bool disposing)
        {
            //ModelRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
