using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes(); //włącza opcję atribute routes która podmieni action "nazwa" na atribute routes

            //tak się tworzy coustom route czyli niestandardową trasę - routes.MapRoute(3 parametry) i tyle

            /* // po włączeniu powyżej routes.MapMvcAttributeRoutes(); możemy usunąć poniższą routes.MapRoute 
            routes.MapRoute(
                "MoviesByReleaseDate",
                "movies/released/{year}/{month}",
                new {controller = "Movies", action = "ByReleaseDate"},
                //new { year = @"\d{4}", month = @"\d{2}"}); //oklejny argument to obiekt anonimowy któy dodajemy dodatkowo (nie jest wymagany)
                new {year = @"2015|2016", month = @"/d{2}"}); // @"2015|2016" ograniczenie do roku 2015-2016
                // taki zapis @"\d{4}" to - ograniczenie na year że ma podać 4 cyfry np 2020 to samo month
            */

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
/*MapRoute() przyjmuje trzy parametry : name, url, defaults
 name - powinien być unikatowy
 url - wzorzec projektowy url (url pattern)
 defaults - użyjemy tutaj obiektu anonimowego, gdzie wypisujemy nazwę kontrolera i akcję*/