﻿using Fuelman.DAL;
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
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "vehicle", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

            var vehicles = uof.VehicleRepository.Get(null);

            // return View(vehicles);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
