using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = new MapperConfiguration(c => { 
                c.CreateMap<Customer, CustomerDto>(); 
                c.AddProfile<MappingProfile>();});
            
            //Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
