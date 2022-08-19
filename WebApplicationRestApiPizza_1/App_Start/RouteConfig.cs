using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplicationRestApiPizza_1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
             routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // routes.IgnoreRoute("{webform}.aspx/{*pathInfo}");
            // exemple   routes.IgnoreRoute("Employee/");  
         //   routes.MapPageRoute("test1", "test1", "~/Views/pizzas/WebForm1.aspx");//attention bloque le @html.actionlink
           // routes.MapPageRoute("test2", "test2", "~/WebForm1.aspx");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
