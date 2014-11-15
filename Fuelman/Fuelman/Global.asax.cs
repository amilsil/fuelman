using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Fuelman
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            var serializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var dateTimeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd hh:mm:ss"
            };

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                               .Converters.Add(dateTimeConverter);

            GlobalConfiguration.Configuration.Services.RemoveAll(
                typeof(System.Web.Http.Validation.ModelValidatorProvider),
                v => v is System.Web.Http.Validation.Providers.InvalidModelValidatorProvider);
            //serializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            //serializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
    }
}