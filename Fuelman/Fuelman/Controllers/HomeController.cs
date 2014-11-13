using Fuelman.DAL;
using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fuelman.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork uof;
        public HomeController()
        {
            uof = new UnitOfWork();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //Database.SetInitializer<VehicleDbContext>(new VehicleDbContextInitializer());
            //VehicleDbContext db = new VehicleDbContext();
            //db.Database.Initialize(true);

            return View();
        }

        public ActionResult vehicles()
        {
            ViewBag.ApiUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("/api"));

            var vehicles = uof.VehicleRepository.Get(null);

            return View();
        }

        public ActionResult test()
        {
            return View();
        }
    }
}
