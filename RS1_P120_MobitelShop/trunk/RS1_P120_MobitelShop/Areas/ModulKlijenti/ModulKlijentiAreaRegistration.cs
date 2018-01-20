using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti
{
    public class ModulKlijentiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulKlijenti";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            var ns = typeof(RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers.HomeController).Namespace;

            var routeName = AreaName + "_Route";
            var routeUrl = AreaName + "/{controller}/{action}/{id}";
            //var routeDefaults = new { controller = "Home", action = "Index", id = UrlParameter.Optional };
            var routeDefaults = new { controller = "Home", action = "Index", area = AreaName, id = UrlParameter.Optional };

            var routeNamespaces = new[] { ns };

            context.MapRoute(
                name: routeName,
                url: routeUrl,
                defaults: routeDefaults,
                namespaces: routeNamespaces
                );
            //context.MapRoute(
            //    "ModulKlijenti_default",
            //    "ModulKlijenti/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}